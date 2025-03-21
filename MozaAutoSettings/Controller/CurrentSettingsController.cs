using mozaAPI;
using MozaAutoSettings.Models;
using MozaAutoSettings.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaAutoSettings.Controller
{
    class CurrentSettingsController
    {
        private WheelBaseSettingsModel currentWheelBaseSettings { get; set; } = new WheelBaseSettingsModel();

        public CurrentSettingsController()
        {
            this.updateCurrentWheelBaseSettingsFromAPI();
        }

        public WheelBaseSettingsModel getCurrentWheelBaseSettings()
        {
            return this.currentWheelBaseSettings;
        }

        public void updateCurrentWheelBaseSettingsFromAPI()
        {
            this.currentWheelBaseSettings = MozaAPIService.getSettingsFromWheelBase();
            if (currentWheelBaseSettings == null)
            {

                this.currentWheelBaseSettings = MozaAPIService.getSettingsFromWheelBase();

            }
        }


        public Tuple<string,bool> sendSettingsToWheelBase(WheelBaseSettingsModel wheelBaseSettings)
        {
            ERRORCODE err = MozaAPIService.sendSettingsToWheelBase(wheelBaseSettings);
            if (err != ERRORCODE.NORMAL)
            {
                return new Tuple<string, bool>("Error sending settings to wheel base: " + err.ToString(), false);
            }
            return new Tuple<string, bool>("Settings sent successfully", true);
        }

    }
}
