using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Magellan;
using Magellan.Behaviors;
using Magellan.Controls;

namespace XamlReaderParseTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var pc = new ParserContext();
            //pc.XamlTypeMapper = XamlTypeMapper.DefaultMapper;
            //pc.XamlTypeMapper = new XamlTypeMapper(new string[] { "Magellan", typeof(Button).Assembly.FullName, "PresentationCore", "WindowsBase" });
            //pc.XamlTypeMapper.AddMappingProcessingInstruction("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Magellan.Behaviors", "Magellan");
            //pc.XamlTypeMapper.AddMappingProcessingInstruction("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "System.Windows.Controls", typeof(Button).Assembly.FullName);
            //pc.XamlTypeMapper.AddMappingProcessingInstruction("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "System.Windows.Controls", "PresentationCore");
            //pc.XamlTypeMapper.AddMappingProcessingInstruction("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "System.Windows.Controls", "WindowsBase");

            var button = XamlReader.Parse(
                @"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
	                xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                    xmlns:i='clr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity'
                    xmlns:magellan='clr-namespace:Magellan.Behaviors;assembly=Magellan.Mvc'
                    xmlns:magellanx='clr-namespace:Magellan.Behaviors;assembly=Magellan'
                    Name='MyButton'
                    Content='Bye!'
                    >
                    <i:Interaction.Triggers>
                        <i:EventTrigger EventName='Click'>
                            <magellan:NavigateControllerAction x:Name='NavBehavior' Controller='MyController' Action='MyAction'>
                                <magellan:NavigateControllerAction.Parameters>
                                    <magellanx:Parameter ParameterName='btn' Value='{Binding ElementName=MyButton}' />
                                    <magellanx:Parameter ParameterName='btnContent' Value='{Binding ElementName=MyButton, Path=Content}' />
                                    <magellanx:Parameter ParameterName='x' Value='Wazoo' />
                                </magellan:NavigateControllerAction.Parameters>
                            </magellan:NavigateControllerAction>
                        </i:EventTrigger>
                    </i:Interaction.Triggers>
                </Button>"

                //,pc
                
                );

            Content = button;
        }
    }
}
