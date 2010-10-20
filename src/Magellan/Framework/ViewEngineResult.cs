using System.Collections.Generic;

namespace Magellan.Framework
{
    /// <summary>
    /// The base class for the objects returned by view engines.
    /// </summary>
    public class ViewEngineResult
    {
        private readonly bool _success;
        private readonly IEnumerable<string> _searchLocations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewEngineResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="searchLocations">The search locations.</param>
        public ViewEngineResult(bool success, IEnumerable<string> searchLocations)
        {
            _success = success;
            _searchLocations = searchLocations;
        }

        /// <summary>
        /// Gets a value indicating whether the view could be resolved.
        /// </summary>
        /// <value><c>true</c> if the view was found; otherwise, <c>false</c>.</value>
        public bool Success
        {
            get { return _success; }
        }

        /// <summary>
        /// Gets the list of locations that were searched in the attempt to locate this view.
        /// </summary>
        /// <value>The search locations.</value>
        public IEnumerable<string> SearchLocations
        {
            get { return _searchLocations; }
        }

        /// <summary>
        /// Renders this view.
        /// </summary>
        public virtual void Render()
        {
        }
    }
}