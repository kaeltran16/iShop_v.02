using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Exceptions
{
    public class OversizeException: ApplicationException
    {
        public OversizeException(string targetName, int targetParameter) 
            : base($"The {targetName} has reached the maximum size of {targetParameter}.", null)
        {
        }

        public OversizeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
