using System;
using System.Linq;

namespace WorkflowCore.Services.DefaultDataStore
{
    public class ApiDetail
    {
        public string Key { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string ClientSecret { get; set; }
        public string BaseUrl { get; set; }
    }
}
