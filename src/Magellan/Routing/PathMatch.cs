using System.Collections.Generic;
using System.Linq;

namespace Magellan.Routing
{
    /// <summary>
    /// Represents the result of matching a set of route parameter values to a route, to produce a path.
    /// </summary>
    public sealed class PathMatch
    {
        private readonly bool _success;
        private readonly IRoute _route;
        private readonly RouteValueDictionary _routeValues;
        private readonly RouteValueDictionary _leftOver;
        private readonly List<object> _segmentValues;
        private readonly string _failReason;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathMatch"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="route">The route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="leftOver">The left over.</param>
        /// <param name="segmentValues">The segment values.</param>
        /// <param name="failReason">The fail reason.</param>
        private PathMatch(bool success, IRoute route, RouteValueDictionary routeValues, RouteValueDictionary leftOver, List<object> segmentValues, string failReason)
        {
            _success = success;
            _route = route;
            _routeValues = routeValues;
            _leftOver = leftOver;
            _segmentValues = segmentValues;
            _failReason = failReason;
        }

        /// <summary>
        /// Produces a successful result.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="leftOver">The left over values.</param>
        /// <param name="segmentValues">The segment values.</param>
        /// <returns></returns>
        public static PathMatch Successful(IRoute route, RouteValueDictionary routeValues, RouteValueDictionary leftOver, List<object> segmentValues)
        {
            return new PathMatch(true, route, routeValues, leftOver, segmentValues, null);
        }

        /// <summary>
        /// Produces an unsuccessful result.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        public static PathMatch Failure(IRoute route, string reason)
        {
            return new PathMatch(false, route, null, null, null, reason);
        }

        /// <summary>
        /// Gets a value indicating whether the attempt to match route data to a path was successful.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success
        {
            get { return _success; }
        }

        /// <summary>
        /// Gets the first route that successfully matched the route values to produce a path.
        /// </summary>
        /// <value>The route.</value>
        public IRoute Route
        {
            get { return _route; }
        }

        /// <summary>
        /// Gets the route values.
        /// </summary>
        /// <value>The route values.</value>
        public RouteValueDictionary RouteValues
        {
            get { return _routeValues; }
        }

        /// <summary>
        /// Gets a list of values that would be combined together to produce a path.
        /// </summary>
        /// <value>The segment values.</value>
        public IList<object> SegmentValues
        {
            get { return _segmentValues; }
        }

        /// <summary>
        /// Gets the path that was matched.
        /// </summary>
        /// <value>The path.</value>
        public string Path
        {
            get 
            {
                return string.Join("/",
                    (_segmentValues ?? new List<object>())
                    .Where(x => x != null)
                    .Where(x => x is string == false || (x is string && ((string)x).Length > 0))
                    .Select(x => x.ToString())
                    .ToArray()); 
            }
        }

        /// <summary>
        /// Gets the left over values that were given to the route, but not matched.
        /// </summary>
        /// <value>The left over values.</value>
        public RouteValueDictionary LeftOverValues
        {
            get { return _leftOver; }
        }

        /// <summary>
        /// If the route values were not matched to a route, returns a reason explaining why.
        /// </summary>
        /// <value>The fail reason.</value>
        public string FailReason
        {
            get { return _failReason; }
        }
    }
}