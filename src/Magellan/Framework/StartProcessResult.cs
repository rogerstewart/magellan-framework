using System.Diagnostics;
using Magellan.Diagnostics;

namespace Magellan.Framework
{
    /// <summary>
    /// An <see cref="ActionResult"/> that creates a new process.
    /// </summary>
    public class StartProcessResult : ActionResult
    {
        private readonly ProcessStartInfo _startInfo;
        private readonly bool _waitForExit;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartProcessResult"/> class.
        /// </summary>
        /// <param name="startInfo">The start info.</param>
        /// <param name="waitForExit">if set to <c>true</c> this result should wait for the started process 
        /// to exit.</param>
        public StartProcessResult(ProcessStartInfo startInfo, bool waitForExit)
        {
            _startInfo = startInfo;
            _waitForExit = waitForExit;
        }

        /// <summary>
        /// Gets the startup arguments for the process.
        /// </summary>
        /// <value>The start info.</value>
        public ProcessStartInfo StartInfo
        {
            get { return _startInfo; }
        }

        /// <summary>
        /// Gets a value indicating whether this result should wait for the started process to exit.
        /// </summary>
        /// <value><c>true</c> if this result should wait for the started process to exit; 
        /// otherwise, <c>false</c>.</value>
        public bool WaitForExit
        {
            get { return _waitForExit; }
        }

        /// <summary>
        /// Gets the started process (available after render).
        /// </summary>
        /// <value>The process.</value>
        public Process StartedProcess { get; private set; }

        /// <summary>
        /// When implemented in a derived class, performs the bulk of the action rendering.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        protected override void ExecuteInternal(ControllerContext controllerContext)
        {
            TraceSources.MagellanSource.TraceInformation("The StartProcessResult is launching the process '{0}'.", _startInfo.FileName);
            StartedProcess = Process.Start(_startInfo);
            if (_waitForExit)
            {
                TraceSources.MagellanSource.TraceInformation("The StartProcessResult is waiting for the process '{0}' to exit.", _startInfo.FileName);
                StartedProcess.WaitForExit();
            }
        }
    }
}
