using Magellan.Framework;

namespace TaxCalculatorDemo.Features.Tax.Views.Submit
{
    public class SubmitViewModel : ViewModel
    {
        public SubmitViewModel(decimal taxableIncome)
        {
            TaxableIncome = taxableIncome;
        }

        public decimal TaxableIncome { get; set; }
    }
}
