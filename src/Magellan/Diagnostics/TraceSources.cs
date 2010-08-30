using System.Diagnostics;

namespace Magellan.Diagnostics
{
    /// <summary>
    /// Contains all Magellan trace sources.
    /// </summary>
    public static class TraceSources
    {
        private static TraceSource _magellanSource;

        /// <summary>
        /// Refreshes the trace settings from configuration.
        /// </summary>
        public static void Refresh()
        {
            Trace.Refresh();
        }

        /// <summary>
        /// Gets the Magellan trace source (named 'Magellan' in configuration).
        /// </summary>
        /// <value>The magellan source.</value>
        public static TraceSource MagellanSource
        {
            get { return _magellanSource ?? (_magellanSource = CreateTraceSource("Magellan")); }
        }

        private static TraceSource CreateTraceSource(string name)
        {
            var source = new TraceSource(name);
#if SILVERLIGHT
            source.Switch.Level = SourceLevels.Off;
#else
            source.Switch.Level = SourceLevels.Warning;
#endif
            return source;
        }
    }
}
