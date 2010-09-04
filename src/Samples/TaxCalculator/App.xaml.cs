using System.Windows;
using Magellan;
using Magellan.Framework;
using TaxCalculator.Features.Home;
using TaxCalculator.Features.Tax;

namespace TaxCalculator
{
    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var controllerFactory = new AsyncControllerFactory();
            controllerFactory.Register("Home", () => new HomeController());
            controllerFactory.Register("Tax", () => new TaxController());

            var routes = new ControllerRouteCatalog(controllerFactory);
            routes.MapRoute("{controller}/{action}/{id}", new {controller = "Home", action = "Index", id = ""});

            var factory = new NavigatorFactory("tax", routes);
            var mainWindow = new MainWindow(factory);
            mainWindow.MainNavigator.Navigate<HomeController>(x => x.Index());
            mainWindow.Show();
        }
    }
}
