using WorkflowCore.Interface;

namespace WorkflowCore.Services.DefaultDataStore
{
    public class DataStoreSecurityDefinition: IDataStoreSecurityDefinition
    {
        public string Flow { get; set; }
        public string AuthorizationUrl { get; set; }
        public string TokenUrl { get; set; }
        public string Scopes { get; set; }
        public string Type { get; set; }
    }
}
