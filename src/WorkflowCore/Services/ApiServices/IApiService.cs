﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WorkflowCore.Services.ApiServices
{
    public interface IApiService
    {
        Task<dynamic> Get();
 
    }
}
