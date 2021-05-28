// <copyright file="ConvertExtensions.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_AUTH.Extensions
{
    /// <summary>
    /// Convert extensions.
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// Convert string to bool.
        /// </summary>
        /// <returns>
        /// bool value.
        /// </returns>
        /// <param name="value">Value.</param>
        public static bool StringToBool(string value)
        {
            if (value == "1" || value == "true")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Convert string to int.
        /// </summary>
        /// <returns>
        /// int value.
        /// </returns>
        /// <param name="value">Value.</param>
        public static int StringToInt(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            var result = 0;

            try
            {
                result = System.Convert.ToInt32(value);
            }
            catch
            {
                return result;
            }

            return result;
        }

        /// <summary>
        /// Convert string to nullable int.
        /// </summary>
        /// <returns>
        /// nullable int value.
        /// </returns>
        /// <param name="value">Value.</param>
        public static int? StringToNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            int? result = null;

            try
            {
                result = System.Convert.ToInt32(value);
            }
            catch
            {
                return result;
            }

            return result;
        }

        /// <summary>
        /// Convert string to nullable float.
        /// </summary>
        /// <returns>
        /// nullable float value.
        /// </returns>
        /// <param name="value">Value.</param>
        public static float? StringToNullableFloat(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            float? result = null;

            try
            {
                result = float.Parse(value);
            }
            catch
            {
                return result;
            }

            return result;
        }
    }
}
