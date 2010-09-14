using System;
using System.Diagnostics;
using System.Threading;

namespace Magellan.Framework
{
    public class TimerImplementation : ITimer, IDisposable
    {
        private readonly TimeSpan _when;
        private readonly Action<ITimer> _callback;
        private readonly bool _recurring;
        private readonly Timer _realTimer;
        private bool _manuallyCancelled;
        private bool _isDisposed;
        private readonly object _lock = new object();

        public TimerImplementation(TimeSpan when, Action<ITimer> callback, bool recurring)
        {
            _when = when;
            _callback = callback;
            _recurring = recurring;
            _realTimer = new Timer(Ticked, null, TimeSpan.FromMilliseconds(-1), when);
        }

        private void Ticked(object state)
        {
            Pause();

            _callback(this);

            lock (_lock)
            {
                if (_recurring)
                {
                    Resume();
                }
                else
                {
                    Dispose();
                }
            }
        }

        public void Start()
        {
            Resume();
        }

        private void Pause()
        {
            lock (_lock)
            {
                if (_isDisposed)
                    return;

                _realTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
            }
        }

        private void Resume()
        {
            lock (_lock)
            {
                if (_isDisposed || _manuallyCancelled)
                    return;

                _realTimer.Change(_when, _when);
            }
        }

        public void Cancel()
        {
            lock (_lock)
            {
                _manuallyCancelled = true;
                Dispose();
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_isDisposed)
                    return;

                Pause();
                _realTimer.Dispose();
                _isDisposed = true;
                Trace.WriteLine("Disposed: " + GetType().Name);
            }
        }
    }
}