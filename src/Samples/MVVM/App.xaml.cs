using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Magellan;
using Magellan.Mvvm;
using MVVM.ViewModels;
using MVVM.Views;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var main = new MainWindow();
            var frame = main.MainFrame;

            var models = new ViewModelFactory();
            models.Register("Search", () => new SearchView(), () => new SearchViewModel());

            var routes = new ViewModelRouteCatalog(models);
            routes.MapRoute("{viewModel}");
            
            var navigation = new NavigatorFactory(routes);
            var navigator = navigation.CreateNavigator(frame);

            main.Show();
            navigator.Navigate<SearchViewModel>();
        }
    }
}
