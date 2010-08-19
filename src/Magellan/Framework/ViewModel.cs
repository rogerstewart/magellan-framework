
namespace Magellan.Framework
{
    /// <summary>
    /// A base class for view models in the MVVM pattern.
    /// </summary>
    public abstract class ViewModel : PresentationObject, IViewAware
    {
        private object _view;

        /// <summary>
        /// Gets the View that is attached to this ViewModel.
        /// </summary>
        /// <value>The view.</value>
        protected object View
        {
            get { return _view; }
        }

        /// <summary>
        /// Called when a view has been attached (bound) to this ViewModel.
        /// </summary>
        /// <param name="view">The view.</param>
        protected virtual void ViewAttached(object view)
        {
        }

        /// <summary>
        /// Called when the view attached to this ViewModel raises the Loaded event.
        /// </summary>
        protected virtual void Loaded()
        {
            
        }

        void IViewAware.ViewAttached(object view)
        {
            _view = view;
            ViewAttached(view);
        }

        void IViewAware.Loaded()
        {
            Loaded();
        }
    }
}
