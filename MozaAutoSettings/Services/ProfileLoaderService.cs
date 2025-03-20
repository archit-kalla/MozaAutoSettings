using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MozaAutoSettings.Controller;
using MozaAutoSettings.Models;

namespace MozaAutoSettings.Services
{
    class ProfileLoaderService
    {
        private readonly ProfilesController _profilesController;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly TimeSpan _checkInterval;

        private ProfileModel currentlyLoadedProfile;

        public ProfileLoaderService()
        {
            _profilesController = new ProfilesController();
            _cancellationTokenSource = new CancellationTokenSource();
            _checkInterval = TimeSpan.FromSeconds(30); // Check every 30 seconds
            currentlyLoadedProfile = new ProfileModel();
            StartBackgroundTask();
        }

        private void StartBackgroundTask()
        {
            Task.Run(async () => await CheckOpenProgramsAsync(_cancellationTokenSource.Token));
        }

        private async Task CheckOpenProgramsAsync(CancellationToken cancellationToken)
        {
            //update the profile list
            ProfilesController.readProfilesFromDirectory();
            while (!cancellationToken.IsCancellationRequested)
            {
                var openPrograms = GetOpenPrograms();
                foreach (var program in openPrograms)
                {
                    var profile = ProfilesController.getProfile(program);
                    if (profile!=null)
                    {
                        if (currentlyLoadedProfile.Name != profile.Name)
                        {
                            currentlyLoadedProfile = profile;
                            var result = _profilesController.applyProfile(profile);
                            if (result.Item2)
                            {
                                Debug.WriteLine($"Profile {profile.Name} applied successfully.");
                            }
                            else
                            {
                                Debug.WriteLine($"Failed to apply profile {profile.Name}: {result.Item1}");
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"Profile {profile.Name} is already applied.");
                        }

                    }
                }
                await Task.Delay(_checkInterval, cancellationToken);
            }
        }

        private List<string> GetOpenPrograms()
        {
            var processList = Process.GetProcesses();
            var openPrograms = new List<string>();

            foreach (var process in processList)
            {
                try
                {
                    if (process.MainWindowHandle != IntPtr.Zero && process.MainModule != null)
                    {
                        Debug.WriteLine(System.IO.Path.GetFileName(process.MainModule.FileName));

                        //remove the leaading path from the process name and only keep the name of the executable and add it to the list
                        openPrograms.Add(System.IO.Path.GetFileName(process.MainModule.FileName));
                    }
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    // Log the exception and continue
                    Debug.WriteLine($"Access denied to process: {process.ProcessName}. Exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log any other exceptions and continue
                    Debug.WriteLine($"Error accessing process: {process.ProcessName}. Exception: {ex.Message}");
                }
            }
            return openPrograms;
        }

        public void StopBackgroundTask()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}