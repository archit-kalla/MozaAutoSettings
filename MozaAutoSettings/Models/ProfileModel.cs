using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaAutoSettings.Models
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public string Process { get; set; }
        public string ProcessPath { get; set; }
        public WheelBaseSettingsModel WheelBaseSettings { get; set; }
    }
}
