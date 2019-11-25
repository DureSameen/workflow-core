using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Services.DefaultDataStore;

namespace WorkflowCore.Interface
{
   public interface IDataStore
    { 
            IList<DataStoreActivity> Activities { get; set; }
            DataStoreGlobalConfiguration GlobalConfiguration { get; set; }
    }
}
