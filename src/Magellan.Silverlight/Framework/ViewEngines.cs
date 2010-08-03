using System;
using System.Collections.Generic;

namespace Magellan.Framework
{
    /// <summary>
    /// Manages the global registry of view engines that are available for rendering a view.
    /// </summary>
    public class ViewEngines
    {
        private static readonly ViewEngineCollection _engines;

        /// <summary>
        /// Initializes the <see cref="ViewEngines"/> class.
        /// </summary>
        static ViewEngines()
        {
            var engines = new ViewEngineCollection();
            engines.Add(new PageViewEngine(new DefaultViewActivator()));
            engines.Add(new ChildWindowViewEngine(new DefaultViewActivator()));
            _engines = engines;
        }

        /// <summary>
        /// Gets the currently active view engines.
        /// </summary>
        /// <value>The engines.</value>
        public static ViewEngineCollection Engines
        {
            get
            {
                return _engines;
            }
        }
    }
}
