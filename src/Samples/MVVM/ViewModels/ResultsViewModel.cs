using System.Collections.Generic;
using System.Collections.ObjectModel;
using Magellan.Mvvm;

namespace MVVM.ViewModels
{
    public class ResultsViewModel : ViewModel
    {
        private readonly ObservableCollection<string> _results = new ObservableCollection<string>();

        public ResultsViewModel(IEnumerable<string> results)
        {
            foreach (var result in results)
            {
                _results.Add(result);
            }
        }

        public ObservableCollection<string> Results
        {
            get { return _results; }
        }
    }
}
