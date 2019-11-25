  
using WorkflowCore.Services.DefaultDataStore;

namespace WorkflowCore.Interface
{
   public interface IDataStoreGlobalConfiguration
    {
        string BaseUrl { get; set; }
        DataStoreSecurityDefinition SecurityDefinitions { get; set; }
          
    }  
}
