using Magellan.Framework;

namespace Magellan.Routing
{
    public class ControllerRouteHandler : IRouteHandler
    {
        private readonly IControllerFactory _controllerFactory;

        public ControllerRouteHandler(IControllerFactory controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }

        public void ProcessRequest(RouteRequest request)
        {
            var controllerName = request.RouteValues.GetOrDefault<string>("controller");
            var actionName = request.RouteValues.GetOrDefault<string>("action");

            var navigationRequest = new NavigationRequest(controllerName, actionName, new ParameterValueDictionary(request.RouteValues));
            var controller = _controllerFactory.CreateController(navigationRequest, controllerName);
            using (controller)
            {
                controller.Controller.Execute(new ControllerContext(controller.Controller, navigationRequest));
            }
        }
    }
}
