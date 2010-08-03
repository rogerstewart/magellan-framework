using Magellan.Mvc;

namespace iPhone.Applications.Home.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return Page("Home", true);
        }
    }
}
