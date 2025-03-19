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
using System.ComponentModel;
using System.Runtime.CompilerServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MozaAutoSettings.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profiles : Page, INotifyPropertyChanged
    {
        private ProfilesController _profilesController = new ProfilesController();

        
        public List<int> wheelAngles = new List<int>() { 360, 540, 900, 1080, 1440, 1800 };
        public List<int> roadSensitivities = new List<int>() { 10, 14, 18, 22, 26, 30, 34, 38, 42, 46, 50 };

        private List<ProfileModel> _ProfileList;
        public List<ProfileModel> ProfileList
        {
            get => _ProfileList;
            set
            {
                _ProfileList = value;
                OnPropertyChanged();
            }
        }

        private ProfileModel _selectedProfile;
        public ProfileModel selectedProfile
        {
            get => _selectedProfile;
            set
            {
                _selectedProfile = value;
                OnPropertyChanged();
            }
        }

        private bool _isProfileSelected;
        public bool isProfileSelected
        {
            get => _isProfileSelected;
            set
            {
                _isProfileSelected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Profiles()
        {
            this.InitializeComponent();

            updateProfilesList();
            this.isProfileSelected = false;
            selectedProfile = new ProfileModel();
            this.DataContext = this;

        }

        private void updateProfilesList()
        {
            this.ProfileList = _profilesController.getProfiles();
            profileListView.ItemsSource = this.ProfileList;
        }

        private void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            //refreshCurrentSettings();
            updateProfilesList();
            this.DataContext = this; //probably not needed but just in case
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            //saveProfile();
            if (selectedProfile != null)
            {   
                _profilesController.removeProfile(selectedProfile);
                _profilesController.addProfile(selectedProfile);
                updateProfilesList();
            }
        }

        private void profileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (profileListView.SelectedItem != null)
            {
                this.selectedProfile = (ProfileModel)profileListView.SelectedItem;
                this.isProfileSelected = true;
                if (selectedProfile != null)
                {
                    Debug.WriteLine("Selected profile: " + selectedProfile.Name);
                }
                this.DataContext = this;
            }


        }
    }
}
