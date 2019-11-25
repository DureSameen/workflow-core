using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using CheeseCake.SharedKernel.Common.API;
using Newtonsoft.Json;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.ApiServices;

namespace WorkflowCore.API.Steps
{
    public class  UsersListStep : StepBody
    {
        private readonly IApiService  _myService;
        private ILogger _logger;

        
        public UsersListStep(IApiService myService,ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UsersListStep>();
            _myService = myService;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var listJson = _myService.RunTask("UsersList").Result ;
            var users= JsonConvert.DeserializeObject<List<dynamic>>(listJson);
            Console.WriteLine($"Users List :\r\n{users}");
            _logger.LogInformation("Hi there!");
            return ExecutionResult.Outcome(users);
        }
    }
}
