using System.Collections;

namespace Magellan.Routing
{
    /// <summary>
    /// A bag that tracks whether objects in a route dictionary were consumed when matching route data to a 
    /// path.
    /// </summary>
    public sealed class RouteValueBag
    {
        private readonly RouteValueDictionary _dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteValueBag"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public RouteValueBag(IDictionary dictionary)
        {
            _dictionary = new RouteValueDictionary(dictionary);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return _dictionary.Count == 0; }
        }

        /// <summary>
        /// Gets the remaining items in the dictionary.
        /// </summary>
        /// <returns></returns>
        public RouteValueDictionary GetRemaining()
        {
            return new RouteValueDictionary(_dictionary);
        }

        /// <summary>
        /// Takes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object Take(string key)
        {
            if (_dictionary.ContainsKey(key))
            {
                var value = _dictionary[key];
                _dictionary.Remove(key);
                return value;
            }

            return UrlParameter.NotSpecified;
        }
    }
}