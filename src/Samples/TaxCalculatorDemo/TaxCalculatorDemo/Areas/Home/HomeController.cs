using Magellan.Framework;
using TaxCalculatorDemo.Areas.Home.Views.About;
using TaxCalculatorDemo.Areas.Home.Views.Index;

namespace TaxCalculatorDemo.Areas.Home
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
