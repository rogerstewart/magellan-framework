using System.Windows.Controls;
using Magellan;
using Magellan.Mvc;

namespace iPhone.Applications.Phone.Views
{
    public partial class IndexView : Page, INavigationAware
    {
        public IndexView()
        {
            InitializeComponent();
            Loaded += (x, y) =>
            {
                Navigator.Factory.CreateNavigator(ContactsFrame).Navigate(new { controller = "Phone", action = "Groups" });
                Navigator.Factory.CreateNavigator(KeypadFrame).Navigate(new {controller = "Phone", action = "Keypad"});
            };
        }

        public INavigator Navigator { get; set; }
    }
}
