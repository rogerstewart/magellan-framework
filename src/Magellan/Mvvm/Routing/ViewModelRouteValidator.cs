using System.Linq;

namespace Magellan.Routing
{
    internal class ViewModelRouteValidator : DefaultRouteValidator
    {
        public override RouteValidationResult Validate(ParsedRoute route)
        {
            var result = base.Validate(route);
            if (!result.Success)
                return result;

            var hasViewModel = route.Segments.OfType<ParameterSegment>().Any(x => x.ParameterName == "viewModel")
                                || route.Defaults.GetOrDefault<object>("viewModel") != null;
            
            if (!hasViewModel) 
                return RouteValidationResult.Failure("The route does not contain a '{viewModel}' segment, and no default ViewModel type was provided.");
            
            return RouteValidationResult.Successful();
        }
    }
}