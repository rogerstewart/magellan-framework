using System;
using System.Collections.Generic;
using System.Linq;

namespace Magellan.Framework
{
    /// <summary>
    /// Contains a list of <see cref="IModelBinder">model binders</see>.
    /// </summary>
    public class ModelBinderDictionary
    {
        private readonly Dictionary<Func<Type, bool>, IModelBinder> _binders = new Dictionary<Func<Type, bool>, IModelBinder>();
        private IModelBinder _defaultBinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBinderDictionary"/> class.
        /// </summary>
        /// <param name="defaultBinder">The default binder.</param>
        public ModelBinderDictionary(IModelBinder defaultBinder)
        {
            _defaultBinder = defaultBinder;
        }

        /// <summary>
        /// Gets or sets the default model binder.
        /// </summary>
        /// <value>The default model binder.</value>
        public IModelBinder DefaultModelBinder
        {
            get { return _defaultBinder = _defaultBinder ?? new DefaultModelBinder(); }
            set { _defaultBinder = value;}
        }

        /// <summary>
        /// Maps a model type to a binder.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="binder">The binder.</param>
        public void Add(Type modelType, IModelBinder binder)
        {
            var evaluator = (Func<Type, bool>)(modelType.IsAssignableFrom);
            _binders.Add(evaluator, binder);
        }

        /// <summary>
        /// Gets the binder for the specified model type.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns></returns>
        public IModelBinder GetBinder(Type modelType)
        {
            foreach (var value in _binders)
            {
                if (value.Key(modelType))
                {
                    return value.Value;
                }
            }
            return DefaultModelBinder;
        }

        /// <summary>
        /// Removes the specified model binder.
        /// </summary>
        /// <param name="binder">The model binder to remove.</param>
        public void Remove(IModelBinder binder)
        {
            var pairs = _binders.Where(x => x.Value == binder).ToArray();
            foreach (var pair in pairs)
            {
                _binders.Remove(pair.Key);
            }
        }

        /// <summary>
        /// Determines whether the specified model binder has been registered.
        /// </summary>
        /// <param name="binder">The model binder.</param>
        /// <returns>
        /// 	<c>true</c> if the specified model type has been mapped; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(IModelBinder binder)
        {
            return _binders.Any(x => x.Value == binder);
        }

        /// <summary>
        /// Clears all mappings.
        /// </summary>
        public void Clear()
        {
            _binders.Clear();
        }
    }
}