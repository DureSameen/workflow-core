using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowCore.Services.ApiServices
{
    public interface IAuthorizationService
    { 
        string AccessToken { get; set; }

        Task<string> GetAccessToken(HttpClient client, string tokenEndpoint);
    }
}
