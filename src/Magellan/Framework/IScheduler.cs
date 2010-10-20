using System;

namespace Magellan.Framework
{
    public interface IScheduler
    {
        ITimer ScheduleOnce(TimeSpan when, Action<ITimer> callback);
        ITimer ScheduleRecurring(TimeSpan when, Action<ITimer> callback);
        void CancelAll();
    }
}