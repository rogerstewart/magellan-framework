using Magellan.Framework;
using TaxCalculator.Features.Home.Views.About;
using TaxCalculator.Features.Home.Views.Index;

namespace TaxCalculator.Features.Home
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
