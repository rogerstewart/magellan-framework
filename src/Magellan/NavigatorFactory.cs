using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using Magellan.Abstractions;
using Magellan.Exceptions;
using Magellan.Progress;
using Magellan.Routing;
using Magellan.Utilities;
using Magellan.Views;

namespace Magellan
{
    /// <summary>
    /// The core navigation management object in Magellan. Navigation factories produce navigators, which are bound to a frame or other control.
    /// </summary>
    public class NavigatorFactory : INavigatorFactory
    {
        private readonly string _uriScheme;
        private readonly NavigationProgressListenerCollection _navigationProgressListeners = new NavigationProgressListenerCollection();
        private readonly IRouteResolver _routes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatorFactory"/> class.
        /// </summary>
        /// <param name="routeCatalogs">The route catalogs that will provide routes for this navigator. You
        /// can use ControllerRouteCatalog for Model-View-Controller, or TODO for
        /// Model-View-ViewModel.</param>
        public NavigatorFactory(params RouteCatalog[] routeCatalogs)
            : this(null, routeCatalogs)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatorFactory"/> class.
        /// </summary>
        /// <param name="uriScheme">The URI scheme that will prefix handled URI's.</param>
        /// <param name="routeCatalogs">The route catalogs that will provide routes for this navigator. You
        /// can use a ControllerRouteCatalog for Model-View-Controller, or TODO for
        /// Model-View-ViewModel.</param>
        public NavigatorFactory(string uriScheme, params RouteCatalog[] routeCatalogs)
            : this(uriScheme, new RouteResolver(routeCatalogs))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatorFactory"/> class.
        /// </summary>
        /// <param name="routes">The routes that navigators created from this factory will use when resolving requests.</param>
        public NavigatorFactory(IRouteResolver routes) 
            : this(null, routes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatorFactory"/> class.
        /// </summary>
        /// <param name="uriScheme">The URI scheme that will prefix handled URI's.</param>
        /// <param name="routes">The routes that navigators created from this factory will use when resolving requests.</param>
        public NavigatorFactory(string uriScheme, IRouteResolver routes)
        {
            uriScheme = uriScheme ?? "magellan";

            Guard.ArgumentNotNullOrEmpty(uriScheme, "uriScheme");
            Guard.ArgumentNotNull(routes, "routes");

            if (!Uri.CheckSchemeName(uriScheme))
            {
                throw new ArgumentException(string.Format("The scheme '{0}' is not a valid URI scheme.", uriScheme));
            }

            _uriScheme = uriScheme;
            _routes = routes;
        }

        /// <summary>
        /// Gets a collection of objects that will be notified as the navigation infrastructure raises navigation events.
        /// </summary>
        /// <value>The progress listeners.</value>
        public NavigationProgressListenerCollection ProgressListeners
        {
            get { return _navigationProgressListeners; }
        }

        /// <summary>
        /// Creates an <see cref="INavigator"/> bound to the specified navigation service. This method can
        /// be called multiple times for the same <paramref name="navigationService"/>.
        /// </summary>
        /// <param name="navigationService">The navigation service which will be used if the view renders
        /// page information.</param>
        /// <returns>
        /// An instance of the <see cref="INavigator"/> interface which can be used for navigation.
        /// </returns>
        public INavigator CreateNavigator(INavigationService navigationService)
        {
            Guard.ArgumentNotNull(navigationService, "navigationService");

            return CreateNavigator(() => navigationService);
        }

        /// <summary>
        /// Creates an <see cref="INavigator"/> bound to the specified navigation service. This method can
        /// be called multiple times for the same <paramref name="navigationService"/>.
        /// </summary>
        /// <param name="navigationService">The navigation service which will be used if the view renders
        /// page information.</param>
        /// <returns>
        /// An instance of the <see cref="INavigator"/> interface which can be used for navigation.
        /// </returns>
        public INavigator CreateNavigator(NavigationService navigationService)
        {
            Guard.ArgumentNotNull(navigationService, "navigationService");

            return CreateNavigator(() => new FrameNavigationServiceWrapper(System.Windows.Threading.Dispatcher.CurrentDispatcher, navigationService));
        }

        /// <summary>
        /// Creates an <see cref="INavigator"/> bound to the specified frame. This method can
        /// be called multiple times for the same <paramref name="frame"/>.
        /// </summary>
        /// <param name="frame">The navigation service which will be used if the view renders page information.</param>
        /// <returns>
        /// An instance of the <see cref="INavigator"/> interface which can be used for navigation.
        /// </returns>
        public INavigator CreateNavigator(Frame frame)
        {
            Guard.ArgumentNotNull(frame, "frame");

            return CreateNavigator(() => new FrameNavigationServiceWrapper(frame.Dispatcher, frame.NavigationService));
        }

        /// <summary>
        /// Creates an <see cref="INavigator"/> bound to the specified frame. This method can
        /// be called multiple times for the same <paramref name="frame"/>.
        /// </summary>
        /// <param name="frame">The navigation service which will be used if the view renders page information.</param>
        /// <returns>
        /// An instance of the <see cref="INavigator"/> interface which can be used for navigation.
        /// </returns>
        public INavigator CreateNavigator(ContentControl frame)
        {
            Guard.ArgumentNotNull(frame, "frame");
            if (frame is ButtonBase || frame is Label)
            {
                throw new ArgumentException("An attempt was made to create an INavigator for a '{0}', which should not be used as navigation containers. If the intention was to find a navigator that owns the UI element, the NavigatorFactory.GetOwningNavigator method should be used instead.");
            }

            if (frame is Frame)
            {
                return CreateNavigator((Frame) frame);
            }

            return CreateNavigator(() => new ContentNavigationServiceWrapper(frame));
        }

        /// <summary>
        /// Creates an <see cref="INavigator"/> bound to the navigation service that owns a given source
        /// element. This method can  be called multiple times for the same <paramref name="sourceElement"/>.
        /// </summary>
        /// <param name="sourceElement">A UI element that lives inside the frame that you want a navigator
        /// for.</param>
        /// <returns>
        /// An instance of the <see cref="INavigator"/> interface which can be used for navigation.
        /// </returns>
        public INavigator GetOwningNavigator(UIElement sourceElement)
        {
            Guard.ArgumentNotNull(sourceElement, "sourceElement");

            var existingNavigator = NavigationProperties.GetNavigator(sourceElement);
            if (existingNavigator != null)
            {
                return existingNavigator;
            }

            Func<INavigationService> lazyFrameGetter = () =>
            {
                var realService = NavigationService.GetNavigationService(sourceElement);
                if (realService == null)
                {
                    throw new ImpossibleNavigationRequestException("The Navigator for this UI element is not avaialable. Please ensure the current view has been loaded into a frame, or that the NavigationProperties.Navigator attached property has been set.");
                }
                var wrapper = new FrameNavigationServiceWrapper(sourceElement.Dispatcher, realService);
                return wrapper;
            };

            return CreateNavigator(lazyFrameGetter);
        }

        private INavigator CreateNavigator(Func<INavigationService> navigationService)
        {
            return new Navigator(this, _uriScheme, _routes, navigationService);
        }
    }
}