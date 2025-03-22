using MozaAutoSettings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using mozaAPI;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MozaAutoSettings.Services
{
    class MozaAPIService
    {

        private static ERRORCODE err = ERRORCODE.NOINSTALLSDK;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);


        private static readonly string LibsFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libs");

        static List<string> callOrder = new List<string>()
        {
            "setMotorLimitAngle",
            "setMotorRoadSensitivity",
            "setMotorFfbStrength",
            "setMotorLimitWheelSpeed",
            "setMotorSpringStrength",
            "setMotorNaturalDamper",
            "setMotorNaturalFriction",
            "setMotorSpeedDamping",
            "setMotorPeakTorque",
            "setMotorNaturalInertiaRatio",
            "setMotorNaturalInertia",
            "setMotorSpeedDampingStartPoint",
            "setMotorHandsOffProtection",
            "setMotorFfbReverse",
            "setMotorEqualizerAmp"
        };
        private static void LoadUnmanagedDll()
        {
            ////check 
            //SetDllDirectory(@"C:\Users\Archit\source\repos\MozaAutoSettings\MozaAutoSettings\lib");
            // if in debug mode, set dll directory to libs folder
            string folderPath;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                folderPath = @"C:\Users\Archit\source\repos\MozaAutoSettings\MozaAutoSettings\lib";
            }
            else
            {
                folderPath = LibsFolder;
            }
            //// Register all DLLs in the libs folder
            //string[] files = System.IO.Directory.GetFiles(folderPath, "*.dll");
            //foreach (string file in files)
            //{
            //    RegisterDll(file);
            //}

            SetDllDirectory(folderPath);

        }

        private static void RegisterDll(string dllPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\RegAsm.exe";
            process.StartInfo.Arguments = $"/codebase \"{dllPath}\""; // /codebase for registering the assembly with its location
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            //check if the dll was registered successfully
            if (process.ExitCode != 0)
            {
                Debug.WriteLine("Error registering dll: " + dllPath);
            }
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
        public static ERRORCODE sendSettingsToWheelBase(WheelBaseSettingsModel wheelBaseSettings)
        {
            List<ERRORCODE> errList = new List<ERRORCODE>();

            if (err == ERRORCODE.NOINSTALLSDK)
            {
                Initialize();
            }
            errList.Add(mozaAPI.mozaAPI.setMotorLimitAngle(wheelBaseSettings.MotorLimitAngle.Item1, wheelBaseSettings.MotorLimitAngle.Item2));

            errList.Add(ERRORCODE.NORMAL); //ignoring below to maintain order
            //err = mozaAPI.mozaAPI.setMotorRoadSensitivity(wheelBaseSettings.MotorRoadSensitivity);
            errList.Add(mozaAPI.mozaAPI.setMotorFfbStrength(wheelBaseSettings.MotorFfbStrength));
            errList.Add(mozaAPI.mozaAPI.setMotorLimitWheelSpeed(wheelBaseSettings.MotorLimitWheelSpeed));
            errList.Add(mozaAPI.mozaAPI.setMotorSpringStrength(wheelBaseSettings.MotorSpringStrength));
            errList.Add(mozaAPI.mozaAPI.setMotorNaturalDamper(wheelBaseSettings.MotorNaturalDamper));
            errList.Add(mozaAPI.mozaAPI.setMotorNaturalFriction(wheelBaseSettings.MotorNaturalFriction));
            errList.Add(mozaAPI.mozaAPI.setMotorSpeedDamping(wheelBaseSettings.MotorSpeedDamping));
            errList.Add(mozaAPI.mozaAPI.setMotorPeakTorque(wheelBaseSettings.MotorPeakTorque));
            errList.Add(ERRORCODE.NORMAL); //ignoring below to maintain order
            //err = mozaAPI.mozaAPI.setMotorNaturalInertiaRatio(wheelBaseSettings.MotorNaturalInertiaRatio);
            errList.Add(mozaAPI.mozaAPI.setMotorNaturalInertia(wheelBaseSettings.MotorNaturalInertia));
            errList.Add(mozaAPI.mozaAPI.setMotorSpeedDampingStartPoint(wheelBaseSettings.MotorSpeedDampingStartPoint));
            errList.Add(mozaAPI.mozaAPI.setMotorHandsOffProtection(wheelBaseSettings.MotorHandsOffProtection));
            errList.Add(mozaAPI.mozaAPI.setMotorFfbReverse(wheelBaseSettings.MotorFfbReverse));

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
            errList.Add(mozaAPI.mozaAPI.setMotorEqualizerAmp(wheelBaseSettings.MotorEqualizerAmp));

            ERRORCODE retErr = ERRORCODE.NORMAL;
            //find which call errors
            for (int i = 0; i < errList.Count; i++)
            {
                ERRORCODE error = errList[i];
                if (error != ERRORCODE.NORMAL)
                {
                    Debug.WriteLine("Error: " + error +" "+ callOrder[i]);
                    retErr = error;
                }
            }
            return retErr;
        }

        public static WheelBaseSettingsModel getSettingsFromWheelBase()
        {
            if (err == ERRORCODE.NOINSTALLSDK)
            {
                Initialize();
            }

            //create a list of 15 ERRORCODEs
            ERRORCODE[] errArray = new ERRORCODE[15];
            for (int i = 0; i < errArray.Length; i++)
            {
                errArray[i] = ERRORCODE.NORMAL;
            }



            var wheelBaseSettings = new WheelBaseSettingsModel
            {
                MotorLimitAngle = mozaAPI.mozaAPI.getMotorLimitAngle(ref errArray[0]),
                MotorRoadSensitivity = mozaAPI.mozaAPI.getMotorRoadSensitivity(ref errArray[1]),
                MotorFfbStrength = mozaAPI.mozaAPI.getMotorFfbStrength(ref errArray[2]),
                MotorLimitWheelSpeed = mozaAPI.mozaAPI.getMotorLimitWheelSpeed(ref errArray[3]),
                MotorSpringStrength = mozaAPI.mozaAPI.getMotorSpringStrength(ref errArray[4]),
                MotorNaturalDamper = mozaAPI.mozaAPI.getMotorNaturalDamper(ref errArray[5]),
                MotorNaturalFriction = mozaAPI.mozaAPI.getMotorNaturalFriction(ref errArray[6]),
                MotorSpeedDamping = mozaAPI.mozaAPI.getMotorSpeedDamping(ref errArray[7]),
                MotorPeakTorque = mozaAPI.mozaAPI.getMotorPeakTorque(ref errArray[8]),
                MotorNaturalInertiaRatio = mozaAPI.mozaAPI.getMotorNaturalInertiaRatio(ref errArray[9]),
                MotorNaturalInertia = mozaAPI.mozaAPI.getMotorNaturalInertia(ref errArray[10]),
                MotorSpeedDampingStartPoint = mozaAPI.mozaAPI.getMotorSpeedDampingStartPoint(ref errArray[11]),
                MotorHandsOffProtection = mozaAPI.mozaAPI.getMotorHandsOffProtection(ref errArray[12]),
                MotorFfbReverse = mozaAPI.mozaAPI.getMotorFfbReverse(ref errArray[13]),
                MotorEqualizerAmp = mozaAPI.mozaAPI.getMotorEqualizerAmp(ref errArray[14])
            };

            //check for errors
            for (int i = 0; i < errArray.Length; i++)
            {
                ERRORCODE error = errArray[i];
                if (error != ERRORCODE.NORMAL)
                {
                    Debug.WriteLine("Error: " + error + " " + callOrder[i]);
                }
            }

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
                Debug.WriteLine("Motor Limit Angle is invalid");
                return false;
            }
            if (settings.MotorLimitAngle.Item1 > 2000 || settings.MotorLimitAngle.Item2 > 2000)
            {
                Debug.WriteLine("Motor Limit Angle is invalid");
                return false;
            }
            if (settings.MotorRoadSensitivity < 0 || settings.MotorRoadSensitivity > 100)
            {
                Debug.WriteLine("Motor Road Sensitivity is invalid");
                return false;
            }
            if (settings.MotorFfbStrength < 0 || settings.MotorFfbStrength > 100)
            {
                Debug.WriteLine("Motor FFB Strength is invalid");
                return false;
            }
            if (settings.MotorLimitWheelSpeed < 10 || settings.MotorLimitWheelSpeed > 200)
            {
                Debug.WriteLine("Motor Limit Wheel Speed is invalid");
                return false;
            }
            if (settings.MotorSpringStrength < 0 || settings.MotorSpringStrength > 100)
            {
                Debug.WriteLine("Motor Spring Strength is invalid");
                return false;
            }
            if (settings.MotorNaturalDamper < 0 || settings.MotorNaturalDamper > 100)
            {
                Debug.WriteLine("Motor Natural Damper is invalid");
                return false;
            }
            if (settings.MotorNaturalFriction < 0 || settings.MotorNaturalFriction > 100)
            {
                Debug.WriteLine("Motor Natural Friction is invalid");
                return false;
            }
            if (settings.MotorSpeedDamping < 0 || settings.MotorSpeedDamping > 100)
            {
                Debug.WriteLine("Motor Speed Damping is invalid");
                return false;
            }
            if (settings.MotorPeakTorque < 50 || settings.MotorPeakTorque > 100)
            {
                Debug.WriteLine("Motor Peak Torque is invalid");
                return false;
            }
            //if (settings.MotorNaturalInertiaRatio < 100 || settings.MotorNaturalInertiaRatio > 4000)
            //{
            //    Debug.WriteLine("Motor Natural Inertia Ratio is invalid");
            //    return false;
            //}
            if (settings.MotorNaturalInertia < 100 || settings.MotorNaturalInertia > 500)
            {
                Debug.WriteLine("Motor Natural Inertia is invalid");
                return false;
            }
            if (settings.MotorSpeedDampingStartPoint < 0 || settings.MotorSpeedDampingStartPoint > 400)
            {
                Debug.WriteLine("Motor Speed Damping Start Point is invalid");
                return false;
            }

            if (settings.EqualizerAmp7_5 < 0 || settings.EqualizerAmp7_5 > 500)
            {
                Debug.WriteLine("Equalizer Amp 7.5 is invalid");
                return false;
            }
            if (settings.EqualizerAmp13 < 0 || settings.EqualizerAmp13 > 500)
            {
                Debug.WriteLine("Equalizer Amp 13 is invalid");
                return false;
            }
            if (settings.EqualizerAmp22_5 < 0 || settings.EqualizerAmp22_5 > 500)
            {
                Debug.WriteLine("Equalizer Amp 22.5 is invalid");
                return false;
            }
            if (settings.EqualizerAmp39 < 0 || settings.EqualizerAmp39 > 500)
            {
                Debug.WriteLine("Equalizer Amp 39 is invalid");
                return false;
            }
            if (settings.EqualizerAmp55 < 0 || settings.EqualizerAmp55 > 500)
            {
                Debug.WriteLine("Equalizer Amp 55 is invalid");
                return false;
            }
            if (settings.EqualizerAmp100 < 0 || settings.EqualizerAmp100 > 100)
            {
                Debug.WriteLine("Equalizer Amp 100 is invalid");
                return false;
            }
            if (settings.MotorEqualizerAmp == null || settings.MotorEqualizerAmp.Count != 6)
            {
                Debug.WriteLine("Motor Equalizer Amp is invalid");
                return false;
            }

            return true;
        }


    }
}
