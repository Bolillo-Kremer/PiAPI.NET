using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace PiAPI
{
    /// <summary>
    /// Adds easy functionality to help interface with PiAPI
    /// https://github.com/Bolillo-Kremer/PiAPI
    /// https://youtube.com/BolilloKremer
    /// </summary>
    public static class Pi
    {
        /// <summary>
        /// If given a url, will not use <see cref="Pi.IpAddress"/> or <see cref="Pi.Port"/>
        /// </summary>
        public static string UrlOverride { get; set; } = string.Empty;

        /// <summary>
        /// IP Address of your Raspberry Pi
        /// </summary>
        public static string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// The port that PiAPI is running on (Default 5000)
        /// </summary>
        public static string Port { get; set; } = "5000";

        /// <summary>
        /// Initiates a GPIO pin on the Pi
        /// </summary>
        /// <param name="Pin">The pin number to initiate</param>
        /// <param name="Direction">Either "in" or "out"</param>
        /// <param name="Edge">edges should be configured for the pin</param>
        /// <param name="Options">An optional options object</param>
        /// <returns>The default state of the new pin</returns>
        public static string InitPin(int Pin, string Direction, string Edge = null, string Options = null)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/InitPin";

                if (Options != null) Options = JObject.FromObject(Options).ToString();

                JObject PinSettings = JObject.FromObject(new
                {
                    pin = Pin,
                    direction = Direction,
                    edge = Edge,
                    options = Options
                });

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
        public static string UnexportPin(int Pin)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/Unexport";

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
        public static string CleanExit()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/CleanExit";

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
        /// <param name="Pin">The pin on the Pi (Or "*" for all pins)</param>
        /// <param name="State">The state to set the pin (Or "toggle")</param>
        /// <returns>The state of the pin (Or a JSON of all of the pins and their states)</returns>
        public static string SetState(int Pin, object State)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/SetState";


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
        /// <param name="State">The state to set the pins to (Or "toggle")</param>
        /// <returns>JSON of all the pins that succeeded and failed</returns>
        public static string SetAllStates(object State)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/SetState";


                JObject PinSettings = JObject.FromObject(new
                {
                    pin = "*",
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
        /// <param name="Pin">The pin on the Pi (Or "*" for all pins)</param>
        /// <returns>The state of the pin (Or a JSON of all the pin states)</returns>
        public static string GetState(int Pin)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/GetState";

                return Utilities.Post(Url, Pin.ToString()).Result;
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
        public static string GetAllStates()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/GetState";

                return Utilities.Post(Url, "*").Result;                
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }

        /// <summary>
        /// A JSON array of all active pins
        /// </summary>
        public static string ActivePins {
            get
            {
                if (IpAddress != string.Empty || UrlOverride != string.Empty)
                {
                    string Url = Utilities.RawUrl + "/ActivePins";

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
        public static string Command(string Command)
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/Command";

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
        public static void Reboot()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/Command";

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
        public static void Shutdown()
        {
            if (IpAddress != string.Empty || UrlOverride != string.Empty)
            {
                string Url = Utilities.RawUrl + "/Command";

                _ = Utilities.Post(Url, "sudo shutdown -h");
            }
            else
            {
                throw new Exception("API url not provided");
            }
        }


        /// <summary>
        /// Allows you to delay the execution of the next line of the thread while executing the current line
        /// </summary>
        /// <param name="DelayFunction">See <see cref="Action"/> that you want to delay</param>
        /// <param name="Miliseconds">Miliseconds that you want to delay</param>
        public static void Delay(this Action DelayFunction, int Miliseconds)
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();
            DelayFunction();
            while (true)
            {
                if (SW.ElapsedMilliseconds > Miliseconds)
                {
                    SW.Stop();
                    break;
                }
            }
        }
    }
}
