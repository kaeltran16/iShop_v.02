using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iShop.Common.DataAnnotations
{
    public class NotEmptyCollection: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is ICollection collection))
                throw new ArgumentException($"{value} can not be converted into Collection.");

            return collection.Count > 0;
        }
    }
}
