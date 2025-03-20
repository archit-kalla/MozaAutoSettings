using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaAutoSettings.Models
{
    public class ProfileModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Process { get; set; }
        [Required]
        public WheelBaseSettingsModel WheelBaseSettings { get; set; }
    }
}
