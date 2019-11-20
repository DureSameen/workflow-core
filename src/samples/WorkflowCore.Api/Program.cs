using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workflow.DataStore.Json.Providers;
using WorkflowCore.API.Steps;
using WorkflowCore.Interface;

namespace WorkflowCore.API
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            //start the workflow host
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<UsersWorkflow>();        
            host.Start();            

            host.StartWorkflow("Users");
            
            Console.ReadLine();
            host.Stop();
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
