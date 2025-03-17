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
        public List<ProfileModel> ProfileList { get; set; } = new List<ProfileModel>();

        public ProfilesController() 
        {
        }

        public void addProfile(ProfileModel profile)
        {
            this.ProfileList.Add(profile);
        }

        
        public void removeProfile(ProfileModel profile) 
        {
            this.ProfileList.Remove(profile);
        }

        //read json files from a directory and add content to profile list
        public void readProfilesFromDirectory(string directory)
        {
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

        //public void refreshProceses()
        //{
        //    this.processes.Clear();
        //    Process[] processes = Process.GetProcesses();
        //    foreach (Process p in processes)
        //    {
        //        if (!String.IsNullOrEmpty(p.MainWindowTitle))
        //        {
        //            this.processes.Add(p.ProcessName);
        //        }
        //    }
        //}

    }
}
