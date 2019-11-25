using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowCore.Services.ApiServices
{
    public class AuthorizationService : IAuthorizationService
    {
        public string AccessToken { get; set; }

        public async Task<string> GetAccessToken(HttpClient client, string tokenEndpoint)
        {
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = tokenEndpoint, 
                ClientId = "dev-gateway-api",
                Scope = "ledger_read_only catalog_read_only user_read_only",
                ClientSecret = "secret",
                UserName = "admin1",
                Password = "password"
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