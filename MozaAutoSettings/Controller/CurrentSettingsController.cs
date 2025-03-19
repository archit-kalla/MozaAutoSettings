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


        public void sendSettingsToWheelBase(WheelBaseSettingsModel wheelBaseSettings)
        {
            MozaAPIService.sendSettingsToWheelBase(wheelBaseSettings);
        }

    }
}
