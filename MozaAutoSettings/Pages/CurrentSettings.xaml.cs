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
using MozaAutoSettings.Controller;
using MozaAutoSettings.Models;
using System.Diagnostics;
using MozaAutoSettings.Services;
using MozaAutoSettings.Dialogues;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MozaAutoSettings.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrentSettings : Page
    {
        private CurrentSettingsController currentSettingsController;
        public WheelBaseSettingsModel currentWheelBaseSettings { get; set; }

        public List<int> wheelAngles = new List<int>() { 360, 540, 900, 1080, 1440, 1800 };
        public List<int> roadSensitivities = new List<int> () { 10, 14, 18, 22, 26, 30, 34, 38, 42, 46, 50 };
        public CurrentSettings()
        {
            this.InitializeComponent();
            if (this.currentSettingsController == null)
            {
                this.currentSettingsController = new CurrentSettingsController();
            }
            this.currentWheelBaseSettings = this.currentSettingsController.getCurrentWheelBaseSettings();

            //write debug for currentwheelbase settings
            Console.WriteLine("Current Wheel Base Settings:");
            if (this.currentWheelBaseSettings == null)
            {
                Console.WriteLine("No settings found.");
                return;
            }
            else
            {
                Debug.WriteLine("Motor Limit Angle: " + this.currentWheelBaseSettings.MotorLimitAngle.Item1);
                Debug.WriteLine("Motor Limit Angle: " + this.currentWheelBaseSettings.MotorLimitAngle.Item2);
                Debug.WriteLine("Motor Road Sensitivity: " + this.currentWheelBaseSettings.MotorRoadSensitivity);
                Debug.WriteLine("Motor FFB Strength: " + this.currentWheelBaseSettings.MotorFfbStrength);
                Debug.WriteLine("Motor Limit Wheel Speed: " + this.currentWheelBaseSettings.MotorLimitWheelSpeed);
                Debug.WriteLine("Motor Spring Strength: " + this.currentWheelBaseSettings.MotorSpringStrength);
                Debug.WriteLine("Motor Natural Damper: " + this.currentWheelBaseSettings.MotorNaturalDamper);
                Debug.WriteLine("Motor Natural Friction: " + this.currentWheelBaseSettings.MotorNaturalFriction);
                Debug.WriteLine("Motor Speed Damping: " + this.currentWheelBaseSettings.MotorSpeedDamping);
                Debug.WriteLine("Motor Peak Torque: " + this.currentWheelBaseSettings.MotorPeakTorque);
                Debug.WriteLine("Motor Natural Inertia Ratio: " + this.currentWheelBaseSettings.MotorNaturalInertiaRatio);
                Debug.WriteLine("Motor Natural Inertia: " + this.currentWheelBaseSettings.MotorNaturalInertia);
                Debug.WriteLine("Motor Speed Damping Start Point: " + this.currentWheelBaseSettings.MotorSpeedDampingStartPoint);
                Debug.WriteLine("Motor Hands Off Protection: " + this.currentWheelBaseSettings.MotorHandsOffProtection);
                Debug.WriteLine("Motor FFB Reverse: " + this.currentWheelBaseSettings.MotorFfbReverse);
                Debug.WriteLine("Motor Equalizer Amp: " + string.Join(", ", this.currentWheelBaseSettings.MotorEqualizerAmp.Select(kv => kv.Key + ": " + kv.Value)));
                Debug.WriteLine(
                    "Equalizer 7.5: " + this.currentWheelBaseSettings.EqualizerAmp7_5 +
                    " Equalizer 13: " + this.currentWheelBaseSettings.EqualizerAmp13 +
                    " Equalizer 22.5: " + this.currentWheelBaseSettings.EqualizerAmp22_5 +
                    " Equalizer 39: " + this.currentWheelBaseSettings.EqualizerAmp39 +
                    " Equalizer 55: " + this.currentWheelBaseSettings.EqualizerAmp55 +
                    " Equalizer 100: " + this.currentWheelBaseSettings.EqualizerAmp100
                );
            }
            

            this.DataContext = this.currentWheelBaseSettings;

        }

        private void Apply_Clicked(object sender, RoutedEventArgs e)
        {
            this.currentSettingsController.sendSettingsToWheelBase(this.currentWheelBaseSettings);
        }

        private void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            this.currentWheelBaseSettings = this.currentSettingsController.getCurrentWheelBaseSettings();
        }

        private async void Save_Clicked(object sender, RoutedEventArgs e)
        {
            // popup save dialog
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Save to Profile";
            dialog.PrimaryButtonText = "Save";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var saveToProfileDialogue = new SaveToProfileDialogue();
            dialog.Content = saveToProfileDialogue;

            var result = await dialog.ShowAsync();


            if (result == ContentDialogResult.Primary)
            {
                // save to profile
                Debug.WriteLine("Save to profile");
                // get the selected profile from the dialogue
                var selectedFileName = saveToProfileDialogue.SelectedFileName;
                if (!string.IsNullOrEmpty(selectedFileName))
                {
                    Debug.WriteLine("Selected file: " + selectedFileName);
                    // Use the selected file name as needed
                }
                else
                {
                    Debug.WriteLine("file not selected or cancelled file pick");
                }
                    var profileName = saveToProfileDialogue.ProfileName;
                if (!string.IsNullOrEmpty(profileName))
                {
                    Debug.WriteLine("Profile name: " + profileName);
                    // Use the profile name as needed
                }

            }
            else
            {
                // cancel
                Debug.WriteLine("Cancel");
            }

        }
    }
}
