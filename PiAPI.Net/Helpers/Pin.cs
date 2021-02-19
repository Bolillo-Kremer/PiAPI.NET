namespace PiAPI
{
    /// <summary>
    /// Initiates API values for Pin
    /// </summary>
    public static class Pin
    {
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
    }
}
