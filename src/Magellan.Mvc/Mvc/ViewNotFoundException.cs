using System;
using System.Collections.Generic;
using System.Text;
using Magellan.Exceptions;

namespace Magellan.Mvc
{
    /// <summary>
    /// An exception thrown by the Magellan navigation framework. This exception indicates that an action has 
    /// attempted to return a view result, but none of the view engines were able to find the specified view.
    /// Please ensure the view name is correct, and see the <see cref="SearchLocations"/> property for a list
    /// of searched names.
    /// </summary>
    public class ViewNotFoundException : NavigationException
    {
        private readonly string _viewName;
        private readonly IEnumerable<string> _searchLocations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewNotFoundException"/> class.
        /// </summary>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="searchLocations">The search locations.</param>
        public ViewNotFoundException(string controllerName, string actionName, string viewName, IEnumerable<string> searchLocations)
            : base(BuildMessage(controllerName, actionName, viewName, searchLocations))
        {
            _viewName = viewName;
            _searchLocations = searchLocations;
        }

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <value>The name of the view.</value>
        public string ViewName
        {
            get { return _viewName; }
        }

        /// <summary>
        /// Gets the search locations.
        /// </summary>
        /// <value>The search locations.</value>
        public IEnumerable<string> SearchLocations
        {
            get { return _searchLocations; }
        }

        private static string BuildMessage(string controllerName, string actionName, string viewName, IEnumerable<string> searchLocations)
        {
            var errorMessage = new StringBuilder();
            errorMessage.AppendFormat("The action '{0}' on controller '{1}' returned a view result for the view '{2}'.", actionName, controllerName, viewName).AppendLine();
            errorMessage.AppendFormat("However, none of the registered view engines could find the view '{0}'.", viewName).AppendLine();
            errorMessage.AppendLine("The following locations were searched:");
            foreach (var excuse in searchLocations)
            {
                errorMessage.Append(" - ").AppendLine(excuse);
            }
            return errorMessage.ToString();
        }
    }
}