using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Magellan.Abstractions;

namespace Magellan.Controls
{
    public class NavigationFrame : ContentControl, INavigationService, INavigate
    {
        public event NavigatingCancelEventHandler Navigating;
        public event NavigatedEventHandler Navigated;
        public event NavigationFailedEventHandler NavigationFailed;

        public bool CanGoBack
        {
            get { return true; }
        }

        public bool CanGoForward
        {
            get { return true; }
        }

        public void GoBack()
        {
            
        }

        public void GoForward()
        {
            
        }

        public bool NavigateTo(object content)
        {
            Content = content;
            return true;
        }

        IDispatcher INavigationService.Dispatcher
        {
            get { return new DispatcherWrapper(Dispatcher); }
        }

        public bool Navigate(Uri source)
        {
            Application.Current.Host.NavigationState = source.ToString();
            return true;
        }
    }
}
