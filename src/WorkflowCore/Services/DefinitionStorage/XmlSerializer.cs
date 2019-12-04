using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using WorkflowCore.Models.DefinitionStorage.v1;
using WorkflowCore.Models.Steps;

namespace WorkflowCore.Services.DefinitionStorage
{
   public class XmlSerializer
    {


        public DefinitionSourceV1 DeserializeInto(string source, DefinitionSourceV1 definition)
        {
            try
            {
                var document = XDocument.Parse(source);
                // select the process part of the xml, we are not interested in the diagram part
                var elements = document.Root?.Elements().FirstOrDefault(e => e.Name.LocalName.Equals("process"))?.Descendants();
                var userContextElement =
                    document.Root?.Elements().FirstOrDefault(e => e.Name.LocalName.Equals("userContext"));
                // loop through all the elements and if we encounter something we know, we parse it
                if (elements != null)
                    foreach (var element in elements)
                    {
                        var taskName = element.Attribute("name")?.Value;
                        var accessToken= userContextElement?.Attribute("accessToken")?.Value;
                       
                        switch (element.Name.LocalName)
                        {
                            case "startEvent":
                                definition.Steps.Add(AddTask( typeof(StartTask)  , taskName, accessToken));
                                break;
                            case "endEvent":
                                definition.Steps.Add(AddTask(typeof(StopTask) , taskName, accessToken));
                                break;
                            case "userTask":
                                definition.Steps.Add(AddTask(typeof(UserTask) , taskName,  accessToken));
                                break;
                            case "sendTask":
                                definition.Steps.Add(AddTask(typeof(SendTask) , taskName, accessToken));
                                break;
                        }
                    }
            }
            catch (Exception e)
            {
                
            }

            return definition;
        }

        private StepSourceV1 AddTask(Type stepType, string name, string accessToken)
        {
            var step = new StepSourceV1();
            
            var assemblyName = stepType.GetTypeInfo().Assembly.GetName().Name;
            var typeName = stepType.Name ;
            var baseName = (stepType.Namespace + "." + typeName).Substring(assemblyName.Length).Trim('.');
            step.StepType = assemblyName +"."+ baseName + ", " + assemblyName;
            step.Name = name;
            step.AccessToken = accessToken;
            
            return step;
        }
    }

}
