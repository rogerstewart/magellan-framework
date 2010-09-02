using Magellan.Framework;
using TaxCalculatorDemo.Features.Home.Views.About;
using TaxCalculatorDemo.Features.Home.Views.Index;

namespace TaxCalculatorDemo.Features.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Page("Index", new IndexViewModel());
        }

        public ActionResult About()
        {
            return Dialog("About", new AboutViewModel());
        }
    }
}
