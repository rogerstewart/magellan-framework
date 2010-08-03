using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Magellan.Abstractions;
using Magellan.Diagnostics;

namespace Magellan.Framework
{
    /// <summary>
    /// The result from the <see cref="PageViewEngine"/>.
    /// </summary>
    public class PageViewEngineResult : FrameworkElementViewEngineResult
    {
        private readonly ParameterValueDictionary _viewParameters;
        private readonly ControllerContext _controllerContext;
        private readonly INavigationService _navigator;
        private readonly IViewActivator _viewActivator;
        private Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewEngineResult"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="viewParameters">The view parameters.</param>
        /// <param name="controllerContext">The controller context.</param>
        public PageViewEngineResult(IViewActivator viewActivator, Type type, ParameterValueDictionary viewParameters, ControllerContext controllerContext) : base(viewParameters, controllerContext)
        {
            _viewActivator = viewActivator;
            _type = type;
            _viewParameters = viewParameters;
            _controllerContext = controllerContext;

            _navigator = controllerContext.Request.ActionParameters.GetOrDefault<INavigationService>(WellKnownParameters.Navigator);
        }

        public UserControl RenderedInstance { get; private set; }

        /// <summary>
        /// Renders this view.
        /// </summary>
        public override void Render()
        {
            var dispatcher = _viewParameters.GetOrDefault<IDispatcher>(WellKnownParameters.Dispatcher)
                ?? new DispatcherWrapper();

            dispatcher.Dispatch(
                delegate
                {
                    TraceSources.MagellanSource.TraceInformation("The WindowViewEngine is instantiating the window '{0}'.", _type);

                    // Prepare the window
                    RenderedInstance = (UserControl)_viewActivator.Instantiate(_type);
                    WireModelToView(RenderedInstance);

                    TraceSources.MagellanSource.TraceVerbose("The ChildWindowViewEngine is rendering the window '{0}' as a dialog.", _type);
                    _navigator.NavigateTo(RenderedInstance);
                });
        }
    }
}