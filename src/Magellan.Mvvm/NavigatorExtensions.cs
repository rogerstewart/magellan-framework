using Magellan.Routing;

namespace Magellan
{
    /// <summary>
    /// Extension methods for the <see cref="INavigator"/> interface that help with MVVM navigation 
    /// scenarios.
    /// </summary>
    public static class NavigatorExtensions
    {
        /// <summary>
        /// Resolves and navigates to the first route that matches the route values for the given view model.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigator">The navigator.</param>
        public static void Navigate<TViewModel>(this INavigator navigator)
        {
            Navigate<TViewModel>(navigator, null);
        }

        /// <summary>
        /// Resolves and navigates to the first route that matches the route values for the given view model.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigator">The navigator.</param>
        /// <param name="parameters">The parameters.</param>
        public static void Navigate<TViewModel>(this INavigator navigator, object parameters)
        {
            var routeValues = new RouteValueDictionary(parameters);
            routeValues["viewModel"] = typeof (TViewModel).Name.Replace("ViewModel", "");

            navigator.ProcessRequest(new NavigationRequest(routeValues));
        }
    }
}
