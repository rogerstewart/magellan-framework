namespace Magellan.Routing
{
    /// <summary>
    /// Represents the result from matching a route value to a segment.
    /// </summary>
    public sealed class SegmentValueMatch
    {
        private readonly bool _success;
        private readonly object _segmentValue;
        private readonly string _failReason;

        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentValueMatch"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="segmentValue">The segment value.</param>
        /// <param name="failReason">The fail reason.</param>
        private SegmentValueMatch(bool success, object segmentValue, string failReason)
        {
            _success = success;
            _segmentValue = segmentValue;
            _failReason = failReason;
        }

        /// <summary>
        /// Produces a successful result.
        /// </summary>
        /// <returns></returns>
        public static SegmentValueMatch Successful()
        {
            return Successful(null);
        }

        /// <summary>
        /// Produces a successful result.
        /// </summary>
        /// <param name="segmentValue">The segment value.</param>
        /// <returns></returns>
        public static SegmentValueMatch Successful(object segmentValue)
        {
            return new SegmentValueMatch(true, segmentValue, null);
        }

        /// <summary>
        /// Produces an unsuccessful result.
        /// </summary>
        /// <param name="failReason">The fail reason.</param>
        /// <returns></returns>
        public static SegmentValueMatch Failure(string failReason)
        {
            return new SegmentValueMatch(false, null, failReason);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SegmentValueMatch"/> was successful.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success
        {
            get { return _success; }
        }

        /// <summary>
        /// Gets the particular value from the segment.
        /// </summary>
        /// <value>The segment value.</value>
        public object SegmentValue
        {
            get { return _segmentValue; }
        }

        /// <summary>
        /// Gets the reason why the segment value wasn't matched.
        /// </summary>
        /// <value>The fail reason.</value>
        public string FailReason
        {
            get { return _failReason; }
        }
    }
}