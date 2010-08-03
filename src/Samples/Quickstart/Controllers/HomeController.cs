using Magellan.Mvc;
using Quickstart.Views.Home;

namespace Quickstart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Page();
        }

        public ActionResult Add(int a, int b)
        {
            var model = new AddModel
            {
                A = a,
                B = b,
                Result = a + b
            };
            return Page("Add", model);
        }
    }
}
