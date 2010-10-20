using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Magellan.Framework
{
    public class Flash : PresentationObject
    {
        private bool _closed;

        public Flash(string message, object additionalData, bool closeable, TimeSpan? expiry)
        {
            Message = message;
            AdditionalData = additionalData;
            Closeable = closeable;
            Expiry = expiry;
            Close = new RelayCommand(CloseExecuted);
        }

        public ICommand Close { get; private set; }
        public string Message { get; private set; }
        public object AdditionalData { get; private set; }
        public bool Closeable { get; private set; }
        public TimeSpan? Expiry { get; private set; }

        public bool Closed
        {
            get { return _closed; }
            set { _closed = value; NotifyChanged("Closed"); }
        }

        private void CloseExecuted()
        {
            Closed = true;
        }
    }
}