using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCore.Interface
{
    
        public enum TaskOrEventType
        {

            StartEvent,
            EndEvent,
            UserTask,
            SendTask
        }

        public enum ActivityType
        {

            PostCondition,
            PreCondition,
            Activity
        }
     
}
