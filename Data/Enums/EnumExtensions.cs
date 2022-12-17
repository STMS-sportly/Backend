using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
    public static class EnumExtensions
    {
        public static int EnumDescriptionToInt(Type enumType, string enumDescription) => (int)(Enum.Parse(enumType, enumDescription));

        public static string? EnumIntToString(Type enumType, int enumValue) => Enum.GetName(enumType, enumValue);
    }
}
