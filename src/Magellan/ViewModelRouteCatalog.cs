using Magellan.Framework;
using Magellan.Routing;
using Magellan.Utilities;

namespace Magellan
{
    /// <summary>
    /// A <see cref="RouteCatalog"/> for registering routes that will be served using Magellan's 
    /// Model-View-Controller support.
    /// </summary>
    public class ViewModelRouteCatalog : RouteCatalog
    {
        private readonly IRouteValidator _validator = new ViewModelRouteValidator();
        private readonly IRouteHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRouteCatalog"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ViewModelRouteCatalog(IViewModelFactory factory)
        {
            Guard.ArgumentNotNull(factory, "factory");
            _handler = new ViewModelRouteHandler(factory);
        }

        /// <summary>
        /// Registers a route for the given route specification.
        /// </summary>
        /// <param name="routeSpecification">The route specification that describes the pattern of the route 
        /// - for example, "{controller}/{action}".</param>
        /// <returns>The current <see cref="ViewModelRouteCatalog"/>, to allow method chaining.</returns>
        public ViewModelRouteCatalog MapRoute(string routeSpecification)
        {
            return MapRoute(routeSpecification, null);
        }

        /// <summary>
        /// Registers a route for the given route specification.
        /// </summary>
        /// <param name="routeSpecification">The route specification that describes the pattern of the route
        /// - for example, "{controller}/{action}".</param>
        /// <param name="defaults">Default values for the route, e.g., <c>new { controller = "Sample" }</c></param>
        /// <returns>
        /// The current <see cref="ViewModelRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ViewModelRouteCatalog MapRoute(string routeSpecification, object defaults)
        {
            return MapRoute(routeSpecification, defaults, null);
        }

        /// <summary>
        /// Registers a route for the given route specification.
        /// </summary>
        /// <param name="routeSpecification">The route specification that describes the pattern of the route
        /// - for example, "{controller}/{action}".</param>
        /// <param name="defaults">Default values for the route, e.g., <c>new { controller = "Sample" }</c></param>
        /// <param name="constraints">Constraints for the route, e.g., <c>new { id = "^[0-9]+$" }</c></param>
        /// <returns>
        /// The current <see cref="ViewModelRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ViewModelRouteCatalog MapRoute(string routeSpecification, object defaults, object constraints)
        {
            return MapRoute(routeSpecification, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints));
        }

        /// <summary>
        /// Registers a route for the given route specification.
        /// </summary>
        /// <param name="routeSpecification">The route specification that describes the pattern of the route
        /// - for example, "{controller}/{action}".</param>
        /// <param name="defaults">Default values for the route, e.g., <c>new RouteValueDictionary(new { controller = "Sample" })</c></param>
        /// <returns>
        /// The current <see cref="ViewModelRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ViewModelRouteCatalog MapRoute(string routeSpecification, RouteValueDictionary defaults)
        {
            return MapRoute(routeSpecification, defaults, null);
        }

        /// <summary>
        /// Registers a route for the given route specification.
        /// </summary>
        /// <param name="routeSpecification">The route specification that describes the pattern of the route
        /// - for example, "{controller}/{action}".</param>
        /// <param name="defaults">Default values for the route, e.g., <c>new RouteValueDictionary(new { controller = "Sample" })</c></param>
        /// <param name="constraints">Constraints for the route, e.g., <c>new RouteValueDictionary(new { id = "^[0-9]+$" })</c></param>
        /// <returns>
        /// The current <see cref="ViewModelRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ViewModelRouteCatalog MapRoute(string routeSpecification, RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            defaults = defaults ?? new RouteValueDictionary();

            Add(new Route(routeSpecification, () => _handler, defaults, constraints, _validator));
            return this;
        }
    }
}