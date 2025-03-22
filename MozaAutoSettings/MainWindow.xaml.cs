using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Devices.Enumeration;
using MozaAutoSettings.Pages;
using System.Runtime.InteropServices;
using MozaAutoSettings.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MozaAutoSettings
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            
        }


        private void MyNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

            var item = (NavigationViewItem)args.SelectedItem;

            switch ((string)item.Tag)
            {

                case "Profiles":
                    contentFrame.Navigate(typeof(Profiles),
                        null,
                        new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
                    break;
                case "Current":
                    contentFrame.Navigate(typeof(CurrentSettings),
                        null,
                        new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
                    break;
                default:
                    break;
            }
        }

    }
}
