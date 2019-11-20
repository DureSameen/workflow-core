using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace WorkflowCore.Services.DefaultDataStore
{
    public class DataStoreGlobalConfiguration: IDataStoreGlobalConfiguration
    {
        public string BaseUrl { get; set; }
        public IDataStoreSecurityDefinition SecurityDefinitions { get; set; }
    }
}
