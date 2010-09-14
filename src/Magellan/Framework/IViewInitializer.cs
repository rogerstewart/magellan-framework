using System.ComponentModel;
using Magellan.Routing;

namespace Magellan.Framework
{
    /// <summary>
    /// An interface implemented by objects that know how to prepare a view and view model for a navigation 
    /// request.
    /// </summary>
    public interface IViewInitializer
    {
        /// <summary>
        /// Initializes the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="model">The model.</param>
        /// <param name="request">The request.</param>
        void Prepare(object view, object model, ResolvedNavigationRequest request);

        void NotifyActivated(object view);
        
        void NotifyDeactivating(object view, CancelEventArgs e);
        
        void NotifyDeactivated(object view);
    }
}