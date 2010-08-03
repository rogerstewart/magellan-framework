using System;
using System.Windows;
using Magellan.Abstractions;
using Magellan.Controls;
using Magellan.Framework;
using Magellan.Routing;

namespace Magellan
{
    public sealed class Navigator
    {
        private readonly RouteCollection _routes;
        private NavigationFrame _defaultFrame;

        public Navigator(RouteCollection routes)
        {
            _routes = routes;
            Application.Current.Host.NavigationStateChanged += HostNavigationStateChanged;
        }

        public void RegisterFrame(NavigationFrame frame)
        {
            _defaultFrame = frame;
        }

        public void Navigate(params Func<string, object>[] routeValues)
        {
            var path = _routes.MatchRouteToPath(new RouteValueDictionary(routeValues));
            Application.Current.Host.NavigationState = path.Path;
        }

        private void HostNavigationStateChanged(object sender, System.Windows.Interop.NavigationStateChangedEventArgs e)
        {
            var path = e.NewNavigationState;
            var route = _routes.MatchPathToRoute(path);
            var handler = route.Route.CreateRouteHandler();

            if (route.Values.GetOrDefault<INavigationService>(WellKnownParameters.Navigator) == null)
            {
                route.Values.Add(WellKnownParameters.Navigator, _defaultFrame);
            }

            handler.ProcessRequest(new RouteRequest(e.NewNavigationState, route));
        }
    }
}