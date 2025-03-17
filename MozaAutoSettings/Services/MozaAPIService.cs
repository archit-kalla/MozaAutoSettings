using MozaAutoSettings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mozaAPI;

namespace MozaAutoSettings.Services
{
    class MozaAPIService
    {
        public void sendSettingsToWheelBase(WheelBaseSettingsModel wheelBaseSettings)
        {
            mozaAPI.mozaAPI.setMotorLimitAngle(wheelBaseSettings.MotorLimitAngle.Item1, wheelBaseSettings.MotorLimitAngle.Item2); // Assuming 0 as a placeholder for gameMaximumAngle
            mozaAPI.mozaAPI.setMotorRoadSensitivity(wheelBaseSettings.MotorRoadSensitivity);
            mozaAPI.mozaAPI.setMotorFfbStrength(wheelBaseSettings.MotorFfbStrength);
            mozaAPI.mozaAPI.setMotorLimitWheelSpeed(wheelBaseSettings.MotorLimitWheelSpeed);
            mozaAPI.mozaAPI.setMotorSpringStrength(wheelBaseSettings.MotorSpringStrength);
            mozaAPI.mozaAPI.setMotorNaturalDamper(wheelBaseSettings.MotorNaturalDamper);
            mozaAPI.mozaAPI.setMotorNaturalFriction(wheelBaseSettings.MotorNaturalFriction);
            mozaAPI.mozaAPI.setMotorSpeedDamping(wheelBaseSettings.MotorSpeedDamping);
            mozaAPI.mozaAPI.setMotorPeakTorque(wheelBaseSettings.MotorPeakTorque);
            mozaAPI.mozaAPI.setMotorNaturalInertiaRatio(wheelBaseSettings.MotorNaturalInertiaRatio);
            mozaAPI.mozaAPI.setMotorNaturalInertia(wheelBaseSettings.MotorNaturalInertia);
            mozaAPI.mozaAPI.setMotorSpeedDampingStartPoint(wheelBaseSettings.MotorSpeedDampingStartPoint);
            mozaAPI.mozaAPI.setMotorHandsOffProtection(wheelBaseSettings.MotorHandsOffProtection);
            mozaAPI.mozaAPI.setMotorFfbReverse(wheelBaseSettings.MotorFfbReverse);
            mozaAPI.mozaAPI.setMotorEqualizerAmp(wheelBaseSettings.MotorEqualizerAmp);

        }

        public WheelBaseSettingsModel getSettingsFromWheelBase()
        {
            ERRORCODE err = ERRORCODE.NORMAL;

            var wheelBaseSettings = new WheelBaseSettingsModel
            {
                MotorLimitAngle = mozaAPI.mozaAPI.getMotorLimitAngle(ref err),
                MotorRoadSensitivity = mozaAPI.mozaAPI.getMotorRoadSensitivity(ref err),
                MotorFfbStrength = mozaAPI.mozaAPI.getMotorFfbStrength(ref err),
                MotorLimitWheelSpeed = mozaAPI.mozaAPI.getMotorLimitWheelSpeed(ref err),
                MotorSpringStrength = mozaAPI.mozaAPI.getMotorSpringStrength(ref err),
                MotorNaturalDamper = mozaAPI.mozaAPI.getMotorNaturalDamper(ref err),
                MotorNaturalFriction = mozaAPI.mozaAPI.getMotorNaturalFriction(ref err),
                MotorSpeedDamping = mozaAPI.mozaAPI.getMotorSpeedDamping(ref err),
                MotorPeakTorque = mozaAPI.mozaAPI.getMotorPeakTorque(ref err),
                MotorNaturalInertiaRatio = mozaAPI.mozaAPI.getMotorNaturalInertiaRatio(ref err),
                MotorNaturalInertia = mozaAPI.mozaAPI.getMotorNaturalInertia(ref err),
                MotorSpeedDampingStartPoint = mozaAPI.mozaAPI.getMotorSpeedDampingStartPoint(ref err),
                MotorHandsOffProtection = mozaAPI.mozaAPI.getMotorHandsOffProtection(ref err),
                MotorFfbReverse = mozaAPI.mozaAPI.getMotorFfbReverse(ref err),
                MotorEqualizerAmp = mozaAPI.mozaAPI.getMotorEqualizerAmp(ref err)
            };

            return wheelBaseSettings;
        }
    }
}
