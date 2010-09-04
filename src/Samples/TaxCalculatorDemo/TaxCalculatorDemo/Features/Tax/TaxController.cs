using Magellan.Framework;
using TaxCalculatorDemo.Features.Tax.Views.EnterDetails;
using TaxCalculatorDemo.Features.Tax.Views.Submit;

namespace TaxCalculatorDemo.Features.Tax
{
    public class TaxController : Controller
    {
        public ActionResult EnterDetails()
        {
            return Page("EnterDetails", new EnterDetailsViewModel());
        }

        public ActionResult Submit(EnterDetailsViewModel details)
        {
            return Page("Submit", new SubmitViewModel(details.GrossIncome));
        }
    }
}

