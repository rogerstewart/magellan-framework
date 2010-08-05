using System;
using Magellan.Mvc;

namespace iPhone.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NotImplementedYetAttribute : Attribute, IActionFilter
    {
        private readonly string _message;
        private IMessageService _messageService;

        public NotImplementedYetAttribute() : this(null)
        {
        }

        public NotImplementedYetAttribute(string message)
        {
            _message = message;
            if (string.IsNullOrEmpty(_message))
            {
                _message = "This feature has not been implemented (this is just a demo after all).";
            }
        }

        public IMessageService MessageService
        {
            get { return _messageService = _messageService ?? new MessageService(); }
            set { _messageService = value; }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            MessageService.ShowInformation(_message);
            context.OverrideResult = new DoNothingResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
