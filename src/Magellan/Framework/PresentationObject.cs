using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Magellan.Abstractions;
using Magellan.ComponentModel;

namespace Magellan.Framework
{
    /// <summary>
    /// Serves as a base class for the "third object" in seperated presentation patterns - view models, 
    /// presenters, and so on.  
    /// </summary>
    public abstract class PresentationObject : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly BusyState _busyState = new BusyState();
        private readonly ValidationMessageDictionary _validationMessages = new ValidationMessageDictionary();

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationObject"/> class.
        /// </summary>
        protected PresentationObject()
        {
            _busyState.PropertyChanged += (x, y) => NotifyChanged("IsBusy");
            _validationMessages.ErrorsChanged += (x, y) => NotifyChanged("");
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the <see cref="BusyState"/> that tracks whether background operations are running within 
        /// this presentation object.
        /// </summary>
        /// <value>The state of the busy.</value>
        public BusyState BusyState
        {
            get { return _busyState; }
        }

        /// <summary>
        /// Gets a value indicating whether any background operations are in progress.
        /// </summary>
        /// <value><c>true</c> if this presentation object is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return BusyState.IsBusy; }
        }

        /// <summary>
        /// Raises a property changed events for a given set of properties.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="otherProperties">The other properties.</param>
        protected void NotifyChanged(string propertyName, params string[] otherProperties)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            foreach (var other in otherProperties)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(other));    
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Gets the validation messages associated with this object.
        /// </summary>
        /// <value>The validation messages.</value>
        public ValidationMessageDictionary ValidationMessages
        {
            get { return _validationMessages; }
        }

        public string this[string columnName]
        {
            get { return string.Join(Environment.NewLine, ValidationMessages[columnName].ToArray()); }
        }

        public string Error
        {
            get { return string.Join(Environment.NewLine, ValidationMessages.AllMessages()); }
        }
    }

    /// <summary>
    /// Maintains a list of validation message objects associated with the property name (key) that is associated with 
    /// each error.
    /// </summary>
    public class ValidationMessageDictionary
    {
        private readonly Dictionary<string, ValidationMessageCollection> _items = new Dictionary<string, ValidationMessageCollection>();
        private readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessageDictionary"/> class.
        /// </summary>
        public ValidationMessageDictionary()
        {
            
        }

        /// <summary>
        /// Occurs when the set of errors changes.
        /// </summary>
        public event EventHandler ErrorsChanged;

        /// <summary>
        /// Gets the validation messages (a collection) associated with the specified key, usually a property name.
        /// </summary>
        public ValidationMessageCollection this[string key]
        {
            get
            {
                lock (_lock)
                {
                    if (!_items.ContainsKey(key))
                    {
                        _items[key] = new ValidationMessageCollection();
                        _items[key].CollectionChanged += (x, y) => OnErrorsChanged(EventArgs.Empty);
                    }
                    return _items[key];
                }
            }
        }

        /// <summary>
        /// Gets all error messages for every key.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> AllMessages()
        {
            return _items.Values.SelectMany(x => x);
        }

        /// <summary>
        /// Gets all available keys.
        /// </summary>
        /// <value>The keys.</value>
        public string[] Keys
        {
            get { return _items.Keys.ToArray(); }
        }

        /// <summary>
        /// Adds an error message for the specified key.
        /// </summary>
        /// <param name="key">The key, usually a property name.</param>
        /// <param name="message">The error message or object.</param>
        public void Add(string key, object message)
        {
            this[key].Add(message);
        }

        /// <summary>
        /// Removes all validation errors.
        /// </summary>
        public void Clear()
        {
            List<ValidationMessageCollection> items;
            lock (_lock)
            {
                items = _items.Values.ToList();
                _items.Clear();
            }

            foreach (var item in items)
            {
                item.Clear();
            }
            OnErrorsChanged(EventArgs.Empty);
        }

        protected void OnErrorsChanged(EventArgs e)
        {
            var handler = ErrorsChanged;
            if (handler != null) handler(this, e);
        }
    }

    public class ValidationMessageCollection : DispatchedObservableCollection<object>
    {
        
    }

    /// <summary>
    /// An ObservableCollection that raises all events on the dispatcher (UI) thread.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DispatchedObservableCollection<T> : ObservableCollection<T>, INotifyCollectionChanged
    {
        private bool _raiseCollectionChanged = true;
        private IDispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchedObservableCollection&lt;T&gt;"/> class.
        /// </summary>
        public DispatchedObservableCollection() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchedObservableCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="collection"/> parameter cannot be null.
        /// </exception>
        public DispatchedObservableCollection(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

        /// <summary>
        /// Gets or sets the dispatcher that collection changed events will be sent to.
        /// </summary>
        /// <value>The dispatcher.</value>
        public IDispatcher Dispatcher
        {
            get { return _dispatcher ?? (_dispatcher = GetDefaultDispatcher()); }
            set { _dispatcher = value; }
        }

        private static IDispatcher GetDefaultDispatcher()
        {
            var dispatcher = Application.Current == null
                ? System.Windows.Threading.Dispatcher.CurrentDispatcher
                : Application.Current.Dispatcher;

            return new DispatcherWrapper(dispatcher);
        }

        /// <summary>
        /// Inserts the item to the specified position.
        /// </summary>
        /// <param name="index">The index to insert at.</param>
        /// <param name="item">The item to be inserted.</param>
        protected override void InsertItem(int index, T item)
        {
            Dispatcher.Dispatch(() => InsertItemBase(index, item));
        }

        /// <summary>
        /// Exposes the base implementation fo the <see cref="InsertItem"/> function.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <remarks>Used to avoid compiler warning regarding unverificable code.</remarks>
        private void InsertItemBase(int index, T item)
        {
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Moves the item within the collection.
        /// </summary>
        /// <param name="oldIndex">The old position of the item.</param>
        /// <param name="newIndex">The new position of the item.</param>
        protected override void MoveItem(int oldIndex, int newIndex)
        {
            Dispatcher.Dispatch(() => MoveItemBase(oldIndex, newIndex));
        }

        /// <summary>
        /// Exposes the base implementation fo the <see cref="MoveItem"/> function.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        /// <remarks>Used to avoid compiler warning regarding unverificable code.</remarks>
        private void MoveItemBase(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
        }

        /// <summary>
        /// Sets the item at the specified position.
        /// </summary>
        /// <param name="index">The index to set the item at.</param>
        /// <param name="item">The item to set.</param>
        protected override void SetItem(int index, T item)
        {
            Dispatcher.Dispatch(() => SetItemBase(index, item));
        }

        /// <summary>
        /// Exposes the base implementation fo the <see cref="SetItem"/> function.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <remarks>Used to avoid compiler warning regarding unverificable code.</remarks>
        private void SetItemBase(int index, T item)
        {
            base.SetItem(index, item);
        }

        /// <summary>
        /// Removes the item at the specified position.
        /// </summary>
        /// <param name="index">The position used to identify the item to remove.</param>
        protected override void RemoveItem(int index)
        {
            Dispatcher.Dispatch(() => RemoveItemBase(index));
        }

        /// <summary>
        /// Exposes the base implementation fo the <see cref="RemoveItem"/> function.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <remarks>Used to avoid compiler warning regarding unverificable code.</remarks>
        private void RemoveItemBase(int index)
        {
            base.RemoveItem(index);
        }

        /// <summary>
        /// Clears the items contained by the collection.
        /// </summary>
        protected override void ClearItems()
        {
            Dispatcher.Dispatch(ClearItemsBase);
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="ClearItems"/> function.
        /// </summary>
        /// <remarks>Used to avoid compiler warning regarding unverificable code.</remarks>
        private void ClearItemsBase()
        {
            base.ClearItems();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged"/> event with the provided arguments.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_raiseCollectionChanged)
                base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddRange(IEnumerable<T> items)
        {
            _raiseCollectionChanged = false;
            foreach (var item in items)
                Add(item);
            _raiseCollectionChanged = true;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="items">The items.</param>
        public void RemoveRange(IEnumerable<T> items)
        {
            _raiseCollectionChanged = false;
            foreach (var item in items)
                Remove(item);
            _raiseCollectionChanged = true;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}