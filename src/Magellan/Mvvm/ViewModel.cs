using System.ComponentModel;

namespace Magellan.Mvvm
{
    /// <summary>
    /// Serves as a base class for view models in the MVVM pattern.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged, INavigationAware
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
        /// Gets or sets the navigator that can be used for performing subsequent navigation actions.
        /// </summary>
        /// <value></value>
        public INavigator Navigator { get; set; }
    }
}
