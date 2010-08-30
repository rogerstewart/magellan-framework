using System.ComponentModel;

namespace Magellan.Framework
{
    /// <summary>
    /// Implemented by <see cref="ViewModel">view models</see> and other <see cref="PresentationObject">
    /// presentation objects</see> that are interested in knowing when the view they are bound to is 
    /// connected.
    /// </summary>
    public interface IViewAware
    {
        /// <summary>
        /// Notifies the target that a view has been attached.
        /// </summary>
        /// <param name="view">The view.</param>
        void ViewAttached(object view);

        /// <summary>
        /// Notifies the target that the view has been loaded.
        /// </summary>
        void Loaded();
    }
}