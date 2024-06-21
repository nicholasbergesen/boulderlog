using Boulderlog.Models;
using System.Collections.Generic;

namespace Boulderlog.Domain
{
    public static class Const
    {
        public static class Role
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public static IEnumerable<ColorDTO> HoldColors = new List<ColorDTO>()
        {
            new ColorDTO()
            {
                ColorName = "White",
                ColorHex = "#FFFFFF",
            },
            new ColorDTO()
            {
                ColorName = "Yellow",
                ColorHex = "#ede932",
            },
            new ColorDTO()
            {
                ColorName = "Orange",
                ColorHex = "#FFA500",
            },
            new ColorDTO()
            {
                ColorName = "Green",
                ColorHex = "#008000",
            },
            new ColorDTO()
            {
                ColorName = "Blue",
                ColorHex = "#003153",
            },
            new ColorDTO()
            {
                ColorName = "Red",
                ColorHex = "#FF0000",
            },
            new ColorDTO()
            {
                ColorName = "Purple",
                ColorHex = "#860d86",
            },
            new ColorDTO()
            {
                ColorName = "Grey",
                ColorHex = "#808080",
            },
            new ColorDTO()
            {
                ColorName = "Brown",
                ColorHex = "#AB5236",
            },
            new ColorDTO()
            {
                ColorName = "Black",
                ColorHex = "#000000",

            },
            new ColorDTO()
            {
                ColorName = "Pink",
                ColorHex = "#fa9bd6",

            },
            new ColorDTO()
            {
                ColorName = "Mint",
                ColorHex = "#3EB489",

            },
        };
    }
}
