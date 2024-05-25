using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Boulderlog.Data;
using Boulderlog.Data.Models;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Boulderlog.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 31536000, VaryByQueryKeys = ["id"])]
        // InvalidOperationException: 'VaryByQueryKeys' requires the response cache middleware.
        //Microsoft.AspNetCore.Mvc.Filters.ResponseCacheFilterExecutor.Execute(FilterContext context)
        public async Task<IActionResult> GetImage(string id)
        {
            var image = await _context.Image.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return File(image.Bytes, image.FileType);
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(string id, Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage([FromBody] string imageBase64)
        {
            string[] split = imageBase64.Split(',');
            string[] dataType = split[0].Substring(split[0].IndexOf(':') + 1).Split(';');
            string fileType = dataType[0];
            string format = dataType[1];

            if (format != "base64")
            {
                return BadRequest($"Image data format {format} unkown, base64 expected");
            }

            var image = new Image()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                FileName = "FileName",
                FileType = fileType,
                Bytes = Convert.FromBase64String(split[1])
            };

            _context.Image.Add(image);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImageExists(image.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(string id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Image.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageExists(string id)
        {
            return _context.Image.Any(e => e.Id == id);
        }
    }
}
