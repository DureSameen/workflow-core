using Newtonsoft.Json;
using System;

namespace Workflow.DataStore.Json
{
    public static class ExtensionMethods
    {
        private static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        public static void AddJsonFile(this IServiceProvider provider, string path)
        {
             
        }


    }
}
