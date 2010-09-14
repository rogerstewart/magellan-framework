
using System;
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
        /// Called when the view attached to this ViewModel raises the Loaded event.
        /// </summary>
        protected virtual void Activating()
        {
            
        }

        void IViewAware.ViewAttached(object view)
        {
            _view = view;
            ViewAttached(view);
        }

        void IViewAware.Activating()
        {
            Activating();
        }

        void IViewAware.Activated()
        {
            
        }

        void IViewAware.Deactivating(CancelEventArgs e)
        {
            
        }

        void IViewAware.Deactivated()
        {
            
        }
    }
}
