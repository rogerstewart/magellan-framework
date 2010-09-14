using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Magellan.ComponentModel;

namespace Magellan.Framework
{
    public class FlashCollection : DispatchedObservableCollection<Flash>
    {
        private readonly IScheduler _scheduler;
        private readonly Dictionary<Flash, ITimer> _timers = new Dictionary<Flash, ITimer>();

        public FlashCollection(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        protected override void InsertItem(int index, Flash item)
        {
            item.PropertyChanged += ItemPropertyChanged;
            if (item.Expiry != null)
            {
                var timer = _scheduler.ScheduleOnce(item.Expiry.Value, x => Remove(item));
                _timers[item] = timer;
            }
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (index >= 0 && index < Count)
            {
                var item = this[index];
                if (_timers.ContainsKey(item))
                {
                    var timer = _timers[item];
                    timer.Cancel();
                }
            }
            base.RemoveItem(index);
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Closed")
            {
                var item = (Flash)sender;
                Remove(item);
            }
        }
    }
}
