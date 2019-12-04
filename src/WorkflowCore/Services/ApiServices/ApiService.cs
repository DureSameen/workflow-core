using IdentityModel.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<dynamic> RunTask(string id,string accessToken)
        {
            try
            {
                var activity = _dataStore.Activities.FirstOrDefault(a => a.Id == id);
                var apiDetails = _dataStore.ApiDetails.FirstOrDefault(api => api.Key == activity?.ApiKey);
                var baseUrl = apiDetails?.BaseUrl + activity?.Path;
               

                switch (activity?.HttpMethod)

                {
                    case "Get":
                        using (HttpClient client = new HttpClient())
                        { 
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
