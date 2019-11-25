using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace WorkflowCore.Services.DefaultDataStore
{
    public class DataStore: IDataStore
    {
        public DataStore(IDataStoreProvider dataStoreProvider)
        {
            Activities = dataStoreProvider.DataStore.Activities;
            GlobalConfiguration = dataStoreProvider.DataStore.GlobalConfiguration;
            ApiDetails = dataStoreProvider.DataStore.ApiDetails;
        }

        public DataStore()
        {
        }
        public IList<DataStoreActivity> Activities { get; set; }
        public IList<ApiDetail> ApiDetails { get; set; }
        public DataStoreGlobalConfiguration GlobalConfiguration { get; set; }
         
    }
}
