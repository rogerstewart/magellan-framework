using System;
using System.Windows;
using Magellan.Diagnostics;

namespace Magellan.Mvc
{
    /// <summary>
    /// A <see cref="ViewEngineResult"/> for Window-derived elements.
    /// </summary>
    public class WindowViewEngineResult : FrameworkElementViewEngineResult
    {
        private readonly Type _viewType;
        private readonly IViewActivator _viewActivator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowViewEngineResult"/> class.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="options">The options.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewActivator">The view activator.</param>
        public WindowViewEngineResult(Type viewType, ViewResultOptions options, ControllerContext controllerContext, IViewActivator viewActivator) 
            : base(controllerContext, options)
        {
            _viewType = viewType;
            _viewActivator = viewActivator;
        }

        /// <summary>
        /// Gets or sets the rendered instance of the Window.
        /// </summary>
        /// <value>The rendered instance.</value>
        public Window RenderedInstance { get; private set; }

        /// <summary>
        /// Gets the type of the view.
        /// </summary>
        /// <value>The type of the view.</value>
        public Type ViewType
        {
            get { return _viewType; }
        }

        /// <summary>
        /// Renders this view.
        /// </summary>
        public override void Render()
        {
            var dispatcher = ControllerContext.Request.Navigator.Dispatcher;

            dispatcher.Dispatch(
                delegate
                {
                    TraceSources.MagellanSource.TraceInformation("The WindowViewEngine is instantiating the window '{0}'.", _viewType);
                    
                    // Prepare the window
                    RenderedInstance = (Window)_viewActivator.Instantiate(_viewType);
                    WireModelToView(RenderedInstance);

                    // Decide how to show it
                    var dialog = Options.GetViewType() == "Dialog";
                    if (dialog)
                    {
                        TraceSources.MagellanSource.TraceVerbose("The WindowViewEngine is rendering the window '{0}' as a dialog.", _viewType);
                        RenderedInstance.ShowDialog();
                    }
                    else
                    {
                        TraceSources.MagellanSource.TraceVerbose("The WindowViewEngine is rendering the window '{0}' as a normal window.", _viewType);
                        RenderedInstance.Show();
                    }
                });
        }
    }
}