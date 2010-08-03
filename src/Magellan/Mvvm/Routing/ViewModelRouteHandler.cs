using System;
using System.Windows;
using Magellan.Mvvm;

namespace Magellan.Routing
{
    public class ViewModelRouteHandler : IRouteHandler
    {
        private readonly IViewModelFactory _factory;

        public ViewModelRouteHandler(IViewModelFactory factory)
        {
            _factory = factory;
        }

        public void ProcessRequest(ResolvedNavigationRequest request)
        {
            var modelName = request.RouteValues.GetOrDefault<string>("viewModel");
            
            var pair = _factory.CreateViewModel(request, modelName);

            var element = pair.View as FrameworkElement;
            if (element != null)
            {
                element.DataContext = pair.ViewModel;
            }

            request.Navigator.NavigateDirectToContent(element, request);
        }
    }
}