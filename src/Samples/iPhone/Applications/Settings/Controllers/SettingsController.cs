using Magellan.Mvc;
using iPhone.Infrastructure.Filters;

namespace iPhone.Applications.Settings.Controllers
{
    public class SettingsController : Controller
    {
        [RoleFilter("Administrators")]
        public ActionResult Index()
        {
            return Page();
        }
    }
}
