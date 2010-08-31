using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Magellan.Diagnostics;
using Magellan.Routing;
using Magellan.Utilities;

namespace Magellan.Framework
{
    /// <summary>
    /// A default implementation of <see cref="IViewModelFactory"/> that uses delegates for resolving views and 
    /// view models.
    /// </summary>
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly ModelBinderDictionary _modelBinders = new ModelBinderDictionary(new DefaultModelBinder());
        private readonly IViewInitializer _initializer;
        private readonly Dictionary<string, Func<object>> _modelBuilders = new Dictionary<string, Func<object>>();
        private readonly Dictionary<string, Func<object>> _viewBuilders = new Dictionary<string, Func<object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
        /// </summary>
        public ViewModelFactory() 
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
        /// </summary>
        public ViewModelFactory(IViewInitializer initializer)
        {
            _initializer = initializer ?? new DefaultViewInitializer(_modelBinders);
        }

        /// <summary>
        /// Registers a view and view model pair by the specified name.
        /// </summary>
        /// <param name="name">The name that will later be used to resolve the view/view model pair.</param>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="viewModelType">Type of the view model.</param>
        public void Register(string name, Func<object> viewType, Func<object> viewModelType)
        {
            Guard.ArgumentNotNull(name, "controllerName");
            Guard.ArgumentNotNull(viewType, "viewBuilder");

            TraceSources.MagellanSource.TraceVerbose("Registering view/view model pair '{0}'", name);

            if (_modelBuilders.ContainsKey(name.ToUpper(CultureInfo.InvariantCulture)))
            {
                throw new ArgumentException(string.Format("A view model with the name '{0}' has already been added.", name));
            }
            _modelBuilders.Add(name.ToUpper(CultureInfo.InvariantCulture), viewModelType);

            if (_viewBuilders.ContainsKey(name.ToUpper(CultureInfo.InvariantCulture)))
            {
                throw new ArgumentException(string.Format("A view with the name '{0}' has already been added.", name));
            }
            _viewBuilders.Add(name.ToUpper(CultureInfo.InvariantCulture), viewType);
        }

        /// <summary>
        /// Creates a view and view model to handle the given navigation request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns>
        /// An object containing the View/ViewModel pair.
        /// </returns>
        public ViewModelFactoryResult CreateViewModel(ResolvedNavigationRequest request, string viewModelName)
        {
            var name = viewModelName.ToUpper(CultureInfo.InvariantCulture);
            if (!_modelBuilders.ContainsKey(name))
            {
                throw new ArgumentException(string.Format("A view model by the name {0} is not registered in this ViewModelFactory.", name));
            }
            
            var model = _modelBuilders[name]();
            var view = _viewBuilders[name]();

            _initializer.Prepare(view, model, request);

            return new ViewModelFactoryResult(view, model);
        }
    }
}
