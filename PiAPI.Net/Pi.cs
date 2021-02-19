using Newtonsoft.Json.Linq;
using System;

namespace PiAPI
{
    /// <summary>
    /// Adds easy functionality to help interface with PiAPI
    /// https://github.com/Bolillo-Kremer/PiAPI
    /// https://youtube.com/BolilloKremer
    /// </summary>
    public class Pi
    {
        /// <summary>
        /// If given a url, will not use <see cref="Pi.IpAddress"/> or <see cref="Pi.SetPort"/>
        /// </summary>
        protected string UrlOverride;

        /// <summary>
        /// IP Address of your Raspberry Pi
        /// </summary>
        private string IpAddress;

        /// <summary>
        /// The port that PiAPI is running on on the raspberry pi
        /// </summary>
        private long SetPort;

        /// <summary>
        /// PiAPI's default port
        /// </summary>
        public static long DefualtPort { get; } = 5000;

        /// <summary>
        /// Gets the raw url to PiAPI
        /// </summary>
        public string RawUrl
        {
            get
            {
                string PiUrl = string.Empty;
                if (UrlOverride == string.Empty)
                {
                    PiUrl += $"http://{IpAddress}";
                    if (SetPort != -1) PiUrl += $":{SetPort}";
                }
                else
                {
                    PiUrl = UrlOverride;
                }
                return PiUrl;
            }
        }

        /// <summary>
        /// Initiates new Pi object
        /// </summary>
        ///<param name="IpAddress">The IpAddress of the raspberry pi</param>
        ///<param name="Port">The port that PiAPI is running on on the raspberry pi</param>
        public Pi(string IpAddress, long Port) {
            this.IpAddress = IpAddress;
            this.SetPort = Port;
            this.UrlOverride = "";
        }

        /// <summary>
        /// Initiates new Pi object
        /// </summary>
        ///<param name="UrlOverride">The full url that PiAPI is running on on the raspberry pi</param>
        public Pi(string UrlOverride) {
            this.IpAddress = null;
            this.SetPort = -1;
            this.UrlOverride = UrlOverride;
        }

        /// <summary>
        /// Initiates a GPIO pin on the Pi
        /// </summary>
        /// <param name="Pin">The pin number to initiate</param>
        /// <param name="Direction">Either "in" or "out"</param>
        /// <param name="Edge">edges should be configured for the pin</param>
        /// <param name="EdgeTimeout">An optional options object</param>
        /// <returns>The default state of the new pin</returns>
        public string InitPin(int Pin, string Direction, string Edge = null, int EdgeTimeout = -1)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/InitPin";


                JObject PinSettings = new JObject();

                PinSettings["pin"] = Pin;
                PinSettings["direction"] = Direction;

                if (Edge != null)
                {
                    PinSettings["edge"] = Edge;
                }

                if (EdgeTimeout != -1)
                {
                    PinSettings["edgeTimeout"] = EdgeTimeout;
                }

                return Utilities.Post(Url, PinSettings.ToString()).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Unexports a pin from the Pi
        /// </summary>
        /// <param name="Pin">The pin to be unexported</param>
        /// <returns>Response from the API</returns>
        public string UnexportPin(int Pin)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/Unexport";

                return Utilities.Post(Url, Pin.ToString()).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
            
        }

        /// <summary>
        /// Unexports all pins on the Pi
        /// </summary>
        /// <returns>Response from the API</returns>
        public string CleanExit()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/CleanExit";

                return Utilities.Get(Url).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Sets the state of a given pin on the Pi
        /// </summary>
        /// <param name="Pin">The pin on the Pi</param>
        /// <param name="State">The state to set the pin (-1 is toggle)</param>
        /// <returns>The state of the pin (Or a JSON of all of the pins and their states)</returns>
        public string SetState(int Pin, int State)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/SetState";


                JObject PinSettings = JObject.FromObject(new
                {
                    pin = Pin,
                    state = State
                });

                return Utilities.Post(Url, PinSettings.ToString()).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Sets all pins on the Pi to a given state
        /// </summary>
        /// <param name="State">The state to set the pins to (-1 to toggle)</param>
        /// <returns>JSON of all the pins that succeeded and failed</returns>
        public string SetAllStates(int State)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/SetState";


                JObject PinSettings = JObject.FromObject(new
                {
                    pin = Pin.All,
                    state = State
                });

                return Utilities.Post(Url, PinSettings.ToString()).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Gets the state of a pin
        /// </summary>
        /// <param name="Pin">The pin on the Pi</param>
        /// <returns>The state of the pin (Or a JSON of all the pin states)</returns>
        public int GetState(int Pin)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/GetState";

                return Convert.ToInt16(Utilities.Post(Url, Pin.ToString()).Result);
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Gets all current pin states from the PI
        /// </summary>
        /// <returns>JSON string of all the active pins and their states</returns>
        public string GetAllStates()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/GetState";

                return Utilities.Post(Url, Pin.All).Result;                
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// A JSON array of all active pins
        /// </summary>
        public string ActivePins {
            get
            {
                if (IpAddress != string.Empty || UrlOverride != string.Empty)
                {
                    string Url = RawUrl + "/ActivePins";

                    return Utilities.Get(Url).Result;
                }
                else
                {
                    throw new Exception("API url not provided");
                }
            }
        }

        /// <summary>
        /// Executes a Pi terminal command
        /// </summary>
        public string Command(string Command)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/Command";

                return Utilities.Post(Url, Command).Result;
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Reboots the Pi
        /// </summary>
        public void Reboot()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/Command";

                _ = Utilities.Post(Url, "sudo reboot");
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Shutsdown the Pi
        /// </summary>
        public void Shutdown()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = RawUrl + "/Command";

                _ = Utilities.Post(Url, "sudo shutdown -h");
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// Returns the IPAddress of the Pi
        /// </summary>
        public string getIpAddress() {
            return IpAddress;
        }

        /// <summary>
        /// Returns the Port that was set during initialization
        /// </summary>
        public long getPort() {
            return SetPort;
        }
    }
}
