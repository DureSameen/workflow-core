using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Services.DefaultDataStore;

namespace WorkflowCore.Services.ApiServices
{
    public class AuthorizationService : IAuthorizationService
    {
         
        public string AccessToken { get; set; }

        public async Task<string> GetAccessToken(HttpClient client, string tokenEndpoint, ApiDetail apiDetail, string  username, string password)
        {
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = tokenEndpoint, 
                ClientId = apiDetail.ClientId,
                Scope = apiDetail.Scope,
                ClientSecret = apiDetail.ClientSecret,
                UserName = username,
                Password = password
            }); 
           
             if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return response.Error;
            }
           
            return response.AccessToken ;
        }

    }
}