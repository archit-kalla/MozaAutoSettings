using mozaAPI;
using MozaAutoSettings.Models;
using MozaAutoSettings.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaAutoSettings.Controller
{
    class ProfilesController
    {
        private static List<ProfileModel> ProfileList { get; set; } = new List<ProfileModel>();
        private static readonly object ProfileListLock = new object();

        private static readonly string ProfileDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MozaAutoSettings", "Profiles");

        public static ProfileModel CurrentLoadedProfile { get; set; } = new ProfileModel();

        public ProfilesController()
        {
            Debug.WriteLine("Created profile directory " + ProfileDirectory);
            if (!System.IO.Directory.Exists(ProfileDirectory))
            {
                System.IO.Directory.CreateDirectory(ProfileDirectory);
            }
            readProfilesFromDirectory();
        }

        public static void addProfile(ProfileModel profile)
        {
            lock (ProfileListLock)
            {
                String filePath = System.IO.Path.Combine(ProfileDirectory, profile.Name + ".json");
                writeProfileToProfileDir(profile);
                ProfileList.Add(profile);
            }
        }

        public static void removeProfile(ProfileModel profile)
        {
            lock (ProfileListLock)
            {
                String filePath = System.IO.Path.Combine(ProfileDirectory, profile.Name + ".json");
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                ProfileList.Remove(profile);
            }
        }

        public static List<ProfileModel> getProfiles()
        {
            lock (ProfileListLock)
            {
                return new List<ProfileModel>(ProfileList);
            }
        }

        public static void readProfilesFromDirectory()
        {
            lock (ProfileListLock)
            {
                if (System.IO.Directory.Exists(ProfileDirectory))
                {
                    string[] files = System.IO.Directory.GetFiles(ProfileDirectory, "*.json");
                    foreach (string file in files)
                    {
                        try
                        {
                            ProfileModel profile = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileModel>(System.IO.File.ReadAllText(file));
                            if (profile != null)
                            {
                                if (ProfileList.Any(p => p.Name == profile.Name))
                                {
                                    Debug.WriteLine("profile already exists");
                                    continue;
                                }
                                Debug.WriteLine("adding profile");
                                ProfileList.Add(profile);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error reading profile from file: " + file + " " + ex.Message);
                        }
                    }
                }
            }
        }

        public static Tuple<bool, string> writeProfileToProfileDir(ProfileModel profile)
        {
            String filePath = System.IO.Path.Combine(ProfileDirectory, profile.Name + ".json");
            Debug.WriteLine("saveing to path: " + filePath);
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(profile);
                System.IO.File.WriteAllText(filePath, json);
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error writing profile to file: " + ex.Message);
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        public static Tuple<string, bool> applyProfile(ProfileModel profile)
        {
            if (profile == null)
            {
                return new Tuple<string, bool>("Profile is null", false);
            }
            if (profile.WheelBaseSettings == null)
            {
                return new Tuple<string, bool>("WheelBaseSettings is null", false);
            }
            bool settingsValid = MozaAPIService.validateSettings(profile.WheelBaseSettings);
            ERRORCODE err = MozaAPIService.sendSettingsToWheelBase(profile.WheelBaseSettings);
            if (err != ERRORCODE.NORMAL)
            {
                return new Tuple<string, bool>(err.ToString(), false);
            }
            else
            {
                CurrentLoadedProfile = profile;
                return new Tuple<string, bool>("Profile applied successfully", true);
            }
        }

        public static ProfileModel getProfile(string processName)
        {
            lock (ProfileListLock)
            {
                foreach (ProfileModel profile in ProfileList)
                {
                    if (profile.Process == processName)
                    {
                        return profile;
                    }
                }
                return null;
            }
        }

        public static ProfileModel getCurrentlyLoadedProfile()
        {
            return CurrentLoadedProfile;
        }

        public static void setCurrentlyLoadedProfile(ProfileModel profileModel)
        {
            CurrentLoadedProfile = profileModel;
        }
    }
}