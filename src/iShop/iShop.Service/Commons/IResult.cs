using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Service.Commons
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Error { get; set; }
        object Payload { get; set; }
    }
}
