using System;
using System.Linq;
using System.Linq.Expressions;
using Magellan.Exceptions;
using Magellan.Mvc;
using Magellan.Routing;

namespace Magellan
{
    /// <summary>
    /// Extension methods for the <see cref="INavigator"/> interface that help with MVC navigation 
    /// scenarios.
    /// </summary>
    public static class NavigatorExtensions
    {
        /// <summary>
        /// Navigates to the specified controller, following the action and parameters passed below. Note 
        /// that the controller must be registered in the controller factory with the type name minus the 
        /// "Controller" suffix - for example, a controller of type "PatientSearchController" should be 
        /// registered with the name "PatientSearch" for this call to work.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="navigator">The navigator.</param>
        /// <param name="actionSelector">The action selector.</param>
        public static void Navigate<TController>(this INavigator navigator, Expression<Func<TController, ActionResult>> actionSelector)
            where TController : ControllerBase
        {
            var routeValues = new RouteValueDictionary();
            var controller = typeof (TController);
            var controllerName = controller.Name.Replace("Controller", "");
            routeValues.Add("controller", controllerName);
            
            var body = actionSelector.Body as MethodCallExpression;
            if (body == null)
            {
                throw new ImpossibleNavigationRequestException("The lambda expression used for navigation could not be parsed. The lambda should be a MethodCallExpression, for example: 'x => x.Search(text)'.");
            }

            var method = body.Method;
            var actionName = method.Name;
            routeValues.Add("action", actionName);
            
            var parameters = method.GetParameters();
            var arguments = body.Arguments;
            for (var i = 0; i < parameters.Length && i < arguments.Count; i++)
            {
                var parameter = parameters[i].Name;
                var argument = arguments[i];
                var lambda = Expression.Lambda<Func<TController, object>>(argument, actionSelector.Parameters.ToList());
                var compiled = lambda.Compile();
                var value = compiled(null);

                routeValues.Add(parameter, value);
            }

            var request = new NavigationRequest(routeValues);
            navigator.ProcessRequest(request);
        }
    }
}
