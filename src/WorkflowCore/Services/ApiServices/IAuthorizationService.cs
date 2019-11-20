using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCore.Services.ApiServices
{
    public interface IAuthorizationService
    { 
        string AccessToken { get; set; }
    }
}
