using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCore.Interface
{
    public interface IDataStoreActivity
    { 
        string ApiKey { get; set; }
        string Id { get; set; } 
        string Path { get; set; } 
        string Scheme { get; set; }  
        string HttpMethod { get; set; } 
        string AuthenticationOptionsScope { get; set; }
            
    }
}
