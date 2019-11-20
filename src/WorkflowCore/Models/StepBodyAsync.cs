using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace WorkflowCore.Models
{
    public abstract class StepBodyAsync : IStepBody
    {
        //public string Id { get; set; }
        //public Uri Path { get; set; }
        //public string Version { get; set; }
        //public IList<IStepParameter> Parameters { get; set; }
        //public ActivityType ActivityType { get; set; }
        //public TaskOrEventType TaskOrEventType { get; set; }
        //public string NextStepId { get; set; }
        public abstract Task<ExecutionResult> RunAsync(IStepExecutionContext context);
    }
}
