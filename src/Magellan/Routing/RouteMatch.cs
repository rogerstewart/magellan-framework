using System.Collections;

namespace Magellan.Routing
{
    /// <summary>
    /// Represents the result of attempting to match a route from a path.
    /// </summary>
    public sealed class RouteMatch
    {
        private readonly bool _success;
        private readonly IRoute _route;
        private readonly string _failReason;
        private readonly RouteValueDictionary _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteMatch"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="route">The route.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <param name="failReason">The fail reason.</param>
        private RouteMatch(bool success, IRoute route, IDictionary parameterValues, string failReason)
        {
            _success = success;
            _route = route;
            _failReason = failReason;
            _values = new RouteValueDictionary(parameterValues);
        }

        /// <summary>
        /// Produces a successful result.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public static RouteMatch Successful(IRoute route, IDictionary parameterValues)
        {
            return new RouteMatch(true, route, parameterValues, null);
        }

        /// <summary>
        /// Produces an unsuccessful result, giving a reason for the failure.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        public static RouteMatch Failure(IRoute route, string reason)
        {
            return new RouteMatch(false, route, null, reason);
        }

        /// <summary>
        /// Gets a value indicating whether the attempt to match the route was successful.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success
        {
            get { return _success; }
        }

        /// <summary>
        /// Gets the route that was matched.
        /// </summary>
        /// <value>The route.</value>
        public IRoute Route
        {
            get { return _route; }
        }

        /// <summary>
        /// Gets the collection of route values extracted from the path.
        /// </summary>
        /// <value>The values.</value>
        public RouteValueDictionary Values
        {
            get { return _values; }
        }

        /// <summary>
        /// Gets the reason why the route was not matched.
        /// </summary>
        /// <value>The fail reason.</value>
        public string FailReason
        {
            get { return _failReason; }
        }
    }
}