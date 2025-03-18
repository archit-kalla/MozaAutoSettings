﻿using System;
using System.Collections.Generic;
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
        public Tuple<int,int> MotorLimitAngle { get; set; }
        public int MotorRoadSensitivity { get; set; }
        public int MotorFfbStrength { get; set; }
        public int MotorLimitWheelSpeed { get; set; }
        public int MotorSpringStrength { get; set; }
        public int MotorNaturalDamper { get; set; }
        public int MotorNaturalFriction { get; set; }
        public int MotorSpeedDamping { get; set; }
        public int MotorPeakTorque { get; set; }
        public int MotorNaturalInertiaRatio { get; set; }
        public int MotorNaturalInertia { get; set; }
        public int MotorSpeedDampingStartPoint { get; set; }
        public int MotorHandsOffProtection { get; set; }
        public int MotorFfbReverse { get; set; }
        public int EqualizerAmp7_5 { get; set; }
        public int EqualizerAmp13 { get; set; }
        public int EqualizerAmp22_5 { get; set; }
        public int EqualizerAmp39 { get; set; }
        public int EqualizerAmp55 { get; set; }
        public int EqualizerAmp100 { get; set; }
        public Dictionary<string, int> MotorEqualizerAmp { get; set; }

        public WheelBaseSettingsModel()
        {
            MotorLimitAngle = new Tuple<int, int>(0, 0);
            MotorEqualizerAmp = new Dictionary<string, int>();
        }

    }
}
