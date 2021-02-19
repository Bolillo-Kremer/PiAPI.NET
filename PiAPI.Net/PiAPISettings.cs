using Newtonsoft.Json.Linq;
using System;

namespace PiAPI
{
    /// <summary>
    /// Creates an instance of a setting
    /// </summary>
    public class PiAPISettings
    {      
        private Pi RPi;

        /// <summary>
        /// Initializes instance of PiAPISettings
        /// </summary>
        /// <param name="RPi">An instance of a Pi object</param>
        PiAPISettings(Pi RPi) {
            this.RPi = RPi;
        }

        #region Properties

        /// <summary>
        /// The port that PiAPI runs on
        /// </summary>
        public long Port
        {
            get
            {
                return long.Parse(GetSetting("port"));
            }
            set
            {
                SetSetting("port", value);
            }
        }

        /// <summary>
        /// If not empty, PiAPI requires a key contained in this array for every request
        /// </summary>
        public string[] Keys
        {
            get
            {
                return GetSetting("keys").ToArray<string>();
            }
            set
            {
                SetSetting("keys", value);
            }
        }

        #endregion Properties

        #region Functions

        private string GetSetting(string SettingName)
        {
            if (RPi.getIpAddress() != string.Empty || RPi.getPort() != -1)
            {
                string Url = RPi.RawUrl + "/GetSetting";

                return Utilities.Post(Url, SettingName).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        private void SetSetting(string SettingName, object SettingValue)
        {
            if (RPi.getIpAddress() != string.Empty || RPi.getPort() != -1)
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
