using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Models;

namespace WorkflowCore.Interface
{
    public interface IStepBody
    {
        //string Id { get; set; }
        //Uri Path { get; set; }
        //string Version { get; set; }
        //IList<IStepParameter> Parameters { get; set; }
        //ActivityType ActivityType { get; set; }
        //TaskOrEventType TaskOrEventType { get; set; }
        //string NextStepId { get; set; }
        Task<ExecutionResult> RunAsync(IStepExecutionContext context);        
    }
}
