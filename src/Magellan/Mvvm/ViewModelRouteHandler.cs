using System.Windows;
using Magellan.Routing;

namespace Magellan.Mvvm
{
    /// <summary>
    /// A handler for Magellan's MVVM routing support.
    /// </summary>
    public class ViewModelRouteHandler : IRouteHandler
    {
        private readonly IViewModelFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRouteHandler"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ViewModelRouteHandler(IViewModelFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Processes the navigation request.
        /// </summary>
        /// <param name="request">The navigation request information.</param>
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