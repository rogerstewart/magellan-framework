using System.ComponentModel;

namespace Magellan.Framework
{
    /// <summary>
    /// A base class for view models in the MVVM pattern.
    /// </summary>
    public abstract class ViewModel : PresentationObject, IViewLifecycle
    {
        /// <summary>
        /// Called when a view has been attached (bound) to this ViewModel.
        /// </summary>
        /// <param name="view">The view.</param>
        protected virtual void ViewAttached(object view)
        {
        }

        /// <summary>
        /// Called after the ViewModel has been bound to a View and the View is loaded. This corresponds to
        /// WPF's Loaded event. This method will only be called once the first time the view is loaded.
        /// </summary>
        protected virtual void Loaded()
        {
        }

        /// <summary>
        /// Called when the view that owns this ViewModel is closing. Gives the target the opportunity to 
        /// cancel closing through the <see cref="CancelEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the
        /// event data.</param>
        protected virtual void Closing(CancelEventArgs e)
        {
        }

        /// <summary>
        /// Called when the view that owns this ViewModel has been closed (either navigated away from or
        /// closed).
        /// </summary>
        protected virtual void Closed()
        {
        }

        void IViewLifecycle.ViewAttached(object view)
        {
            ViewAttached(view);
        }

        void IViewLifecycle.Loaded()
        {
            Loaded();
        }

        void IViewLifecycle.Closing(CancelEventArgs e)
        {
            Closing(e);
        }

        void IViewLifecycle.Closed()
        {
            Closed();   
        }
    }
}
