using System;
using System.Collections.Generic;
using System.Text;

namespace PiAPI.Helpers
{
    /// <summary>
    /// Initiates API values for Edge
    /// </summary>
    public static class Edge
    {
        /// <summary>
        /// Rising Edge value
        /// </summary>
        public static string Rising { get; } = "rising";

        /// <summary>
        /// Falling Edge value
        /// </summary>
        public static string Falling { get; } = "falling";

        /// <summary>
        /// Both Edges value
        /// </summary>
        public static string Both { get; } = "both";
    }
}
