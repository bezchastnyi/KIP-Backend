// <copyright file="Faculty_KHPIToFaculty_KIPConverter.cs" company="PlaceholderCompany">
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
    /// Building of the KIP faculty model from the KhPI faculty.
    /// </summary>
    public class Faculty_KHPIToFaculty_KIPConverter : ITypeConverter<Faculty_KHPI, Faculty>
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
        public Faculty Convert(Faculty_KHPI source, Faculty destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var shortName = string.Empty;
            foreach (var faculty in KIPFacultiesShortNames.FacultiesShortNames)
            {
                if (faculty.Key == source.Title)
                {
                    shortName = faculty.Value;
                }
            }

            var obj = new Faculty
            {
                FacultyID = source.Id,
                FacultyName = source.Title,
                FacultyShortName = shortName,
            };

            return obj;
        }
    }
}
