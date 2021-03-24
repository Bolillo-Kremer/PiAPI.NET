using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PiAPI
{
    /// <summary>
    /// Initiates API values for Pin
    /// </summary>
    public class Pin: Pi
    {
        #region Static Values

        /// <summary>
        /// Pin output value
        /// </summary>
        public static string Out { get; } = "out";

        /// <summary>
        /// Pin input value
        /// </summary>
        public static string In { get; } = "in";

        /// <summary>
        /// Key for getting all pins
        /// </summary>
        public static string All { get; } = "*";

        /// <summary>
        /// High state
        /// </summary>
        public static int High { get; } = 1;

        /// <summary>
        /// Low state
        /// </summary>
        public static int Low { get; } = 0;

        /// <summary>
        /// Toggle state
        /// </summary>
        public static int Toggle { get; } = -1;

        #endregion Static Values

        private int PinNum = -1;

        private string PinDirection = null;

        private string PinEdge = null;

        private long PinEdgeTimeout = -1;

        private string Connection = null;


        /// <summary>
        /// PinNumber Value
        /// </summary>
        public int PinNumber 
        { 
            get 
            {
                return this.PinNum;
            }
        }

        /// <summary>
        /// Direction Value
        /// </summary>
        public string Direction
        {
            get 
            {
                return this.PinDirection;
            }
        }

        /// <summary>
        /// Edge Value
        /// </summary>
        public string Edge 
        {
            get
            {
                return this.PinEdge;
            }
        }

        /// <summary>
        /// EdgeTimeout Value
        /// </summary>
        public long EdgeTimeout
        {
            get
            {
                return this.PinEdgeTimeout;
            }
        }

        /// <summary>
        /// State of Pin on the Pi
        /// </summary>
        /// <returns>Response from the API</returns>
        public int State
        {
            get 
            {
                if (UrlIsValid)
                {
                    string Url = RawUrl + "/GetState";

                    return Convert.ToInt16(Utilities.Post(Url, this.PinNumber.ToString()).Result);
                }
                else
                {
                    throw NoURL;
                }
            }
            set
            {
                if (UrlIsValid)
                {
                    string Url = RawUrl + "/SetState";


                    JObject PinSettings = JObject.FromObject(new
                    {
                        pin = this.PinNumber,
                        state = value
                    });

                    _ = Utilities.Post(Url, PinSettings.ToString()).Result;
                }
                else
                {
                    throw NoURL;
                }
            }
        }

        /// <summary>
        /// Initializes instance of Pin object
        /// </summary>
        /// <returns>Response from the API</returns>
        public Pin(string Connection, int PinNumber, string Direction, string Edge = null, long EdgeTimeout = -1) : base(Connection) {
            this.Connection = Connection;
            this.PinNum = PinNumber;
            this.PinDirection = Direction;
            this.PinEdge = Edge;
            this.PinEdgeTimeout = EdgeTimeout;
        }

        /// <summary>
        /// Unexports pin
        /// </summary>
        /// <returns>Response from the API</returns>
        public string Unexport()
        {
            return UnexportPin(this.PinNumber);        
        }

        /// <summary>
        /// Gets Pins Metadata
        /// </summary>
        /// <returns>Response from the API</returns>
        public Dictionary<string, object> GetMetadata() {
            if (UrlIsValid) {
                string Url = RawUrl + "/GetMetadata";

                return Utilities.ToObject<Dictionary<string, object>>(Utilities.Post(Url, this.PinNumber.ToString()));
            }
            else {
                throw NoURL;
            }
        }

        /// <summary>
        /// Adds Metadata to pin
        /// </summary>
        public void AddMetadata(Dictionary<string, object> Meta) {
            if (UrlIsValid) {
                string Url = RawUrl + "/AddMetadata";

                _ = Utilities.Post(Url, Meta.ToJSON());
            }
            else {
                throw NoURL;
            }
        }

        /// <summary>
        /// Adds Metadata to pin
        /// </summary>
        public void AddMetadata(String Data, object Value) {
            Dictionary<string, object> Meta = new Dictionary<string, object>();
            Meta.Add(Data, Value);
            AddMetadata(Meta);
        }
    }
}
