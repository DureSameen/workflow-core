using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Services.DefaultDataStore;

namespace WorkflowCore.Services.ApiServices
{
    public interface IAuthorizationService
    { 
        string AccessToken { get; set; }

        Task<string> GetAccessToken(HttpClient client, string tokenEndpoint, ApiDetail apiDetail, string username,
            string password);
    }
}
