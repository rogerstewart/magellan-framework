using System;
using Magellan.Abstractions;
using Magellan.Progress;
using Magellan.Routing;

namespace Magellan
{
    /// <summary>
    /// Navigators provide navigation services for a specific frame of navigation. They are created using 
    /// the <see cref="NavigatorFactory"/>.
    /// </summary>
    public interface INavigator : INavigationService
    {
        /// <summary>
        /// Gets the scheme that will be used for any URI's built by this navigator.
        /// </summary>
        /// <value>The scheme.</value>
        string Scheme { get; }

        /// <summary>
        /// Gets the <see cref="INavigatorFactory"/> that produced this <see cref="INavigator"/>.
        /// </summary>
        /// <value>The factory.</value>
        INavigatorFactory Factory { get; }

        /// <summary>
        /// Gets a collection of registered progress observers.
        /// </summary>
        /// <value>The progress listeners.</value>
        NavigationProgressListenerCollection ProgressListeners { get; }

        /// <summary>
        /// Resolves and navigates to the given route request.
        /// </summary>
        /// <param name="request">The request.</param>
        void ProcessRequest(NavigationRequest request); 
    }
}