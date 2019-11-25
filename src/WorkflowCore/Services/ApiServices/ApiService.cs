using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using WorkflowCore.Interface;

namespace WorkflowCore.Services.ApiServices
{
    public class ApiService : IApiService
    {
        private readonly IAuthorizationService _authorization;
        private readonly IDataStore _dataStore;
        public ApiService(IDataStore dataStore, IAuthorizationService authorization)
        {
            _authorization = authorization;
            _dataStore = dataStore;

        }

        public async Task<dynamic> RunTask(string id)
        {
            try
            {
                var activity = _dataStore.Activities.FirstOrDefault(a => a.Id == id);
                var baseUrl = _dataStore.GlobalConfiguration.BaseUrl + activity?.Path;


                switch (activity?.HttpMethod)

                {
                    case "Get":
                        using (HttpClient client = new HttpClient())
                        {
                            string accessToken = await _authorization.GetAccessToken(client,
                                _dataStore.GlobalConfiguration.SecurityDefinitions.TokenUrl);
                            client.SetBearerToken(accessToken);
                            var response = await client.GetAsync(baseUrl);
                            response.EnsureSuccessStatusCode();
                            var result = response.Content.ReadAsStringAsync().Result;
                            return result;
                        }


                }
                return null;
            }
            catch (Exception exp)
            {
                return null;
            }

        }

    }
}
