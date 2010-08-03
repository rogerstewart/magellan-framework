namespace Magellan.Mvvm
{
    /// <summary>
    /// A wrapper for a view/view model pair.
    /// </summary>
    public class ViewModelFactoryResult
    {
        private readonly object _view;
        private readonly object _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelFactoryResult"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view model.</param>
        public ViewModelFactoryResult(object view, object viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>The view.</value>
        public object View
        {
            get { return _view; }
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public object ViewModel
        {
            get { return _viewModel; }
        }
    }
}