// <copyright file="Building_KHPIToBuilding_KIPConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_POST_APP.Constants;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP building model from the KhPI buildings.
    /// </summary>
    public class Building_KHPIToBuilding_KIPConverter : ITypeConverter<Building_KHPI, Building>
    {
        /// <summary>
        /// Convert building names.
        /// </summary>
        /// <returns>
        /// Name and short name of the KhPI building.
        /// </returns>
        /// <param name="source">Building KHPI.</param>
        /// <param name = "destination">A g</param>
        /// <param name= "context">A </param>
        public Building Convert(Building_KHPI source, Building destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var shortName = string.Empty;
            foreach (var faculty in KIPBuildingShortNames.buildingShortNames)
            {
                if (faculty.Key == source.title)
                {
                    shortName = faculty.Value;
                }
            }

            var obj = new Building
            {
                BuildingID = source.id,
                BuildingName = source.title,
                BuildingShortName = shortName,
            };

            return obj;
        }
    }
}
