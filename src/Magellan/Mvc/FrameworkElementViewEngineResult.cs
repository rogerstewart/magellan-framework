using System.Windows;
using Magellan.Diagnostics;
using Magellan.Views;

namespace Magellan.Mvc
{
    /// <summary>
    /// Serves as a base class for view engine results that rely on WPF objects. Provides a helper method for 
    /// automatically wiring up the data context and <see cref="INavigationAware"/> support.
    /// </summary>
    public abstract class FrameworkElementViewEngineResult : ViewEngineResult
    {
        private readonly ControllerContext _controllerContext;
        private readonly ViewResultOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkElementViewEngineResult"/> class.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        protected FrameworkElementViewEngineResult(ControllerContext controllerContext, ViewResultOptions options) : base(true, new string[0])
        {
            _controllerContext = controllerContext;
            _options = options;
        }

        /// <summary>
        /// Gets the controller context.
        /// </summary>
        /// <value>The controller context.</value>
        public ControllerContext ControllerContext
        {
            get { return _controllerContext; }
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        public ViewResultOptions Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Wires the model to view, either by setting the DataContext, or if the view implements IView, by 
        /// setting the model property. It also sets the NavigationContext if the view or model implement 
        /// <see cref="INavigationAware"/>.
        /// </summary>
        /// <param name="view">The view.</param>
        protected virtual void WireModelToView(FrameworkElement view)
        {
            // Connect the model to the view
            var model = Options.GetModel();
            if (view is IView)
            {
                TraceSources.MagellanSource.TraceVerbose("The view '{0}' implements the IView interface, so the model is being set as the Model on the IView.", view.GetType().Name);
                ((IView)view).Model = model;
            }
            else
            {
                TraceSources.MagellanSource.TraceVerbose("The view '{0}' does not implement the IView interface, so the model is being set as the DataContext.", view.GetType().Name);
                view.DataContext = model;
            }

            // Make the navigation context available if supported
            var navigator = _controllerContext.Request.Navigator;
            if (navigator != null)
            {
                var navigationAwareModel = model as INavigationAware;
                var navigationAwareView = view as INavigationAware;
                var viewAsDependencyObject = view as DependencyObject;

                if (navigationAwareModel != null)
                {
                    TraceSources.MagellanSource.TraceVerbose("The model '{0}' implements the INavigationAware interface, so it is being provided with a navigator.", navigationAwareModel.GetType().Name);
                    navigationAwareModel.Navigator = navigator;
                }
                if (navigationAwareView != null)
                {
                    TraceSources.MagellanSource.TraceVerbose("The view '{0}' implements the INavigationAware interface, so it is being provided with a navigator.", navigationAwareView.GetType().Name);
                    navigationAwareView.Navigator = navigator;
                }
                NavigationProperties.SetNavigator(viewAsDependencyObject, navigator);
            }
        }
    }
}