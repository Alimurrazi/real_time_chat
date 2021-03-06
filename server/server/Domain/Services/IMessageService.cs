﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Responses;
using server.Domain.Models;

namespace server.Domain.Services
{
    public interface IMessageService
    {
       Task<BaseResponse> SendMessageAsync(Message message);
    }
}