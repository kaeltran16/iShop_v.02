using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iShop.Common.DataAnnotations
{
    public class FutureDate: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Convert.ToDateTime(value) > DateTime.Now;
        }
    }
}
