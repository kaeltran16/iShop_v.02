using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Extensions
{
    public static class IntExtension
    {
        public static int SetBackValueWhenExtended(this int target, int min, int max, int value)
        {
            target = target > max ? value : target;
            target = target < min ? value : target;
            return target;
        }
    }
}
