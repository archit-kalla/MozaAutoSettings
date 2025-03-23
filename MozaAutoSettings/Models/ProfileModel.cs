using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaAutoSettings.Models
{
    public class ProfileModel : IComparable<ProfileModel>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Process { get; set; }
        [Required]
        public WheelBaseSettingsModel WheelBaseSettings { get; set; }

    public int CompareTo(ProfileModel other)
        {
            if (other == null) return 1;

            int nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;

            int processComparison = string.Compare(Process, other.Process, StringComparison.Ordinal);
            if (processComparison != 0) return processComparison;

            return other.WheelBaseSettings.CompareTo(WheelBaseSettings);
        }

        public ProfileModel Clone()
        {
            return new ProfileModel
            {
                Name = this.Name,
                Process = this.Process,
                WheelBaseSettings = this.WheelBaseSettings.Clone()
            };
        }
    }
}
