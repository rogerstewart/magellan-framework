using System.Linq;
using Magellan.Routing;

namespace Magellan.Framework
{
    /// <summary>
    /// A route validator for Magellan's MVVM routin support.
    /// </summary>
    internal class ViewModelRouteValidator : DefaultRouteValidator
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

            var hasViewModel = route.Segments.OfType<ParameterSegment>().Any(x => x.ParameterName == "viewModel")
                                || route.Defaults.GetOrDefault<object>("viewModel") != null;
            
            if (!hasViewModel) 
                return RouteValidationResult.Failure("The route does not contain a '{viewModel}' segment, and no default ViewModel type was provided.");
            
            return RouteValidationResult.Successful();
        }
    }
}