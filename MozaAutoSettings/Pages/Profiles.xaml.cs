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
using Application = Microsoft.UI.Xaml.Application;
using System.Collections.ObjectModel;

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

        private ObservableCollection<ProfileModel> _ProfileList;
        public ObservableCollection<ProfileModel> ProfileList
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


        private ProfileModel _currentlyLoadedProfile;
        public ProfileModel currentlyLoadedProfile
        {
            get => _currentlyLoadedProfile;
            set
            {
                _currentlyLoadedProfile = value;
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
            currentlyLoadedProfile = ProfilesController.getCurrentlyLoadedProfile();
            ProfilesController.readProfilesFromDirectory();
            this.ProfileList = new ObservableCollection<ProfileModel>(ProfilesController.getProfiles());
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
            }
        }

        private void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            updateProfilesList();
        }

        private void saveProfile()
        {
            if (selectedProfile != null)
            {
                isProfileSelected = false;
                ProfilesController.removeProfile(selectedProfile);
                ProfilesController.addProfile(selectedProfile);
                updateProfilesList();
            }
            isProfileSelected = false;
        }

        private async void Save_Clicked(object sender, RoutedEventArgs e)
        {
            saveProfile();


            //show success message
            ContentDialog successDialog = new ContentDialog();
            successDialog.XamlRoot = this.XamlRoot;
            successDialog.Title = "Success";
            successDialog.Content = "Profile saved successfully";
            successDialog.PrimaryButtonText = "Ok";

            await successDialog.ShowAsync();
            return;
        }


        private async void Apply_Clicked(object sender, RoutedEventArgs e)
        {
            saveProfile();
            Tuple<string, bool> result = ProfilesController.applyProfile(selectedProfile);
            if (result.Item2 == false)
            {
                //show error message
                Debug.WriteLine("Error applying profile: " + result.Item1);

                ContentDialog errorDialog = new ContentDialog();
                errorDialog.XamlRoot = this.XamlRoot;
                errorDialog.Title = "Error";
                errorDialog.Content = result.Item1;
                errorDialog.PrimaryButtonText = "Ok";

                await errorDialog.ShowAsync();
                return;
            }
        }

        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedProfile != null)
            {
                ProfilesController.removeProfile(selectedProfile);
                updateProfilesList();
            }
            isProfileSelected = false;
        }

        //constantly update the currently loaded profile
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                currentlyLoadedProfile = ProfilesController.getCurrentlyLoadedProfile();
            };
            timer.Start();
        }

    }
}
