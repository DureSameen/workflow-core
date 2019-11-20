  
namespace WorkflowCore.Interface
{
   public interface IDataStoreGlobalConfiguration
    {
        string BaseUrl { get; set; }
        IDataStoreSecurityDefinition SecurityDefinitions { get; set; }
          
    }  
}
