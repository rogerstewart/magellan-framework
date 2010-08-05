using System;
using Magellan.Mvc;
using System.Threading;

namespace iPhone.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RoleFilterAttribute : Attribute, IActionFilter
    {
        private readonly string _roleName;
        private IMessageService _messageService;

        public RoleFilterAttribute(string roleName)
        {
            _roleName = roleName;
        }

        public string RoleName
        {
            get { return _roleName; }
        }

        public IMessageService MessageService
        {
            get { return _messageService = _messageService ?? new MessageService(); }
            set { _messageService = value; }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!Thread.CurrentPrincipal.IsInRole(RoleName))
            {
                MessageService.ShowInformation(string.Format("This action is only available to users in the {0} group. Try elevating before starting the application.", RoleName));
                context.OverrideResult = new DoNothingResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
