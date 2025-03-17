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
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using MozaAutoSettings.Controller;
using MozaAutoSettings.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MozaAutoSettings.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profiles : Page
    {
        private ProfilesController _profilesController = new ProfilesController();
        //public List<String> Processes
        //{
        //    get { return _profilesController.processes; }
        //}

        public List<ProfileModel> ProfileList
        {
            get { return _profilesController.ProfileList; }
        }

        public Profiles()
        {
            this.InitializeComponent();
        }

        //public void refreshProcessesButtonClick(object sender, RoutedEventArgs e)
        //{
        //    _profilesController.refreshProceses();
        //    //Debug.WriteLine("refreshing profiles");
        //    //Debug.WriteLine(_profilesController.processes.Count);
        //    //foreach (String p in _profilesController.processes)
        //    //{
        //    //    Debug.WriteLine(p);
        //    //}
        //}



    }
}
