using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace WorkflowCore.Services.DefaultDataStore
{
    public class DataStoreActivity: IDataStoreActivity
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string Scheme { get; set; }
        public string HttpMethod { get; set; }
        public string AuthenticationOptionsScope { get; set; }
    }
}
