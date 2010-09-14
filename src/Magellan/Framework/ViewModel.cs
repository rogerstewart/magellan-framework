
using System.ComponentModel;
using Magellan.Abstractions;

namespace Magellan.Framework
{
    /// <summary>
    /// A base class for view models in the MVVM pattern.
    /// </summary>
    public abstract class ViewModel : PresentationObject, IViewAware, INavigationAware
    {
        private object _view;

        /// <summary>
        /// Gets the dispatcher that owns this presentation object.
        /// </summary>
        /// <value>The dispatcher.</value>
        public IDispatcher Dispatcher { get { return Navigator.Dispatcher; } }

        /// <summary>
        /// Gets or sets the navigator that can be used for performing subsequent navigation actions.
        /// </summary>
        /// <value></value>
        public INavigator Navigator { get; set; }

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
        /// Called when the view attached to this ViewModel raises the Loaded event. For a Window, this will be called 
        /// once. For a Page, it may be called multiple times, for example, if the user navigates to the page, then clicks
        /// 'Back', then clicks 'Forward', returning to the page.  
        /// </summary>
        protected virtual void Activated()
        {
        }

        /// <summary>
        /// Called when the view attached to this ViewModel is being closed, allowing for cancellation. For a Window, this 
        /// maps to the Closing event. For a Page, it is the Navigating event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void Deactivating(CancelEventArgs e)
        {
        }

        /// <summary>
        /// Called when the view attached to this ViewModel is closed. For a Window, this maps to the Closed event. For a 
        /// Page, it maps to the Navigated event, when the page is unloaded. Remember that in the case of pages, if the user 
        /// clicks 'Back' and then 'Forward', the view will be re-activated and thus re-deactivated.
        /// </summary>
        protected virtual void Deactivated()
        {
        }

        void IViewAware.ViewAttached(object view)
        {
            _view = view;
            ViewAttached(view);
        }

        void IViewAware.Activated()
        {
            Activated();
        }

        void IViewAware.Deactivating(CancelEventArgs e)
        {
            Deactivating(e);
        }

        void IViewAware.Deactivated()
        {
            Deactivated();
        }
    }
}
