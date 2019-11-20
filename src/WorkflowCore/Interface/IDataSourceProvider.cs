using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCore.Interface
{
    public interface IDataStoreProvider
    {
        IDataStore DataStore { get; }
    }
}
