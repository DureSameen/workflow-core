using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workflow.DataStore.Json.Providers;
using WorkflowCore.API.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.DefinitionStorage;

namespace WorkflowCore.API
{
    public class Program
    {
        protected static IWorkflowHost Host;
        protected static IDefinitionLoader DefinitionLoader;
        protected static IPersistenceProvider PersistenceProvider;
        public static void Main(string[] args)
         {
            IServiceProvider serviceProvider = ConfigureServices();

            ////start the workflow host
            //var host = serviceProvider.GetService<IWorkflowHost>();
           /* host.RegisterWorkflow<UsersWorkflow>()*/;        
             
            PersistenceProvider = serviceProvider.GetService<IPersistenceProvider>();
            DefinitionLoader = serviceProvider.GetService<IDefinitionLoader>();
            Host = serviceProvider.GetService<IWorkflowHost>();
            Host.Start();
            string xmlString = System.IO.File.ReadAllText("userlist.bpmn");
            var workflowId=StartWorkflow(xmlString, null);
            WaitForWorkflowToComplete(workflowId, TimeSpan.FromSeconds(30));
            
            
            Console.ReadLine();
            Host.Stop();
        }
        private static string StartWorkflow(string xml, object data)
        {
            var def = DefinitionLoader.LoadDefinition(xml, Deserializers.Xml);
            var workflowId = Host.StartWorkflow(def.Id, data).Result;
            return workflowId;
        }
        private static void WaitForWorkflowToComplete(string workflowId, TimeSpan timeOut)
        {
            var status = GetStatus(workflowId);
            var counter = 0;
            while ((status == WorkflowStatus.Runnable) && (counter < (timeOut.TotalMilliseconds / 100)))
            {
                Thread.Sleep(100);
                counter++;
                status = GetStatus(workflowId);
            }
        }
        private static WorkflowStatus GetStatus(string workflowId)
        {
            var instance = PersistenceProvider.GetWorkflowInstance(workflowId).Result;
            return instance.Status;
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow(options=> options.UseDataStore(sp => new JsonDataStoreProvider("Activities.json")));
            //services.AddWorkflow(x => x.UseMongoDB(@"mongodb://localhost:27017", "workflow"));
            services.AddTransient<UsersListStep>();
            services.AddTransient<GoodbyeWorld>();
            
            var serviceProvider = services.BuildServiceProvider();

            //config logging
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();            
            loggerFactory.AddDebug();
            return serviceProvider;
        }


    }
}
