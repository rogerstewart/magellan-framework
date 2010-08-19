using System;
using Magellan.Routing;

namespace Magellan.Framework
{
    /// <summary>
    /// Provides information to <see cref="IModelBinder">model binders</see> about the target action parameter and the values available.
    /// </summary>
    public class ModelBindingContext
    {
        private readonly string _targetParameterName;
        private readonly Type _targetParameterType;
        private readonly RouteValueDictionary _potentialValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBindingContext"/> class.
        /// </summary>
        /// <param name="targetParameterName">Name of the target parameter.</param>
        /// <param name="targetParameterType">Type of the target parameter.</param>
        /// <param name="potentialValues">The potential values.</param>
        public ModelBindingContext(string targetParameterName, Type targetParameterType, RouteValueDictionary potentialValues)
        {
            _targetParameterName = targetParameterName;
            _targetParameterType = targetParameterType;
            _potentialValues = potentialValues;
        }

        /// <summary>
        /// Gets the name of the parameter on the action (typically the method argument).
        /// </summary>
        /// <value>The name of the target parameter.</value>
        public string TargetParameterName
        {
            get { return _targetParameterName; }
        }

        /// <summary>
        /// Gets the type of the parameter on the action (typically the method argument type).
        /// </summary>
        /// <value>The type of the target parameter.</value>
        public Type TargetParameterType
        {
            get { return _targetParameterType; }
        }

        /// <summary>
        /// Gets the navigation request values that are available for mapping.
        /// </summary>
        /// <value>The potential values.</value>
        public RouteValueDictionary RouteValues
        {
            get { return _potentialValues; }
        }
    }
}