using Magellan.Framework;
using TaxCalculator.Features.Tax.Model;
using TaxCalculator.Features.Tax.Views.EnterDetails;
using TaxCalculator.Features.Tax.Views.Submit;

namespace TaxCalculator.Features.Tax
{
    public class TaxController : Controller
    {
        private readonly ITaxEstimatorSelector _estimatorSelector;

        public TaxController(ITaxEstimatorSelector estimatorSelector)
        {
        	// Inject the TaxEstimateSelector so it can be used by the Submit() action
            _estimatorSelector = estimatorSelector;
        }

        // Route: /Tax/EnterDetails
        public ActionResult EnterDetails()
        {
            return Page("EnterDetails", new EnterDetailsViewModel());
        }

        // Route: /Tax/Submit
        public ActionResult Submit(TaxPeriod period, decimal grossIncome)
        {
            var situation = new Situation(grossIncome);
            var estimator = _estimatorSelector.Select(period);
            var estimate = estimator.Estimate(situation);

            return Page("Submit", new SubmitViewModel(estimate));
        }
    }
}

