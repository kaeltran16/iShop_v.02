using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Service.Commons
{
    public class Result: IResult
    {
        public Result(bool isSuccess, string error = default(string), object payload = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            Payload = payload;
        }

        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public object Payload { get; set; }
    }
}
