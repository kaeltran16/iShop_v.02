using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Extensions
{
    public static class GuidConverterExtension
    {
        public static Guid ToGuid(this string input, string targetName)
        {
            bool isValid = Guid.TryParse(input, out var output);
            if (!isValid)
                throw new ArgumentException($"The {targetName} is not in right format");
            return output;
        }
    }
}
