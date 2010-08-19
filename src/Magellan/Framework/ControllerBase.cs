using Magellan.Diagnostics;
using Magellan.Routing;

namespace Magellan.Framework
{
    /// <summary>
    /// Serves as a base class for all controllers that use an action invoker for execution.
    /// </summary>
    public abstract class ControllerBase : IController
    {
        private IActionInvoker _actionInvoker;
        private ModelBinderDictionary _modelBinders;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        protected ControllerBase()
        {
        }

        /// <summary>
        /// Gets the controller context.
        /// </summary>
        /// <value>The controller context.</value>
        public ControllerContext ControllerContext { get; set; } 

        /// <summary>
        /// Gets the request context.
        /// </summary>
        public ResolvedNavigationRequest Request
        {
            get { return ControllerContext == null ? null : ControllerContext.Request; }
        }

        /// <summary>
        /// Gets or sets the action invoker that will be used when executing the request.
        /// </summary>
        public IActionInvoker ActionInvoker
        {
            get { return _actionInvoker ?? new DefaultActionInvoker(); }
            set { _actionInvoker = value; }
        }

        /// <summary>
        /// Gets or sets the model binders that are used to map navigation parameters to method parameters.
        /// </summary>
        public ModelBinderDictionary ModelBinders
        {
            get { return _modelBinders = _modelBinders ?? Framework.ModelBinders.CreateDefaults(); }
            set { _modelBinders = value; }
        }

        /// <summary>
        /// Gets or sets the view engines that will be used when resolving views.
        /// </summary>
        /// <value>The view engines.</value>
        public ViewEngineCollection ViewEngines
        {
            get { return ControllerContext == null ? Framework.ViewEngines.CreateDefaults() : ControllerContext.ViewEngines; }
        }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="context">Context information about the current navigation request.</param>
        public void Execute(ControllerContext context)
        {
            TraceSources.MagellanSource.TraceInformation("Controller '{0}' is executing request '{1}'.", GetType().FullName, context.Request);
            ControllerContext = context;
            ActionInvoker.ExecuteAction(ControllerContext, context.ActionName, ModelBinders);
        }
    }
}