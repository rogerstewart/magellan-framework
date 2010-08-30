using System;
using Magellan;
using Magellan.Framework;
using Magellan.Routing;
using Microsoft.Practices.ServiceLocation;

namespace Magellan.Composite.Framework
{
    /// <summary>
    /// An <see cref="IControllerFactory"/> that uses the Common Service Locator for finding controllers.
    /// </summary>
    public class ServiceLocatorControllerFactory : IControllerFactory
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly bool _isAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorControllerFactory"/> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public ServiceLocatorControllerFactory(IServiceLocator serviceLocator) : this(serviceLocator, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorControllerFactory"/> class.
        /// </summary>
        public ServiceLocatorControllerFactory()
            : this(ServiceLocator.Current, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorControllerFactory"/> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        /// <param name="isAsync">Whether or not controllers should be invoked asynchronously.</param>
        public ServiceLocatorControllerFactory(IServiceLocator serviceLocator, bool isAsync)
        {
            _serviceLocator = serviceLocator;
            _isAsync = isAsync;
        }

        /// <summary>
        /// Resolves and provides an instance of the specified controller.
        /// </summary>
        /// <param name="request">The request being served.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns></returns>
        public ControllerFactoryResult CreateController(ResolvedNavigationRequest request, string controllerName)
        {
            var controller = _serviceLocator.GetInstance<IController>(controllerName);
            if (_isAsync && controller is ControllerBase)
            {
                ((ControllerBase)controller).ActionInvoker = new AsyncActionInvoker();
            }

            return new ControllerFactoryResult(
                controller,
                () =>
                {
                    var disposableController = controller as IDisposable;
                    if (disposableController != null)
                    {
                        disposableController.Dispose();
                    }
                });
        }
    }
}
