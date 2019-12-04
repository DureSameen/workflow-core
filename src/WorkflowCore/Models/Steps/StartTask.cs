using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Services.ApiServices;

namespace WorkflowCore.Models.Steps
{
    public class StartTask : StepBody
    {
        private readonly IApiService _myService;
        private readonly ILogger _logger;
       
        public StartTask(IApiService myService,   ILoggerFactory loggerFactory)
        { 
            _logger = loggerFactory.CreateLogger<StartTask> ();
            _myService = myService;
        }

        public override   ExecutionResult Run(IStepExecutionContext context)
        {
            var list=_myService.RunTask(context.Step.Name, context.Step.AccessToken ).Result;  

            _logger.LogInformation("StartTask!");
            return ExecutionResult.Outcome(list);
        }
    }
}
