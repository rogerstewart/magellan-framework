using System;
using System.Collections.Generic;
using System.Threading;
using Magellan.Abstractions;

namespace Magellan.Tests.Helpers
{
    /// <summary>
    /// Represents an <see cref="IDispatcher"/> used for tests. Since test frameworks
    /// do not have message pumps normally used by dispatchers, this dispatcher queues 
    /// actions up to be executed manually.
    /// </summary>
    public class ManualPumpDispatcher : IDispatcher
    {
        private readonly List<Thread> _dispatcherThreads = new List<Thread>();
        private readonly Thread _creationThread = Thread.CurrentThread;
        private readonly Queue<Action> _actionsToDispatch = new Queue<Action>();

        /// <summary>
        /// Dispatches the specified action to the thread.
        /// </summary>
        /// <param name="actionToInvoke">The action to invoke.</param>
        public void Dispatch(Action actionToInvoke)
        {
            _actionsToDispatch.Enqueue(actionToInvoke);
        }

        /// <summary>
        /// Processes all actions that were queued for dispatching.
        /// </summary>
        public void Pump()
        {
            foreach (var action in _actionsToDispatch)
            {
                action();
            }
        }

        /// <summary>
        /// Dispatches the specified action to invoke.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <returns></returns>
        public TResult Dispatch<TResult>(Func<TResult> actionToInvoke)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Checks whether the thread invoking the method .
        /// </summary>
        /// <returns></returns>
        public bool DispatchRequired()
        {
            return
                Thread.CurrentThread != _creationThread
                && _dispatcherThreads.Contains(Thread.CurrentThread) == false;
        }
    }
}