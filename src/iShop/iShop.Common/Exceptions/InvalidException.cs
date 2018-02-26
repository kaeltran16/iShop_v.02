using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Exceptions
{
    public class InvalidException: ApplicationException
    {
        public InvalidException(string targetName)
            :base($"The {targetName} is invalid.")
        {
            
        }

        public InvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
