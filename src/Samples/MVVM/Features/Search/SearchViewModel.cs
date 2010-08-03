using System.Collections.Generic;
using System.Windows.Input;
using Magellan;
using Magellan.Mvvm;
using Sample.Features.Search.Model;
using Sample.Features.Search.ServiceProxies;

namespace Sample.Features.Search
{
    public class SearchViewModel : ViewModel
    {
        private readonly ISearchService _searchService;
        private List<SearchResult> _results;

        public SearchViewModel(ISearchService searchService)
        {
            _searchService = searchService;
            Search = new RelayCommand(SearchExecuted);
            ShowDetails = new RelayCommand<SearchResult>(ShowDetailsExecuted);
            Results = new List<SearchResult>();
        }

        public ICommand Search { get; set; }
        public ICommand ShowDetails { get; set; }
        public string SearchText { get; set; }
        public bool NoResultsFound { get; private set; }

        public List<SearchResult> Results 
        {
            get { return _results; }
            private set
            {
                _results = value;
                NoResultsFound = _results.Count == 0;
                NotifyChanged("Results");
                NotifyChanged("NoResultsFound");
            }
        }
            
        private void SearchExecuted()
        {
            Results = _searchService.Search(SearchText);
        }

        private void ShowDetailsExecuted(SearchResult item)
        {
            Navigator.Navigate("views/details/" + item.Id);
        }
    }
}
