using System;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MozaAutoSettings.Services;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MozaAutoSettings.Dialogues
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SaveToProfileDialogue : Page
    {
        public string SelectedFileName { get; private set; }
        public string ProfileName => profileName.Text;
        public SaveToProfileDialogue()
        {
            this.InitializeComponent();
        }
        private async void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            //disable the button to avoid double-clicking
            var senderButton = sender as Button;
            senderButton.IsEnabled = false;

            // Clear previous returned file name, if it exists, between iterations of this scenario
            PickAFileOutputTextBlock.Text = "";

            // Create a file picker
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.GetWindow());

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add(".exe");

            // Open the picker for the user to pick a file
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                PickAFileOutputTextBlock.Text = "Picked file: " + file.Name;
                SelectedFileName = file.Name;

            }
            else
            {
                PickAFileOutputTextBlock.Text = "Operation cancelled.";
                SelectedFileName = "";
            }

            //re-enable the button
            senderButton.IsEnabled = true;
        }
    }
}
