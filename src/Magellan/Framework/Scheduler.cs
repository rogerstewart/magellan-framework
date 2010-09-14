using System;
using System.Collections.Generic;

namespace Magellan.Framework
{
    public class Scheduler : IScheduler
    {
        private readonly List<WeakReference> _activeTimers = new List<WeakReference>();

        public Scheduler()
        {
        }

        public ITimer ScheduleOnce(TimeSpan when, Action<ITimer> callback)
        {
            return Schedule(when, callback, false);
        }

        public ITimer ScheduleRecurring(TimeSpan when, Action<ITimer> callback)
        {
            return Schedule(when, callback, true);
        }

        private ITimer Schedule(TimeSpan when, Action<ITimer> callback, bool recurring)
        {
            var timer = new TimerImplementation(when, callback, recurring);
            timer.Start();
            _activeTimers.Add(new WeakReference(timer));
            return timer;
        }

        public void CancelAll()
        {
            foreach (var timer in _activeTimers)
            {
                var instance = timer.Target as TimerImplementation;
                if (instance == null)
                    continue;

                instance.Cancel();
            }
        }
    }
}