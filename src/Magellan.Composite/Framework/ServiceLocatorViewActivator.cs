using System;
using Magellan;
using Magellan.Framework;
using Microsoft.Practices.ServiceLocation;

namespace Magellan.Composite.Framework
{
    /// <summary>
    /// A <see cref="IViewActivator"/> that uses the common service locator for object instantiation.
    /// </summary>
    public class ServiceLocatorViewActivator : IViewActivator
    {
        private readonly IServiceLocator _serviceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorViewActivator"/> class.
        /// </summary>
        public ServiceLocatorViewActivator() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorViewActivator"/> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public ServiceLocatorViewActivator(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator ?? ServiceLocator.Current;
        }

        /// <summary>
        /// Resolves an instance of the specified view type.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <returns>An instance of the view.</returns>
        public object Instantiate(Type viewType)
        {
            return _serviceLocator.GetInstance(viewType);
        }
    }
}
