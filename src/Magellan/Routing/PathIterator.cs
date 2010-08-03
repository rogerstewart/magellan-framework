using System.Collections.Generic;

namespace Magellan.Routing
{
    /// <summary>
    /// An iterator that walks a string.
    /// </summary>
    public sealed class PathIterator
    {
        private readonly Queue<string> _partQueue = new Queue<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PathIterator"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public PathIterator(string path)
        {
            var parts = path.SplitUrlPath();
            foreach (var part in parts)
            {
                _partQueue.Enqueue(part);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is at end.
        /// </summary>
        /// <value><c>true</c> if this instance is at end; otherwise, <c>false</c>.</value>
        public bool IsAtEnd
        {
            get { return _partQueue.Count == 0; }
        }

        /// <summary>
        /// Retrieves the next path part.
        /// </summary>
        /// <returns></returns>
        public string Next()
        {
            return _partQueue.Count == 0 
                ? "" 
                : _partQueue.Dequeue();
        }

        /// <summary>
        /// Reads to the end of the path.
        /// </summary>
        /// <returns></returns>
        public string ReadAll()
        {
            var result = string.Join("/", _partQueue.ToArray());
            _partQueue.Clear();
            return result;
        }
    }
}