using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Exceptions
{
    public class NullOrEmptyException : ApplicationException
    {
        public NullOrEmptyException(string targetName, object targetParameter)
            : base($"The {targetName} with {targetParameter} is null or empty.", null)
        {
        }

        public NullOrEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
