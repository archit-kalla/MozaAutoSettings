using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaAutoSettings.Models
{

    public class WheelBaseSettingsModel
    {
        //right now only synchronus support
        public int MotorLimitAngle1
        {
            get => MotorLimitAngle.Item1;
            set => MotorLimitAngle = new Tuple<int, int>(value, value);
        }

        //public int MotorLimitAngle2
        //{
        //    get => MotorLimitAngle.Item2;
        //    set => MotorLimitAngle = new Tuple<int, int>(MotorLimitAngle.Item1, value);
        //}
        [Required]
        public Tuple<int,int> MotorLimitAngle { get; set; }
        [Required]
        public int MotorRoadSensitivity { get; set; }
        [Required]
        public int MotorFfbStrength { get; set; }
        [Required]
        public int MotorLimitWheelSpeed { get; set; }
        [Required]
        public int MotorSpringStrength { get; set; }
        [Required]
        public int MotorNaturalDamper { get; set; }
        [Required]
        public int MotorNaturalFriction { get; set; }
        [Required]
        public int MotorSpeedDamping { get; set; }
        [Required]
        public int MotorPeakTorque { get; set; }
        [Required]
        public int MotorNaturalInertiaRatio { get; set; }
        [Required]
        public int MotorNaturalInertia { get; set; }
        [Required]
        public int MotorSpeedDampingStartPoint { get; set; }
        [Required]
        public int MotorHandsOffProtection { get; set; }
        [Required]
        public int MotorFfbReverse { get; set; }
        [Required]
        public int EqualizerAmp7_5 { get; set; }
        [Required]
        public int EqualizerAmp13 { get; set; }
        [Required]
        public int EqualizerAmp22_5 { get; set; }
        [Required]
        public int EqualizerAmp39 { get; set; }
        [Required]
        public int EqualizerAmp55 { get; set; }
        [Required]
        public int EqualizerAmp100 { get; set; }
        [Required]
        public Dictionary<string, int> MotorEqualizerAmp { get; set; }

        public WheelBaseSettingsModel()
        {
            MotorLimitAngle = new Tuple<int, int>(0, 0);
            MotorEqualizerAmp = new Dictionary<string, int>();
        }

    }
}
