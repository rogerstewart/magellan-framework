using System.Windows;
using Magellan;

namespace TaxCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow(NavigatorFactory navigation)
        {
            InitializeComponent();

            MainNavigator = navigation.CreateNavigator(MainFrame);
        }

        public INavigator MainNavigator { get; set; }
    }
}
