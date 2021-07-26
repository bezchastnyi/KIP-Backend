// <copyright file="BuildingKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Models.KIP;
using KIP_server_NoAuth.Constants;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP building model from the KhPI buildings.
    /// </summary>
    public class BuildingKHPIToKIPConverter : ITypeConverter<BuildingKHPI, Building>
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
        public Building Convert(BuildingKHPI source, Building destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            var shortName = string.Empty;
            foreach (var building in KIPBuildingShortNames.BuildingShortNames)
            {
                if (source.title.Contains(building.Key))
                {
                    shortName = building.Value;
                    break;
                }
            }

            return new Building
            {
                BuildingID = source.id,
                BuildingName = source.title,
                BuildingShortName = shortName,
            };
        }
    }
}
