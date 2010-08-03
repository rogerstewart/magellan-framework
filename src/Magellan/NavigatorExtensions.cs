using System;
using Magellan.Routing;

namespace Magellan
{
    /// <summary>
    /// Extension methods for the <see cref="INavigator"/> interface that help with common navigation 
    /// scenarios.
    /// </summary>
    public static class NavigatorExtensions
    {
        /// <summary>
        /// Navigates to the specified URI, resolving the first matching route.
        /// </summary>
        /// <param name="navigator">The navigator.</param>
        /// <param name="requestUri">The request URI.</param>
        public static void Navigate(this INavigator navigator, string requestUri)
        {
            navigator.Navigate(
                requestUri.IndexOf(':') <= 0
                ? new Uri(navigator.Scheme + "://" + requestUri)
                : new Uri(requestUri));
        }

        /// <summary>
        /// Navigates to the specified URI, resolving the first matching route.
        /// </summary>
        /// <param name="navigator">The navigator.</param>
        /// <param name="requestUri">The request URI.</param>
        public static void Navigate(this INavigator navigator, Uri requestUri)
        {
            navigator.Navigate(requestUri, null);
        }

        /// <summary>
        /// Navigates to the specified URI, resolving the first matching route.
        /// </summary>
        /// <param name="navigator">The navigator.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="additionalData">Additional data (like post data) that is not in the URI but is used
        /// for navigation.</param>
        public static void Navigate(this INavigator navigator, Uri requestUri, object additionalData)
        {
            navigator.Navigate(requestUri, new RouteValueDictionary(additionalData));
        }

        /// <summary>
        /// Navigates to the specified URI, resolving the first matching route.
        /// </summary>
        /// <param name="navigator">The navigator.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="additionalData">Additional data (like post data) that is not in the URI but is used
        /// for navigation.</param>
        public static void Navigate(this INavigator navigator, Uri requestUri, RouteValueDictionary additionalData)
        {
            navigator.ProcessRequest(new NavigationRequest(requestUri, additionalData));
        }

        /// <summary>
        /// Resolves and navigates to the first route that matches the given set of route values.
        /// </summary>
        /// <param name="navigator">The navigator.</param>
        /// <param name="routeValues">The route values. For example, <code>new { controller = "MyController", action = "MyAction" }</code></param>
        public static void Navigate(this INavigator navigator, object routeValues)
        {
            navigator.Navigate(new RouteValueDictionary(routeValues));
        }

        /// <summary>
        /// Resolves and navigates to the first route that matches the given set of route values.
        /// </summary>
        /// <param name="navigator">The navigator.</param>
        /// <param name="request">The request.</param>
        public static void Navigate(this INavigator navigator, RouteValueDictionary request)
        {
            navigator.ProcessRequest(new NavigationRequest(request));
        }
    }
}
