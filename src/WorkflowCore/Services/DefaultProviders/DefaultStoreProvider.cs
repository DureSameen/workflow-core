using WorkflowCore.Interface;

namespace WorkflowCore.Services.DefaultProviders
{
    public class DefaultStoreProvider: IDataStoreProvider
    {
        public IDataStore DataStore { get; }
    }
}
