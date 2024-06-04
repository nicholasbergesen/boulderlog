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

        public static IEnumerable<string> HoldColors = new List<string>() { string.Empty, "White", "Yellow", "Orange", "Green", "Blue", "Red", "Purple", "Grey", "Brown", "Black", "Pink", "Mint" };
    }
}
