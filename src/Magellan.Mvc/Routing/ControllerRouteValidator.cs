using System.Linq;

namespace Magellan.Routing
{
    public class ControllerRouteValidator : DefaultRouteValidator
    {
        public override RouteValidationResult Validate(ParsedRoute route)
        {
            var result = base.Validate(route);
            if (!result.Success)
                return result;

            var hasController = route.Segments.OfType<ParameterSegment>().Any(x => x.ParameterName == "controller")
                                || route.Defaults.GetOrDefault<object>("controller") != null;
            var hasAction = route.Segments.OfType<ParameterSegment>().Any(x => x.ParameterName == "action")
                            || route.Defaults.GetOrDefault<object>("action") != null;

            if (!hasController) return RouteValidationResult.Failure("The route does not contain a '{controller}' segment, and no default controller was provided.");
            if (!hasAction) return RouteValidationResult.Failure("The route does not contain an '{action}' segment, and no default action was provided.");

            return RouteValidationResult.Successful();
        }
    }
}