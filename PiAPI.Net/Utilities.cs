using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PiAPI
{
    /// <summary>
    /// Utilities for communicating with the API
    /// </summary>
    public static class Utilities
    {
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Gets the raw url to PiAPI
        /// </summary>
        public static string RawUrl
        {
            get
            {
                string PiUrl = string.Empty;
                if (Pi.UrlOverride == string.Empty)
                {
                    PiUrl += $"http://{Pi.IpAddress}";
                    if (Pi.Port != null) PiUrl += $":{Pi.Port}";
                }
                else
                {
                    PiUrl = Pi.UrlOverride;
                }
                return PiUrl;
            }
        }

        /// <summary>
        /// Posts a given string to a url
        /// </summary>
        /// <param name="Url">The url to post to</param>
        /// <param name="Content">The content to post to the url</param>
        /// <returns>The response from the server</returns>
        public static async Task<string> Post(string Url, string Content)
        {
            StringContent EncodedContent = new StringContent(Content);
            HttpResponseMessage Res = await Client.PostAsync(Url, EncodedContent);
            string ResText = await Res.Content.ReadAsStringAsync();
            return ResText;
        }

        /// <summary>
        /// Gets from a given url
        /// </summary>
        /// <param name="Url">The url to get from</param>
        /// <returns>The response from the server</returns>
        public static async Task<string> Get(string Url)
        {
            string responseString = await Client.GetStringAsync(Url);
            return responseString;
        }

        /// <summary>
        /// Converts an object into a JSON string
        /// </summary>
        /// <param name="Object">The object to convert</param>
        /// <returns>JSON formatted string</returns>
        public static string ToJSON(this object Object)
        {
            return JsonConvert.SerializeObject(Object);
        }

        /// <summary>
        /// Converts a JSON formatted string to a <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="JSONString">The new dictionary</param>
        /// <returns>New dictionary</returns>
        public static Dictionary<TKey, TVal> ToDictionary<TKey, TVal>(this string JSONString)
        {
            try
            {
                return JObject.Parse(JSONString).ToObject<Dictionary<TKey, TVal>>();
            }
            catch (Exception e)
            {
                string Message = $"Could not convert \"{JSONString}\" to Dictionary<string, string>\n";
                Message += "String must be in JSON Object format\n";
                Message += e.ToString();
                throw new Exception(Message);
            }
        }

        /// <summary>
        /// Converts a JSON formatted string to a string[]/>
        /// </summary>
        /// <param name="JSONString"></param>
        /// <returns>New array</returns>
        public static T[] ToArray<T>(this string JSONString)
        {
            try
            {
                return JArray.Parse(JSONString).ToObject<T[]>();
            }
            catch (Exception e)
            {
                string Message = $"Could not convert \"{JSONString}\" to string[]\n";
                Message += "String must be in JSON Array format\n";
                Message += e.ToString();
                throw new Exception(Message);
            }
        }


        /// <summary>
        /// Converts a JSON formatted string to an object
        /// </summary>
        /// <param name="Object"></param>
        /// <returns>New JObject</returns>
        public static T ToObject<T>(this object Object)
        {
            string Common = string.Empty;

            try
            {
                if (Object is string)
                {
                    Common = Object.ToString();
                }
                else
                {
                    Common = Object.ToJSON();
                }

                return JObject.Parse(Common).ToObject<T>();
            }
            catch (Exception e)
            {
                string Message = $"Could not convert \"{Common}\" to {typeof(T)}\n";
                Message += "String must be in JSON Object format\n";
                Message += e.ToString();
                throw new Exception(Message);
            }
        }
    }
}
