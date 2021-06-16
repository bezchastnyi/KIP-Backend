// <copyright file="ConvertExtensions.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Globalization;

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
            if (value == "1" ||
                value == "true")
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
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            var result = 0;

            try
            {
                result = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
                return 0;
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
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            int? result = null;

            try
            {
                result = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
                return null;
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
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            float? result = null;

            try
            {
                result = float.Parse(value, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ": " + e.StackTrace);
                return null;
            }

            return result;
        }
    }
}
