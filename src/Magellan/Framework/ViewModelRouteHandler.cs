using Magellan.Routing;

namespace Magellan.Framework
{
    /// <summary>
    /// A handler for Magellan's MVVM routing support.
    /// </summary>
    public class ViewModelRouteHandler : IRouteHandler
    {
        private readonly IViewModelFactory _factory;
        private readonly IViewInitializer _initializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRouteHandler"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="initializer">The initializer.</param>
        public ViewModelRouteHandler(IViewModelFactory factory, IViewInitializer initializer)
        {
            _factory = factory;
            _initializer = initializer;
        }

        /// <summary>
        /// Processes the navigation request.
        /// </summary>
        /// <param name="request">The navigation request information.</param>
        public void ProcessRequest(ResolvedNavigationRequest request)
        {
            var modelName = request.RouteValues.GetOrDefault<string>("viewModel");
            
            var pair = _factory.CreateViewModel(request, modelName);

            _initializer.Prepare(pair.View, pair.ViewModel, request);

            request.Navigator.NavigateDirectToContent(pair.View, request);
        }
    }
}