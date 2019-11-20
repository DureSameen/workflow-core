using System;
using System.Linq;
using WorkflowCore.API.Steps;
using WorkflowCore.Interface;

namespace WorkflowCore.API
{
    public class UsersWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder<object> builder)
        {
            // builder.BuildBpmn("userlist.bpmn",Id,Version);
            builder
                .StartWith<UsersListStep>()
                .Then<GoodbyeWorld>();


        }

        public string Id => "Users";
            
        public int Version => 1;
                 
    }
}
