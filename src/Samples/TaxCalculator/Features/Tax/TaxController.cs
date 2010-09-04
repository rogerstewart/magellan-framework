using Magellan.Framework;
using TaxCalculator.Features.Tax.Views.EnterDetails;
using TaxCalculator.Features.Tax.Views.Submit;

namespace TaxCalculator.Features.Tax
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

