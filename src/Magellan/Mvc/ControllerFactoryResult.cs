using System;

namespace Magellan.Mvc
{
    /// <summary>
    /// Returned by controller factories to provide the controller and a custom cleanup action that will be 
    /// invoked after the controller action has been executed.
    /// </summary>
    public class ControllerFactoryResult : IDisposable
    {
        private readonly Action _releaseCallback;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerFactoryResult"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public ControllerFactoryResult(IController controller) : this(controller, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerFactoryResult"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="releaseCallback">The cleanup callback.</param>
        public ControllerFactoryResult(IController controller, Action releaseCallback)
        {
            _releaseCallback = releaseCallback;
            Controller = controller;
        }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        public virtual IController Controller { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_releaseCallback != null) _releaseCallback();
        }
    }
}