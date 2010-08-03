using System.Windows;
using Magellan;
using Magellan.Mvc;
using Quickstart.Controllers;

namespace Quickstart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var controllerFactory = new ControllerFactory();
            controllerFactory.Register("Home", () => new HomeController());

            var routes = new ControllerRouteCatalog(controllerFactory);
            routes.MapRoute("{controller}/{action}");

            var main = new Window1();
            var navigation = new NavigatorFactory(routes);
            var navigator = navigation.CreateNavigator(main.mainFrame);

            navigator.Navigate(new { controller = "Home", action = "Index" });
            main.Show();

            base.OnStartup(e);
        }
    }
}
