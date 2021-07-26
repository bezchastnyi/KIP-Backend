// <copyright file="FacultyKHPIToKIPConverter.cs" company="KIP">
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
    /// Building of the KIP faculty model from the KhPI faculty.
    /// </summary>
    public class FacultyKHPIToKIPConverter : ITypeConverter<FacultyKhPI, Faculty>
    {
        /// <summary>
        /// Convert model of faculty from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of faculty of model faculty KIP.
        /// </returns>
        /// <param name="source">Model of faculty KHPI.</param>
        /// <param name = "destination">Model of faculty KIP.</param>
        /// <param name= "context">The context. </param>
        public Faculty Convert(FacultyKhPI source, Faculty destination, ResolutionContext context)
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
            foreach (var faculty in KIPFacultiesShortNames.FacultiesShortNames)
            {
                if (faculty.Key == source.title)
                {
                    shortName = faculty.Value;
                }
            }

            return new Faculty
            {
                FacultyID = source.id,
                FacultyName = source.title,
                FacultyShortName = shortName,
            };
        }
    }
}
