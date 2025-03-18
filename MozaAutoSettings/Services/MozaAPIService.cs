using MozaAutoSettings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mozaAPI;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.VisualBasic.FileIO;

namespace MozaAutoSettings.Services
{
    class MozaAPIService
    {

        private static ERRORCODE err = ERRORCODE.NOINSTALLSDK;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);

        private static void LoadUnmanagedDll()
        {
            SetDllDirectory(@"C:\Users\Archit\source\repos\MozaAutoSettings\MozaAutoSettings\lib"); //TODO change this to dynamic path
        }

        public static void Initialize()
        {
            LoadUnmanagedDll();
            mozaAPI.mozaAPI.installMozaSDK();

            //test the sdk
            while (err != ERRORCODE.NORMAL)
            {
                mozaAPI.mozaAPI.getMotorFfbStrength(ref err);
            }

            return;
        }
        public static void sendSettingsToWheelBase(WheelBaseSettingsModel wheelBaseSettings)
        {
                
            if (err == ERRORCODE.NOINSTALLSDK)
            {
                Initialize();
            }
            err = mozaAPI.mozaAPI.setMotorLimitAngle(wheelBaseSettings.MotorLimitAngle.Item1, wheelBaseSettings.MotorLimitAngle.Item2);
            err = mozaAPI.mozaAPI.setMotorRoadSensitivity(wheelBaseSettings.MotorRoadSensitivity);
            err = mozaAPI.mozaAPI.setMotorFfbStrength(wheelBaseSettings.MotorFfbStrength);
            err = mozaAPI.mozaAPI.setMotorLimitWheelSpeed(wheelBaseSettings.MotorLimitWheelSpeed);
            err = mozaAPI.mozaAPI.setMotorSpringStrength(wheelBaseSettings.MotorSpringStrength);
            err = mozaAPI.mozaAPI.setMotorNaturalDamper(wheelBaseSettings.MotorNaturalDamper);
            err = mozaAPI.mozaAPI.setMotorNaturalFriction(wheelBaseSettings.MotorNaturalFriction);
            err = mozaAPI.mozaAPI.setMotorSpeedDamping(wheelBaseSettings.MotorSpeedDamping);
            err = mozaAPI.mozaAPI.setMotorPeakTorque(wheelBaseSettings.MotorPeakTorque);
            err = mozaAPI.mozaAPI.setMotorNaturalInertiaRatio(wheelBaseSettings.MotorNaturalInertiaRatio);
            err = mozaAPI.mozaAPI.setMotorNaturalInertia(wheelBaseSettings.MotorNaturalInertia);
            err = mozaAPI.mozaAPI.setMotorSpeedDampingStartPoint(wheelBaseSettings.MotorSpeedDampingStartPoint);
            err = mozaAPI.mozaAPI.setMotorHandsOffProtection(wheelBaseSettings.MotorHandsOffProtection);
            err = mozaAPI.mozaAPI.setMotorFfbReverse(wheelBaseSettings.MotorFfbReverse);

            //packs the individual equalizer amps to a dictionary
            wheelBaseSettings.MotorEqualizerAmp = new Dictionary<string, int>
            {
                { "EqualizerAmp7_5", wheelBaseSettings.EqualizerAmp7_5 },
                { "EqualizerAmp13", wheelBaseSettings.EqualizerAmp13 },
                { "EqualizerAmp22_5", wheelBaseSettings.EqualizerAmp22_5 },
                { "EqualizerAmp39", wheelBaseSettings.EqualizerAmp39 },
                { "EqualizerAmp55", wheelBaseSettings.EqualizerAmp55 },
                { "EqualizerAmp100", wheelBaseSettings.EqualizerAmp100 }
            };
            err = mozaAPI.mozaAPI.setMotorEqualizerAmp(wheelBaseSettings.MotorEqualizerAmp);


            if (err != ERRORCODE.NORMAL)
            {
                Debug.WriteLine("Error sending settings to wheelbase: " + err);
            }
            else
            {
                Debug.WriteLine("Settings sent to wheelbase successfully.");
            }


        }

        public static WheelBaseSettingsModel getSettingsFromWheelBase()
        {
            if (err == ERRORCODE.NOINSTALLSDK)
            {
                Initialize();
            }

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


            

            //unpacks the dictionary to the individual equalizer amps
            foreach (var key in wheelBaseSettings.MotorEqualizerAmp.Keys.ToList())
            {
                switch (key)
                {
                    case "EqualizerAmp7_5":
                        wheelBaseSettings.EqualizerAmp7_5 = wheelBaseSettings.MotorEqualizerAmp[key];
                        break;
                    case "EqualizerAmp13":
                        wheelBaseSettings.EqualizerAmp13 = wheelBaseSettings.MotorEqualizerAmp[key];
                        break;
                    case "EqualizerAmp22_5":
                        wheelBaseSettings.EqualizerAmp22_5 = wheelBaseSettings.MotorEqualizerAmp[key];
                        break;
                    case "EqualizerAmp39":
                        wheelBaseSettings.EqualizerAmp39 = wheelBaseSettings.MotorEqualizerAmp[key];
                        break;
                    case "EqualizerAmp55":
                        wheelBaseSettings.EqualizerAmp55 = wheelBaseSettings.MotorEqualizerAmp[key];
                        break;
                    case "EqualizerAmp100":
                        wheelBaseSettings.EqualizerAmp100 = wheelBaseSettings.MotorEqualizerAmp[key];
                        break;
                }
            }
            return wheelBaseSettings;
        }

        public static ERRORCODE getErrStatus()
        {
            return err;
        }
        public static bool validateSettings(WheelBaseSettingsModel settings)
        {
            if (settings == null)
            {
                return false;
            }
            //validate each setting within bounds
            if (settings.MotorLimitAngle.Item1 < 90 || settings.MotorLimitAngle.Item2 < 90)
            {
                return false;
            }
            if (settings.MotorRoadSensitivity < 0 || settings.MotorRoadSensitivity > 100)
            {
                return false;
            }
            if (settings.MotorFfbStrength < 0 || settings.MotorFfbStrength > 100)
            {
                return false;
            }
            if (settings.MotorLimitWheelSpeed < 10 || settings.MotorLimitWheelSpeed > 200)
            {
                return false;
            }
            if (settings.MotorSpringStrength < 0 || settings.MotorSpringStrength > 100)
            {
                return false;
            }
            if (settings.MotorNaturalDamper < 0 || settings.MotorNaturalDamper > 100)
            {
                return false;
            }
            if (settings.MotorNaturalFriction < 0 || settings.MotorNaturalFriction > 100)
            {
                return false;
            }
            if (settings.MotorSpeedDamping < 0 || settings.MotorSpeedDamping > 100)
            {
                return false;
            }
            if (settings.MotorPeakTorque < 50 || settings.MotorPeakTorque > 100)
            {
                return false;
            }
            if (settings.MotorNaturalInertiaRatio < 0 || settings.MotorNaturalInertiaRatio > 100)
            {
                return false;
            }
            if (settings.MotorNaturalInertia < 100 || settings.MotorNaturalInertia > 500)
            {
                return false;
            }
            if (settings.MotorSpeedDampingStartPoint < 0 || settings.MotorSpeedDampingStartPoint > 400)
            {
                return false;
            }

            if (settings.EqualizerAmp7_5 < 0 || settings.EqualizerAmp7_5 > 500)
            {
                return false;
            }
            if (settings.EqualizerAmp13 < 0 || settings.EqualizerAmp13 > 500)
            {
                return false;
            }
            if (settings.EqualizerAmp22_5 < 0 || settings.EqualizerAmp22_5 > 500)
            {
                return false;
            }
            if (settings.EqualizerAmp39 < 0 || settings.EqualizerAmp39 > 500)
            {
                return false;
            }
            if (settings.EqualizerAmp55 < 0 || settings.EqualizerAmp55 > 500)
            {
                return false;
            }
            if (settings.EqualizerAmp100 < 0 || settings.EqualizerAmp100 > 100)
            {
                return false;
            }
            if (settings.MotorEqualizerAmp == null || settings.MotorEqualizerAmp.Count != 6)
            {
                return false;
            }

            return true;
        }


    }
}
