using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCore.Interface
{
   public interface IDataStore
    { 
            IList<IDataStoreActivity> Activities { get; set; }
            IDataStoreGlobalConfiguration GlobalConfiguration { get; set; }
    }
}
