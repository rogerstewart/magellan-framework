using System.Linq;
using System.Windows.Input;
using Magellan;
using Magellan.Mvvm;

namespace MVVM.ViewModels
{
    public class SearchViewModel : ViewModel
    {
        private string _searchText;

        public SearchViewModel()
        {
            Search = new RelayCommand(SearchExecuted);
        }

        public ICommand Search { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; NotifyChanged("SearchText"); }
        }

        private void SearchExecuted()
        {
            // This might be a service call to fetch the search results - for the sake of this sample we'll
            // just generate a list
            var results = Enumerable.Range(1, 20).Select(x => SearchText + " " + x).ToList();

            Navigator.Navigate<ResultsViewModel>(new {results = results});
        }
    }
}
