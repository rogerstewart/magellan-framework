using System.ComponentModel;

namespace Magellan.Framework
{
    /// <summary>
    /// Implemented by <see cref="ViewModel">view models</see> and other <see cref="PresentationObject">
    /// presentation objects</see> that are interested in participating in lifecycle events of a view.
    /// </summary>
    public interface IViewLifecycle
    {
        /// <summary>
        /// Notifies the target that a view has been attached.
        /// </summary>
        /// <param name="view">The view.</param>
        void ViewAttached(object view);

        /// <summary>
        /// Notifies the target that now is a good time to do any additional loading. This corresponds to 
        /// WPF's Loaded event. This method will only be called once the first time the view is loaded.
        /// </summary>
        void Loaded();

        /// <summary>
        /// Notifies the target that the view that owns it is now closing. Gives the target the opportunity 
        /// to cancel closing through the <see cref="CancelEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the 
        /// event data.</param>
        void Closing(CancelEventArgs e);

        /// <summary>
        /// Notifies the target that the view that owns it has been closed (either navigated away from or 
        /// closed).
        /// </summary>
        void Closed();
    }
}