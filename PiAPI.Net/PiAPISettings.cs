using Newtonsoft.Json.Linq;
using System;

namespace PiAPI
{
    /// <summary>
    /// Creates an instance of a setting
    /// </summary>
    public static class PiAPISettings
    {      
        #region Properties

        /// <summary>
        /// The port that PiAPI runs on
        /// </summary>
        /// <param name="RPi">An instance of a Pi object</param>
        public static long GetAPIPort(this Pi RPi) {
            return long.Parse(RPi.GetAPISetting("port"));
        }

        /// <summary>
        /// The port that PiAPI runs on
        /// </summary>
        /// <param name="RPi">An instance of a Pi object</param>
        /// <param name="Port">The new port value</param>
        public static void SetAPIPort(this Pi RPi, long Port) {
            RPi.SetAPISetting("port", Port);
        }

        #endregion Properties

        #region Functions

        /// <summary>
        /// Returns a specefied setting value from PiAPI
        /// </summary>
        /// <param name="RPi">An instance of a Pi object</param>
        /// <param name="SettingName">The name of the setting</param>
        public static string GetAPISetting(this Pi RPi, string SettingName)
        {
            if (RPi.GetIpAddress() != string.Empty || RPi.GetPort() != -1)
            {
                string Url = RPi.RawUrl + "/GetSetting";

                return Utilities.Post(Url, SettingName).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Sets a specefied PiAPI setting
        /// </summary>
        /// <param name="RPi">An instance of a Pi object</param>
        /// <param name="SettingName">The name of the setting</param>
        /// <param name="SettingValue">The value of the setting</param>
        public static void SetAPISetting(this Pi RPi, string SettingName, object SettingValue)
        {
            if (RPi.GetIpAddress() != string.Empty || RPi.GetPort() != -1)
            {
                string Url = RPi.RawUrl + "/SetSetting";

                JObject Setting = JObject.FromObject(new
                {
                    setting = SettingName,
                    val = SettingValue.ToJSON()
                });

                _ = Utilities.Post(Url, Setting.ToString());
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        #endregion Functions

    }
}
