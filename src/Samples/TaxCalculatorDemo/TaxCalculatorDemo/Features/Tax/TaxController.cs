using Magellan.Framework;
using TaxCalculatorDemo.Features.Tax.Views.EnterDetails;

namespace TaxCalculatorDemo.Features.Tax
{
    public class TaxController : Controller
    {
        public ActionResult EnterDetails()
        {
            return Page("EnterDetails", new EnterDetailsViewModel());
        }

        public ActionResult Submit()
        {

            return DoNothing();
        }
    }
}
