using System;
using System.Collections.Generic;
using System.Linq;

namespace Magellan.Framework
{
    /// <summary>
    /// Maintains a list of validation message objects associated with the property name (key) that is associated with 
    /// each error.
    /// </summary>
    public class ValidationMessageDictionary
    {
        private readonly Dictionary<string, ValidationMessageCollection> _items = new Dictionary<string, ValidationMessageCollection>();
        private readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessageDictionary"/> class.
        /// </summary>
        public ValidationMessageDictionary()
        {
            
        }

        /// <summary>
        /// Occurs when the set of errors changes.
        /// </summary>
        public event EventHandler ErrorsChanged;

        /// <summary>
        /// Gets the validation messages (a collection) associated with the specified key, usually a property name.
        /// </summary>
        public ValidationMessageCollection this[string key]
        {
            get
            {
                lock (_lock)
                {
                    if (!_items.ContainsKey(key))
                    {
                        _items[key] = new ValidationMessageCollection();
                        _items[key].CollectionChanged += (x, y) => OnErrorsChanged(EventArgs.Empty);
                    }
                    return _items[key];
                }
            }
        }

        /// <summary>
        /// Gets all error messages for every key.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> AllMessages()
        {
            return _items.Values.SelectMany(x => x);
        }

        /// <summary>
        /// Gets all available keys.
        /// </summary>
        /// <value>The keys.</value>
        public string[] Keys
        {
            get { return _items.Keys.ToArray(); }
        }

        /// <summary>
        /// Adds an error message for the specified key.
        /// </summary>
        /// <param name="key">The key, usually a property name.</param>
        /// <param name="message">The error message or object.</param>
        public void Add(string key, object message)
        {
            this[key].Add(message);
        }

        /// <summary>
        /// Removes all validation errors.
        /// </summary>
        public void Clear()
        {
            List<ValidationMessageCollection> items;
            lock (_lock)
            {
                items = _items.Values.ToList();
                _items.Clear();
            }

            foreach (var item in items)
            {
                item.Clear();
            }
            OnErrorsChanged(EventArgs.Empty);
        }

        protected void OnErrorsChanged(EventArgs e)
        {
            var handler = ErrorsChanged;
            if (handler != null) handler(this, e);
        }
    }
}