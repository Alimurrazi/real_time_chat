using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Message { get; set; }
        public dynamic Data { get; set; }
        public BaseResponse(bool isSuccess, List<string> message, dynamic data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public BaseResponse GetErrorResponse(string msg)
        {
            List<string> errorMsg = new List<string>();
            errorMsg.Add(msg);
            return new BaseResponse(false, errorMsg, null);
        }
    }
}
