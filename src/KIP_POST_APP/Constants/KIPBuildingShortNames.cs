// <copyright file="KIPBuildingShortNames.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace KIP_POST_APP.Constants
{
    /// <summary>
    /// KIP short names for building constants.
    /// </summary>
    public class KIPBuildingShortNames
    {
        /// <summary>
        /// Gets short names of building.
        /// </summary>
        public static Dictionary<string, string> BuildingShortNames { get; } = new Dictionary<string, string>
        {
            { "Учбовий корпус 1", "У1" },
            { "Учбовий корпус 2", "У2" },
            { "Учбовий корпус 3", "У3" },
            { "Учбовий корпус 4", "У4" },
            { "Учбовий корпус 5", "У5" },
            { "Головний аудіторний корпус", "ГАК" },
        };
    }
}
