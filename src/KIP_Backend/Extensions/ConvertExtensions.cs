﻿// <copyright file="ConvertExtensions.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Globalization;
using System.Linq;

namespace KIP_Backend.Extensions
{
    /// <summary>
    /// Convert extensions.
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// Convert string to bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>bool value.</returns>
        public static bool StringToBool(string value)
        {
            value = value.ToLower();
            return value == "1" || value == "true";
        }

        /// <summary>
        /// Convert string to int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>int value.</returns>
        public static int StringToInt(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            int result;

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
        /// <param name="value">The value.</param>
        /// <returns>nullable int value.</returns>
        public static int? StringToNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            int? result;

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
        /// <param name="value">The value.</param>
        /// <returns>nullable float value.</returns>
        public static float? StringToNullableFloat(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            float? result;

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

        /// <summary>
        /// FixTitle.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>Fixed title.</returns>
        public static string FixTitle(string title)
        {
            var part = title.Split('[').FirstOrDefault();
            return part ?? title;
        }
    }
}
