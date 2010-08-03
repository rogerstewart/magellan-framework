using System.Windows.Interop;
using Magellan.Routing;

namespace Magellan.Abstractions
{
    public class SilverlightNavigationListener
    {
        public SilverlightNavigationListener()
        {
            
        }

        public void Connect(SilverlightHost host)
        {
            host.NavigationStateChanged += HostNavigationStateChanged;
        }

        private static void HostNavigationStateChanged(object sender, NavigationStateChangedEventArgs e)
        {
            var routes = new RouteCollection();
            var routeMatch = routes.MatchPathToRoute(e.NewNavigationState);
            var routeHandler = routeMatch.Route.CreateRouteHandler();
            routeHandler.ProcessRequest(new RouteRequest(e.NewNavigationState, routeMatch));
        }
    }
}
