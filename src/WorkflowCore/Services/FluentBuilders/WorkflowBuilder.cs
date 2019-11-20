using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace WorkflowCore.Services
{
    public class WorkflowBuilder : IWorkflowBuilder
    {
        protected List<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();

        protected WorkflowErrorHandling DefaultErrorBehavior = WorkflowErrorHandling.Retry;

        protected TimeSpan? DefaultErrorRetryInterval;

        public int LastStep => Steps.Max(x => x.Id);

        public IWorkflowBuilder<T> UseData<T>()
        {
            IWorkflowBuilder<T> result = new WorkflowBuilder<T>(Steps);
            return result;
        }

        //public virtual WorkflowDefinition BuildBpmn(string xmlPath, string id, int version)
        //{

        //    var document = XDocument.Parse(xmlPath);
        //    var elements = document.Root?.Elements().FirstOrDefault(e => e.Name.LocalName.Equals("process"))
        //        ?.Descendants();

        //    if (elements != null)
        //        foreach (var element in elements)
        //        {
        //            switch (element.Name.LocalName)
        //            {
        //                case "startEvent":
        //                    Steps.Add(AddStartEvent(document, element));
        //                    break;
        //                case "endEvent":
        //                    Steps.Add(AddStopEvent(document, element));
        //                    break;
        //                case "userTask":
        //                    Steps.Add(AddUserTask(document, element));
        //                    break;
        //                case "sendTask":
        //                    Steps.Add(AddSendTask(document, element));
        //                    break;
        //            }
        //        }

        //    return Build(id, version);
        //}

        public virtual WorkflowDefinition Build(string id, int version)
        {
            AttachExternalIds();
            return new WorkflowDefinition
            {
                Id = id,
                Version = version,
                Steps = new WorkflowStepCollection(Steps),
                DefaultErrorBehavior = DefaultErrorBehavior,
                DefaultErrorRetryInterval = DefaultErrorRetryInterval
            };
        }

        public void AddStep(WorkflowStep step)
        {
            step.Id = Steps.Count();
            Steps.Add(step);
        }

        private void AttachExternalIds()
        {
            foreach (var step in Steps)
            {
                foreach (var outcome in step.Outcomes.Where(x => !string.IsNullOrEmpty(x.ExternalNextStepId)))
                {
                    if (Steps.All(x => x.ExternalId != outcome.ExternalNextStepId))
                        throw new KeyNotFoundException($"Cannot find step id {outcome.ExternalNextStepId}");

                    outcome.NextStep = Steps.Single(x => x.ExternalId == outcome.ExternalNextStepId).Id;
                }
            }
        }

    }

    public class WorkflowBuilder<TData> : WorkflowBuilder, IWorkflowBuilder<TData>
    {

        public override WorkflowDefinition Build(string id, int version)
        {
            var result = base.Build(id, version);
            result.DataType = typeof(TData);
            return result;
        }

        public WorkflowBuilder(IEnumerable<WorkflowStep> steps)
        {
            this.Steps.AddRange(steps);
        }

        public IStepBuilder<TData, TStep> StartWith<TStep>(Action<IStepBuilder<TData, TStep>> stepSetup = null)
            where TStep : IStepBody
        {
            WorkflowStep<TStep> step = new WorkflowStep<TStep>();
            var stepBuilder = new StepBuilder<TData, TStep>(this, step);

            if (stepSetup != null)
            {
                stepSetup.Invoke(stepBuilder);
            }

            step.Name = step.Name ?? typeof(TStep).Name;
            AddStep(step);
            return stepBuilder;
        }

        public IStepBuilder<TData, InlineStepBody> StartWith(Func<IStepExecutionContext, ExecutionResult> body)
        {
            WorkflowStepInline newStep = new WorkflowStepInline();
            newStep.Body = body;
            var stepBuilder = new StepBuilder<TData, InlineStepBody>(this, newStep);
            AddStep(newStep);
            return stepBuilder;
        }

        public IStepBuilder<TData, ActionStepBody> StartWith(Action<IStepExecutionContext> body)
        {
            var newStep = new WorkflowStep<ActionStepBody>();
            AddStep(newStep);
            var stepBuilder = new StepBuilder<TData, ActionStepBody>(this, newStep);
            stepBuilder.Input(x => x.Body, x => body);
            return stepBuilder;
        }

        public IEnumerable<WorkflowStep> GetUpstreamSteps(int id)
        {
            return Steps.Where(x => x.Outcomes.Any(y => y.NextStep == id)).ToList();
        }

        public IWorkflowBuilder<TData> UseDefaultErrorBehavior(WorkflowErrorHandling behavior,
            TimeSpan? retryInterval = null)
        {
            DefaultErrorBehavior = behavior;
            DefaultErrorRetryInterval = retryInterval;
            return this;
        }

        //#region Private Methods

        //private WorkflowStep AddStartEvent(XDocument document, XElement currentElement)
        //{
        //    // the start element is always the start step, with start as the only pre condition
        //    var step = new WorkflowStep
        //    {
        //        Description = SystemStatusDefinitions.Start.ToString(),
        //        WorkflowStepHandler = _store.WorkflowStepHandlers.SingleOrDefault(e => e.Id == StartStep.Identifier),
        //    };
        //    step.Statuses.Add(new WorkflowStepStatus
        //    {
        //        IsPreOrPost = WorkflowStepStatusType.Pre,
        //        StatusDefinition = _store.StatusDefinitions.SingleOrDefault(e =>
        //            e.Description.Equals(SystemStatusDefinitions.Start.ToString()))
        //    });
        //    AddWorkflowStepStatuses(step, document, currentElement);
        //    return step;
        //}

        //private WorkflowStep AddStopEvent(XDocument document, XElement currentElement)
        //{
        //    // the stop element is always the stop step, with stop as the only post condition
        //    var step = new WorkflowStep
        //    {
        //        Description = SystemStatusDefinitions.Stop.ToString(),
        //        WorkflowStepHandler = _store.WorkflowStepHandlers.SingleOrDefault(e => e.Id == StopStep.Identifier),
        //    };
        //    step.Statuses.Add(new WorkflowStepStatus
        //    {
        //        IsPreOrPost = WorkflowStepStatusType.Post,
        //        StatusDefinition = _store.StatusDefinitions.SingleOrDefault(e =>
        //            e.Description.Equals(SystemStatusDefinitions.Stop.ToString()))
        //    });
        //    AddWorkflowStepStatuses(step, document, currentElement);
        //    return step;
        //}

        //private WorkflowStep AddUserTask(XDocument document, XElement currentElement)
        //{
        //    var description = currentElement.Attribute(DescriptionSelector)?.Value;
        //    var stepTypeId = currentElement.Attribute(XName.Get(StepTypeSelector, IDwareNamespace))?.Value;
        //    var viewTypeId = currentElement.Attribute(XName.Get(ViewTypeSelector, IDwareNamespace))?.Value;

        //    var step = new WorkflowStep
        //    {
        //        Description = description,
        //        WorkflowStepHandler = _store.WorkflowStepHandlers.SingleOrDefault(e => e.Id == Guid.Parse(stepTypeId)),
        //        View = viewTypeId != null
        //            ? _store.WorkflowStepViews.SingleOrDefault(e => e.Id == long.Parse(viewTypeId))
        //            : null,
        //        GeneratesTask = true
        //    };
        //    AddWorkflowStepStatuses(step, document, currentElement);
        //    return step;
        //}

        //private WorkflowStep AddSendTask(XDocument document, XElement currentElement)
        //{
        //    var description = currentElement.Attribute(DescriptionSelector)?.Value;
        //    var roleId = currentElement.Attribute(XName.Get(RoleSelector, IDwareNamespace))?.Value;
        //    var mailTemplateId = currentElement.Attribute(XName.Get(MailTemplateSelector, IDwareNamespace))?.Value;

        //    var step = new WorkflowStep
        //    {
        //        Description = description,
        //        Role = _store.Roles.SingleOrDefault(e => e.Id == long.Parse(roleId)),
        //        WorkflowStepHandler =
        //            _store.WorkflowStepHandlers.SingleOrDefault(e => e.Id == SendEmailStep.Identifier),
        //        EmailTemplate = _store.EmailTemplates.SingleOrDefault(e => e.Id == long.Parse(mailTemplateId))
        //    };
        //    AddWorkflowStepStatuses(step, document, currentElement);
        //    AddCcRoleToStep(step, currentElement.Attribute(XName.Get(CcRole01Selector, IDwareNamespace))?.Value);
        //    AddCcRoleToStep(step, currentElement.Attribute(XName.Get(CcRole02Selector, IDwareNamespace))?.Value);
        //    AddCcRoleToStep(step, currentElement.Attribute(XName.Get(CcRole03Selector, IDwareNamespace))?.Value);

        //    return step;
        //}

        ////private void AddWorkflowStepStatuses(WorkflowStep step, XDocument document, XElement currentElement)
        ////{
        ////    var transitions = currentElement.Descendants()
        ////        .Where(e => e.Name.LocalName.Equals(IncomingSelector) ||
        ////                    e.Name.LocalName.Equals(OutgoingSelector))
        ////        .ToList();

        ////    foreach (var transition in transitions)
        ////    {
        ////        // ReSharper disable once PossibleNullReferenceException
        ////        var sequence = document.Descendants().First(d => d.HasAttributes && d.Attribute(IdSelector) != null && d.Attribute(IdSelector).Value.Equals(transition.Value));
        ////        var statusDefinition = sequence?.Attribute(XName.Get(StatusDefinitionSelector, IDwareNamespace))?.Value;
        ////        if (long.TryParse(statusDefinition, out long statusDefinitionId) && step.Statuses.All(s => s.StatusDefinitionId != statusDefinitionId))
        ////        {
        ////            step.Statuses.Add(new WorkflowStepStatus
        ////            {
        ////                IsPreOrPost = transition.Name.LocalName.Equals(IncomingSelector) ? WorkflowStepStatusType.Pre : WorkflowStepStatusType.Post,
        ////                StatusDefinitionId = statusDefinitionId
        ////            });
        ////        }
        ////    }
        ////}

        ////private void AddCcRoleToStep(WorkflowStep step, string roleId)
        ////{
        ////    if (string.IsNullOrEmpty(roleId))
        ////    {
        ////        return;
        ////    }

        ////    step.CcRoles.Add(new WorkflowStepRole
        ////    {
        ////        RoleId = long.Parse(roleId)
        ////    });

        ////}

        //#endregion
    }
}