using System;
using Magellan.Framework;
using TaxCalculatorDemo.Areas.Tax.Views.EnterDetails;

namespace TaxCalculatorDemo.Areas.Tax
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
