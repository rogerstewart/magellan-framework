using System.Text.RegularExpressions;
using Magellan.Exceptions;
using Magellan.Utilities;

namespace Magellan.Routing
{
    /// <summary>
    /// Represents a route specification segment such as "{*path}".
    /// </summary>
    internal class CatchAllParameterSegment : Segment
    {
        private readonly string _parameterName;
        private readonly object _defaultValue;
        private readonly IRouteConstraint _constraint;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatchAllParameterSegment"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="constraint">The constraint.</param>
        public CatchAllParameterSegment(string parameterName, object defaultValue, object constraint)
        {
            Guard.ArgumentNotNullOrEmpty(parameterName, "parameterName");
            _parameterName = parameterName;
            _defaultValue = defaultValue;
            
            if (constraint is string)
            {
                _constraint = new RegexConstraint((string)constraint);
            }
            else if (constraint is IRouteConstraint)
            {
                _constraint = (IRouteConstraint)constraint;
            }
            else if (constraint is Regex)
            {
                _constraint = new RegexConstraint((Regex)constraint);
            }
            else if (constraint == UrlParameter.NotSpecified)
            {
                _constraint = null;
            }
            else if (constraint != null)
            {
                throw new UnsupportedConstraintException("The parameter '{0}' was given an invalid constraints. Constraints must be strings, Regex's or objects that implement IRouteConstraint");
            }
        }

        /// <summary>
        /// Gets the name of the parameter (e.g., if created with a spec of "{*foo}", the value will be 
        /// "foo").
        /// </summary>
        /// <value>The name of the parameter.</value>
        public string ParameterName
        {
            get { return _parameterName; }
        }

        /// <summary>
        /// Matches the path.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override SegmentPathMatch MatchPath(IRoute route, PathIterator reader)
        {
            var values = new RouteValueDictionary();
            var path = reader.ReadAll();

            values[_parameterName] = path;
            if (path.Length == 0)
            {
                if (_defaultValue != UrlParameter.NotSpecified)
                {
                    values[_parameterName] = _defaultValue;
                }
            }
            else if (_constraint != null && !_constraint.IsValid(route, path, _parameterName))
            {
                return SegmentPathMatch.Failure(string.Format("Segment '{0}' did not match constraint for parameter '{1}'", path, _parameterName));
            }
            return SegmentPathMatch.Successful(values);
        }

        /// <summary>
        /// Given a set of route values, extracts any necessary values that would be used by this segment.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="values">The values.</param>
        /// <returns>
        /// An object that indicates whether the values were successfully matched.
        /// </returns>
        public override SegmentValueMatch MatchValues(IRoute route, RouteValueBag values)
        {
            var value = values.Take(_parameterName);
            if (value == UrlParameter.NotSpecified)
            {
                if (_defaultValue != UrlParameter.NotSpecified)
                {
                    return SegmentValueMatch.Successful(_defaultValue);
                }
                return SegmentValueMatch.Successful();
            }

            return SegmentValueMatch.Successful(value);
        }
    }
}
