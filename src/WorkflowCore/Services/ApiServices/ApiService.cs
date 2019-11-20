using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<dynamic> Get()
        { 
            var baseUrl = _dataStore.GlobalConfiguration.BaseUrl + _dataStore.Activities.FirstOrDefault(a => a.HttpMethod =="Get")?.Path ; 

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authorization.AccessToken}"  );
                var response = await client.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }

             
        }
    }
}
