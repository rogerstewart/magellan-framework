using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Magellan.Diagnostics;
using Magellan.Routing;
using Magellan.Utilities;

namespace Magellan.Mvvm
{
    public interface IViewModelFactory
    {
        ViewModelFactoryResult CreateViewModel(ResolvedNavigationRequest request, string viewModelName);
    }

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly Dictionary<string, Func<object>> _modelBuilders = new Dictionary<string, Func<object>>();
        private readonly Dictionary<string, Func<object>> _viewBuilders = new Dictionary<string, Func<object>>();

        public ViewModelFactory()
        {
            
        }

        public void Register(string name, Func<object> viewType, Func<object> viewModelType)
        {
            Guard.ArgumentNotNull(name, "controllerName");
            Guard.ArgumentNotNull(viewType, "viewBuilder");

            TraceSources.MagellanSource.TraceVerbose("Registering controller '{0}'", name);

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

        private void Initialize(ResolvedNavigationRequest request, object instance)
        {
            if (instance is INavigationAware)
            {
                ((INavigationAware) instance).Navigator = request.Navigator;
            }

            var initializers = instance.GetType().GetMethods().Where(x => x.Name == "Initialize");
            foreach (var initializer in initializers)
            {
                var arguments = new List<object>();
                foreach (var parameter in initializer.GetParameters())
                {
                    var targetType = parameter.ParameterType;
                    var source = request.RouteValues.GetOrDefault<object>(parameter.Name);

                    arguments.Add(Convert(source, targetType));
                }
                initializer.Invoke(instance, arguments.ToArray());
            }
        }

        private object Convert(object source, Type targetType)
        {
            if (source == null)
            {
                return targetType.IsValueType
                    ? Activator.CreateInstance(targetType)
                    : null;
            }

            var sourceType = source.GetType();
            if (targetType.IsAssignableFrom(sourceType))
                return source;

            var targetConverter = TypeDescriptor.GetConverter(targetType);
            if (targetConverter.CanConvertFrom(sourceType))
            {
                return targetConverter.ConvertFrom(source);
            }

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            return sourceConverter.CanConvertTo(targetType)
                ? sourceConverter.ConvertTo(source, targetType)
                : source;
        }

        public ViewModelFactoryResult CreateViewModel(ResolvedNavigationRequest request, string viewModelName)
        {
            var name = viewModelName.ToUpper(CultureInfo.InvariantCulture);
            if (!_modelBuilders.ContainsKey(name))
            {
                throw new Exception("TODO");
            }
            
            var model = _modelBuilders[name]();
            var view = _viewBuilders[name]();

            Initialize(request, view);
            Initialize(request, model);

            return new ViewModelFactoryResult(view, model);
        }
    }

    public class ViewModelFactoryResult
    {
        private readonly object _view;
        private readonly object _viewModel;
        
        public ViewModelFactoryResult(object view, object viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public object View
        {
            get { return _view; }
        }

        public object ViewModel
        {
            get { return _viewModel; }
        }
    }
}
