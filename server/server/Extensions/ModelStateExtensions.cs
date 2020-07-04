using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Responses;

namespace server.Extensions
{
    public static class ModelStateExtensions
    {
        public static BaseResponse GetErrorMessages(this ModelStateDictionary modelStateDictionary)
        {
            List<string> errorMsg = new List<string>(); 
            errorMsg = modelStateDictionary.SelectMany(m => m.Value.Errors)
                .Select(m => m.ErrorMessage)
                .ToList();
            return new BaseResponse(false, errorMsg, null);
        }
    }
}
