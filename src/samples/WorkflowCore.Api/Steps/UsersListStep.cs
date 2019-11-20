using System;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.ApiServices;

namespace WorkflowCore.API.Steps
{
    public class  UsersListStep : StepBody
    {
        private readonly IApiService  _myService;

        public UsersListStep(IApiService myService)
        {
            _myService = myService;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var list = _myService.Get();
            return ExecutionResult.Next();
        }
    }
}
