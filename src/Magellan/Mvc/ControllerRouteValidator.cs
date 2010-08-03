using System.Linq;
using Magellan.Routing;

namespace Magellan.Mvc
{
    /// <summary>
    /// A validator for Magellan's MVC routes (<see cref="ControllerRouteHandler"/>).
    /// </summary>
    public class ControllerRouteValidator : DefaultRouteValidator
    {
        /// <summary>
        /// Validates the specified route, producing a <see cref="RouteValidationResult"/> indicating
        /// what the error (if any) was.
        /// </summary>
        /// <param name="route">The route to validate.</param>
        /// <returns>
        /// An object indicating the success of the validation attempt, and details about any error
        /// encountered.
        /// </returns>
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