using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCore.Interface
{
    public interface IDataStoreSecurityDefinition 
    {
        string Flow { get; set; }
        string AuthorizationUrl { get; set; }
        string TokenUrl { get; set; }
        string Scopes { get; set; }
        string Type { get; set; }
    }
}
