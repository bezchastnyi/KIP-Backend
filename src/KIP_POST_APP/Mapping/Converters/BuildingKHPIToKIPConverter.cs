﻿// <copyright file="BuildingKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
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

            var shortName = string.Empty;
            foreach (var faculty in KIPBuildingShortNames.BuildingShortNames)
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
