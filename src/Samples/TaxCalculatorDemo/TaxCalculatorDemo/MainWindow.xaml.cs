using System.Windows;
using System.Windows.Input;
using Magellan;

namespace TaxCalculatorDemo
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
