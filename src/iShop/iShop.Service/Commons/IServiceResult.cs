using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Service.Commons
{
    public interface IServiceResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
        dynamic Payload { get; set; }
    }
}
