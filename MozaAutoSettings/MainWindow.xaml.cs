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

            contentFrame.Navigate(
                       typeof(MozaAutoSettings.Pages.BlankPage1),
                       null,
                       new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                       );
            LoadUnmanagedDll();
            mozaAPI.mozaAPI.installMozaSDK();
        }


        private void MyNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

            var item = (NavigationViewItem)args.SelectedItem;

            switch ((string)item.Tag)
            {
                case "BlankPage1":
                    contentFrame.Navigate(typeof(BlankPage1));
                    break;
                case "Profiles":
                    contentFrame.Navigate(typeof(Profiles));
                    break;
                case "Page2":
                    //contentFrame.Navigate(typeof(MozaAutoSettings.Pages.Page2));
                    break;
                default:
                    break;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);

        public static void LoadUnmanagedDll()
        {
            SetDllDirectory(@"C:\Users\Archit\source\repos\MozaAutoSettings\MozaAutoSettings\lib"); //TODO change this to dynamic path
        }
    }
}
