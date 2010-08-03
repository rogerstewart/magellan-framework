using Magellan.Mvvm;
using Sample.Features.Details.Model;
using Sample.Features.Details.ServiceProxies;

namespace Sample.Features.Details
{
    public class DetailsViewModel : ViewModel
    {
        private readonly IDetailsService _detailsService;

        public DetailsViewModel(IDetailsService detailsService)
        {
            _detailsService = detailsService;
        }

        public void Initialize(int id)
        {
            Person = _detailsService.GetDetails(id);
        }

        public Person Person { get; set; }
    }
}
