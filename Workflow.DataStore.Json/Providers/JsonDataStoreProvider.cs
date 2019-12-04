﻿using Newtonsoft.Json;
using System.IO;
using WorkflowCore.Interface;

namespace Workflow.DataStore.Json.Providers
{
    public class JsonDataStoreProvider:IDataStoreProvider
    {
        public IDataStore DataStore { get; }
        public JsonDataStoreProvider(string path)
        {
           
           using (var r = new StreamReader( path))
           {
               var json = r.ReadToEnd();
               DataStore = JsonConvert.DeserializeObject<WorkflowCore.Services.DefaultDataStore.DataStore>(json);

           }

          
        }
    }
}