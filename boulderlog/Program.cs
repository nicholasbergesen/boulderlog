using Boulderlog.Data;
using Boulderlog.Data.Models;
using Boulderlog.Domain;
using Boulderlog.Domain.Email;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO.Compression;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace Boulderlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Tokens.ProviderMap.Add("CustomEmailConfirmation", new TokenProviderDescriptor(typeof(AppEmailConfirmationTokenProvider<AppUser>)));
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });

            builder.Services.AddTransient<AppEmailConfirmationTokenProvider<AppUser>>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AppRateLimitOptions>(builder.Configuration.GetSection(AppRateLimitOptions.AppRateLimit));
            builder.Services.Configure<AppConfigOptions>(builder.Configuration.GetSection(AppConfigOptions.AppConfig));

            builder.Services
                .AddControllersWithViews()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
                });
            builder.Services.AddRazorPages();
            builder.Services.AddResponseCaching();

            builder.Services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromDays(5);
                o.SlidingExpiration = true;
            });

            var rateLimitOptions = new AppRateLimitOptions();
            builder.Configuration.GetSection(AppRateLimitOptions.AppRateLimit).Bind(rateLimitOptions);
            if (rateLimitOptions != null)
            {
                builder.Services.AddRateLimiter(rateOptions =>
                {
                    rateOptions.AddSlidingWindowLimiter(policyName: rateLimitOptions.Policy, options =>
                    {
                        options.PermitLimit = rateLimitOptions.PermitLimit;
                        options.Window = TimeSpan.FromSeconds(rateLimitOptions.Window);
                        options.SegmentsPerWindow = rateLimitOptions.SegmentsPerWindow;
                        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                        options.QueueLimit = rateLimitOptions.QueueLimit;
                        options.AutoReplenishment = rateLimitOptions.AutoReplenishment;
                    });
                    rateOptions.OnRejected = async (context, cancellationToken) =>
                    {
                        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                        {
                            context.HttpContext.Response.Headers.RetryAfter = ((int)retryAfter.TotalSeconds).ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                        }

                        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken);
                    };
                    rateOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                // Compression prevents browser Link script injection during development
                app.UseResponseCompression();
                //app.UseRateLimiter();

                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions(new Microsoft.AspNetCore.StaticFiles.Infrastructure.SharedOptions())
            {
                HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,
                OnPrepareResponse = (fileContext) =>
                {
                    fileContext.Context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(1),
                        SharedMaxAge = TimeSpan.FromHours(1),
                    };
                },
            });

            app.UseRouting();

            app.UseCors();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");//.RequireRateLimiting(rateLimitOptions.Policy);
            app.MapRazorPages();//.RequireRateLimiting(rateLimitOptions.Policy);
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/"); //Redirecting if wrong url or wrong route is added
                }
            });

            app.Run();
        }
    }
}
