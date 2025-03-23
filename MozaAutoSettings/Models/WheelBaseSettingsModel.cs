using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaAutoSettings.Models
{

    public class WheelBaseSettingsModel : IComparable<WheelBaseSettingsModel>
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

        public int CompareTo(WheelBaseSettingsModel other)
        {
            if (other == null) return 1;
            if (MotorLimitAngle.Item1 != other.MotorLimitAngle.Item1) return -1;
            if (MotorLimitAngle.Item2 != other.MotorLimitAngle.Item2) return -1;
            if (MotorRoadSensitivity != other.MotorRoadSensitivity) return -1;
            if (MotorFfbStrength != other.MotorFfbStrength) return -1;
            if (MotorLimitWheelSpeed != other.MotorLimitWheelSpeed) return -1;
            if (MotorSpringStrength != other.MotorSpringStrength) return -1;
            if (MotorNaturalDamper != other.MotorNaturalDamper) return -1;
            if (MotorNaturalFriction != other.MotorNaturalFriction) return -1;
            if (MotorSpeedDamping != other.MotorSpeedDamping) return -1;
            if (MotorPeakTorque != other.MotorPeakTorque) return -1;
            if (MotorNaturalInertiaRatio != other.MotorNaturalInertiaRatio) return -1;
            if (MotorNaturalInertia != other.MotorNaturalInertia) return -1;
            if (MotorSpeedDampingStartPoint != other.MotorSpeedDampingStartPoint) return -1;
            if (MotorHandsOffProtection != other.MotorHandsOffProtection) return -1;
            if (MotorFfbReverse != other.MotorFfbReverse) return -1;
            if (EqualizerAmp7_5 != other.EqualizerAmp7_5) return -1;
            if (EqualizerAmp13 != other.EqualizerAmp13) return -1;
            if (EqualizerAmp22_5 != other.EqualizerAmp22_5) return -1;
            if (EqualizerAmp39 != other.EqualizerAmp39) return -1;
            if (EqualizerAmp55 != other.EqualizerAmp55) return -1;
            if (EqualizerAmp100 != other.EqualizerAmp100) return -1;

            return 0;

        }

        public WheelBaseSettingsModel Clone()
        {
            return new WheelBaseSettingsModel
            {
                MotorLimitAngle = new Tuple<int, int>(MotorLimitAngle.Item1, MotorLimitAngle.Item2),
                MotorRoadSensitivity = MotorRoadSensitivity,
                MotorFfbStrength = MotorFfbStrength,
                MotorLimitWheelSpeed = MotorLimitWheelSpeed,
                MotorSpringStrength = MotorSpringStrength,
                MotorNaturalDamper = MotorNaturalDamper,
                MotorNaturalFriction = MotorNaturalFriction,
                MotorSpeedDamping = MotorSpeedDamping,
                MotorPeakTorque = MotorPeakTorque,
                MotorNaturalInertiaRatio = MotorNaturalInertiaRatio,
                MotorNaturalInertia = MotorNaturalInertia,
                MotorSpeedDampingStartPoint = MotorSpeedDampingStartPoint,
                MotorHandsOffProtection = MotorHandsOffProtection,
                MotorFfbReverse = MotorFfbReverse,
                EqualizerAmp7_5 = EqualizerAmp7_5,
                EqualizerAmp13 = EqualizerAmp13,
                EqualizerAmp22_5 = EqualizerAmp22_5,
                EqualizerAmp39 = EqualizerAmp39,
                EqualizerAmp55 = EqualizerAmp55,
                EqualizerAmp100 = EqualizerAmp100,
                MotorEqualizerAmp = new Dictionary<string, int>(MotorEqualizerAmp)
            };
        }

    }

}
