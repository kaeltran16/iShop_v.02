using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string targetName, object targetParameter) 
            : base($"The {targetName} with {targetParameter} is not found.", null)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
