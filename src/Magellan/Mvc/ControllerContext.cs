using System;
using Magellan.Routing;

namespace Magellan.Mvc
{
    /// <summary>
    /// Provides information about the current request and the controller that is serving it.
    /// </summary>
    public class ControllerContext : IDisposable
    {
        private readonly IController _controller;
        private readonly ResolvedNavigationRequest _request;
        private readonly ViewEngineCollection _viewEngines;
        private readonly ModelBinderDictionary _modelBinders;
        private readonly Action _releaseCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerContext"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="request">The request.</param>
        /// <param name="viewEngines">The view engines.</param>
        public ControllerContext(IController controller, ResolvedNavigationRequest request, ViewEngineCollection viewEngines)
            : this(controller, request, viewEngines, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerContext"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="request">The request.</param>
        /// <param name="viewEngines">The view engines.</param>
        /// <param name="modelBinders"></param>
        /// <param name="releaseCallback">A callback that is invoked when the current request needs to be 
        /// cleaned up.</param>
        public ControllerContext(IController controller, ResolvedNavigationRequest request, ViewEngineCollection viewEngines, ModelBinderDictionary modelBinders, Action releaseCallback)
        {
            _controller = controller;
            _request = request;
            _viewEngines = viewEngines;
            _modelBinders = modelBinders;
            _releaseCallback = releaseCallback;
        }

        /// <summary>
        /// Gets the active controller.
        /// </summary>
        /// <value>The controller.</value>
        public IController Controller
        {
            get { return _controller; }
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <value>The name of the controller.</value>
        public string ControllerName
        {
            get { return Request.RouteValues.GetOrDefault<string>("controller"); }
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <value>The name of the action.</value>
        public string ActionName
        {
            get { return Request.RouteValues.GetOrDefault<string>("action"); }
        }

        /// <summary>
        /// Gets the current request.
        /// </summary>
        /// <value>The request.</value>
        public ResolvedNavigationRequest Request
        {
            get { return _request; }
        }

        /// <summary>
        /// Gets the view engines.
        /// </summary>
        /// <value>The view engines.</value>
        public ViewEngineCollection ViewEngines
        {
            get { return _viewEngines; }
        }

        /// <summary>
        /// Cleans up and releases the current controller context.
        /// </summary>
        public virtual void Dispose()
        {
            if (_releaseCallback != null) _releaseCallback();
        }
    }
}