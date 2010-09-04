using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Magellan.Diagnostics;

namespace Magellan.Framework
{
    /// <summary>
    /// A <see cref="ViewEngineResult"/> that contains a WPF <see cref="Page"/> that will be navigated to using the <see cref="NavigationService"/> of the incoming request.
    /// </summary>
    public class 
        PageViewEngineResult : FrameworkElementViewEngineResult
    {
        private readonly Type _viewType;
        private readonly IViewActivator _viewActivator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewEngineResult"/> class.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="options">The options.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewActivator">The view activator.</param>
        public PageViewEngineResult(Type viewType, ViewResultOptions options, ControllerContext controllerContext, IViewActivator viewActivator)
            : base(controllerContext, options)
        {
            _viewType = viewType;
            _viewActivator = viewActivator;
        }

        /// <summary>
        /// Gets the type of the view.
        /// </summary>
        /// <value>The type of the view.</value>
        public Type ViewType
        {
            get { return _viewType; }
        }

        /// <summary>
        /// Gets or sets the rendered instance of the Page or ContentControl.
        /// </summary>
        /// <value>The rendered instance.</value>
        public FrameworkElement RenderedInstance { get; private set; }

        /// <summary>
        /// Renders this view.
        /// </summary>
        public override void Render()
        {
            var dispatcher = ControllerContext.Request.Navigator.Dispatcher;

            dispatcher.Dispatch(
                delegate
                {
                    TraceSources.MagellanSource.TraceInformation("The PageViewEngine is rendering the page '{0}'.", _viewType);

                    // Prepare the page
                    RenderedInstance = (FrameworkElement)_viewActivator.Instantiate(_viewType);

                    ViewInitializer.Prepare(RenderedInstance, Model, ControllerContext.Request);

                    // Navigate to the page
                    var navigationService = ControllerContext.Request.Navigator;
                    navigationService.NavigateDirectToContent(RenderedInstance, ControllerContext.Request);

                    TraceSources.MagellanSource.TraceVerbose("The PageViewEngine has navigated to the page '{0}'.", _viewType);

                    // Remove all back entries from the navigation journal if necessary
                    var clearHistory = Options.GetResetHistory();
                    if (clearHistory)
                    {
                        TraceSources.MagellanSource.TraceVerbose("The PageViewEngine is clearing the navigation history.", _viewType);
                        navigationService.ResetHistory();
                    }
                });
        }
    }
}