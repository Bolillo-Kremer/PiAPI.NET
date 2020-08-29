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
        public static long Port
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
        public static string[] Keys
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

        private static string GetSetting(string SettingName)
        {
            if (Pi.IpAddress != string.Empty || Pi.UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/GetSetting";

                return Utilities.Post(Url, SettingName).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        private static void SetSetting(string SettingName, object SettingValue)
        {
            if (Pi.IpAddress != string.Empty || Pi.UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/SetSetting";

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
