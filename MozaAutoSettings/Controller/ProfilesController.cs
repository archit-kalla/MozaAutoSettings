using MozaAutoSettings.Models;
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
        private List<ProfileModel> ProfileList { get; set; } = new List<ProfileModel>();

        public ProfilesController() 
        {
            readProfilesFromDirectory();
        }

        public void addProfile(ProfileModel profile)
        {
            ProfileList.Add(profile);
        }

        
        public void removeProfile(ProfileModel profile) 
        {
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
                            Debug.WriteLine("adding profile");
                            this.ProfileList.Add(profile);
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

    }
}
