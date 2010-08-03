using System;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows;

namespace Magellan.Abstractions
{
    /// <summary>
    /// This dispatcher is used at runtime by both Windows Forms and WPF. The WPF Dispatcher class works 
    /// within Windows Forms, so this appears to be safe.
    /// </summary>
    public class DispatcherWrapper : IDispatcher
    {
        private readonly Dispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherWrapper"/> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        public DispatcherWrapper(Dispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                if (Application.Current != null)
                {
                    dispatcher = Application.Current.Dispatcher;
                }
                else
                {
                    dispatcher = Dispatcher.CurrentDispatcher;
                }
            }
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Dispatches the specified action to the thread.
        /// </summary>
        /// <param name="actionToInvoke">The action to invoke.</param>
        [DebuggerNonUserCode]
        public void Dispatch(Action actionToInvoke)
        {
            if (!DispatchRequired())
            {
                actionToInvoke();
            }
            else
            {
                _dispatcher.Invoke(DispatcherPriority.Normal, actionToInvoke);   
            }
        }

        /// <summary>
        /// Dispatches the specified action to the thread.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <returns></returns>
        [DebuggerNonUserCode]
        public TResult Dispatch<TResult>(Func<TResult> actionToInvoke)
        {
            if (_dispatcher.CheckAccess())
            {
                return actionToInvoke();
            }
            return (TResult)_dispatcher.Invoke(DispatcherPriority.Normal, actionToInvoke);
        }

        /// <summary>
        /// Checks whether the thread invoking the method.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode]
        public bool DispatchRequired()
        {
            return !_dispatcher.CheckAccess();
        }
    }
}