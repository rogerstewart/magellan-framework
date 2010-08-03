using Magellan;
using Magellan.Mvc;
using Magellan.Routing;
using Magellan.Utilities;

namespace Magellan
{
    /// <summary>
    /// A <see cref="RouteCatalog"/> for registering routes that will be served using Magellan's 
    /// Model-View-Controller support.
    /// </summary>
    public class ControllerRouteCatalog : RouteCatalog
    {
        private readonly IControllerFactory _controllerFactory;
        private readonly ViewEngineCollection _viewEngines;
        private readonly ModelBinderDictionary _modelBinders;
        private readonly ControllerRouteHandler _handler;
        private readonly IRouteValidator _validator = new ControllerRouteValidator();

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerRouteCatalog"/> class.
        /// </summary>
        /// <param name="controllerFactory">The controller factory.</param>
        public ControllerRouteCatalog(IControllerFactory controllerFactory) : this(controllerFactory, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerRouteCatalog"/> class.
        /// </summary>
        /// <param name="controllerFactory">The controller factory.</param>
        /// <param name="viewActivator">The view activator that is used to instantiate views.</param>
        public ControllerRouteCatalog(IControllerFactory controllerFactory, IViewActivator viewActivator)
        {
            Guard.ArgumentNotNull(controllerFactory, "controllerFactory");
            _controllerFactory = controllerFactory;
            _viewEngines = Mvc.ViewEngines.CreateDefaults(viewActivator);
            _modelBinders = Mvc.ModelBinders.CreateDefaults();
            _handler = new ControllerRouteHandler(_controllerFactory, _viewEngines, _modelBinders);
        }

        /// <summary>
        /// Gets a collection of model binders that will be used to bind input route data to action parameters.
        /// </summary>
        /// <value>The model binders.</value>
        public ModelBinderDictionary ModelBinders
        {
            get { return _modelBinders; }
        }

        /// <summary>
        /// Gets a collection of view engines that will be used to locate and render views.
        /// </summary>
        /// <value>The view engines.</value>
        public ViewEngineCollection ViewEngines
        {
            get { return _viewEngines; }
        }

        /// <summary>
        /// Registers a route for the given route specification.
        /// </summary>
        /// <param name="routeSpecification">The route specification that describes the pattern of the route 
        /// - for example, "{controller}/{action}".</param>
        /// <returns>The current <see cref="ControllerRouteCatalog"/>, to allow method chaining.</returns>
        public ControllerRouteCatalog MapRoute(string routeSpecification)
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
        /// The current <see cref="ControllerRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ControllerRouteCatalog MapRoute(string routeSpecification, object defaults)
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
        /// The current <see cref="ControllerRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ControllerRouteCatalog MapRoute(string routeSpecification, object defaults, object constraints)
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
        /// The current <see cref="ControllerRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ControllerRouteCatalog MapRoute(string routeSpecification, RouteValueDictionary defaults)
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
        /// The current <see cref="ControllerRouteCatalog"/>, to allow method chaining.
        /// </returns>
        public ControllerRouteCatalog MapRoute(string routeSpecification, RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            Add(new Route(routeSpecification, () => _handler, defaults, constraints, _validator));
            return this;
        }
    }
}