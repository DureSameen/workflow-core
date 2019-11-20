using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace WorkflowCore.Services.DefaultDataStore
{
    public class DataStore: IDataStore
    {
        public IList<IDataStoreActivity> Activities { get; set; }
        public IDataStoreGlobalConfiguration GlobalConfiguration { get; set; }
        
    }
}
