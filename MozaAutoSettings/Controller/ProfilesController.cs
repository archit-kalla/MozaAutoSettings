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
        //public List<String> processes { get; set; }
        private static List<ProfileModel> ProfileList { get; set; } = new List<ProfileModel>();

        public ProfilesController() 
        {
            readProfilesFromDirectory();
        }

        public void addProfile(ProfileModel profile)
        {
            //add profile file to directory
            String filePath = System.IO.Path.Combine("C:\\Users\\Archit\\testsettings", profile.Name + ".json");

            writeProfileToProfileDir(profile);
        }

        public void removeProfile(ProfileModel profile) 
        {
            //remove profile file from directory
            String filePath = System.IO.Path.Combine("C:\\Users\\Archit\\testsettings", profile.Name + ".json");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            ProfileList.Remove(profile);
        }

        public List<ProfileModel> getProfiles()
        {
            return ProfileList;
        }

        //read json files from a directory and add content to profile list
        public void readProfilesFromDirectory()
        {
            String directory = "C:\\Users\\Archit\\testsettings";
            if (System.IO.Directory.Exists(directory))
            {
                string[] files = System.IO.Directory.GetFiles(directory, "*.json");
                foreach (string file in files)
                {
                    try
                    {
                        ProfileModel profile = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileModel>(System.IO.File.ReadAllText(file));
                        if (profile != null)
                        {
                            //check if profile already exists in list
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

        public static bool writeProfileToProfileDir(ProfileModel profile)
        {
            String filePath = System.IO.Path.Combine("C:\\Users\\Archit\\testsettings", profile.Name + ".json");
            Debug.WriteLine("saveing to path: " + filePath);
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(profile);
                System.IO.File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing profile to file: " + ex.Message);
                return false;
            }
        }

        public Tuple<string,bool> applyProfile(ProfileModel profile)
        {
            //apply profile settings to the game
            if (profile == null)
            {
                return new Tuple<string, bool>("Profile is null", false);
            }
            if (profile.WheelBaseSettings == null)
            {
                return new Tuple<string, bool>("WheelBaseSettings is null", false);
            }
            bool settingsValid = MozaAPIService.validateSettings(profile.WheelBaseSettings);
            MozaAPIService.sendSettingsToWheelBase(profile.WheelBaseSettings);
            ERRORCODE err = MozaAPIService.getErrStatus();
            if(err != ERRORCODE.NORMAL)
            {
                return new Tuple<string, bool>(err.ToString(), false);
            }
            else
            {
                return new Tuple<string, bool>("Profile applied successfully", true);
            }
        }

        //look through profile list and if process name matches, return true
        public ProfileModel getProfile(string processName)
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
}
