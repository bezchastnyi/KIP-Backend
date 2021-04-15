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
        /// Convert model of building from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of building of model building KIP.
        /// </returns>
        /// <param name="source">Model of buildind KHPI.</param>
        /// <param name = "destination">Model of building KIP.</param>
        /// <param name= "context">The context. </param>
        public Building Convert(Building_KHPI source, Building destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var shortName = string.Empty;
            foreach (var faculty in KIPBuildingShortNames.BuildingShortNames)
            {
                if (faculty.Key == source.Title)
                {
                    shortName = faculty.Value;
                }
            }

            var obj = new Building
            {
                BuildingID = source.Id,
                BuildingName = source.Title,
                BuildingShortName = shortName,
            };

            return obj;
        }
    }
}
