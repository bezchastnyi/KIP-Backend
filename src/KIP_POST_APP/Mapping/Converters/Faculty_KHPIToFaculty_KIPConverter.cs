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
        /// Convert faculty names.
        /// </summary>
        /// <returns>
        /// Name and short name of the KhPI faculty.
        /// </returns>
        /// <param name="source">Building KHPI.</param>
        /// <param name = "destination">A g</param>
        /// <param name= "context">A </param>
        public Faculty Convert(Faculty_KHPI source, Faculty destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var shortName = string.Empty;
            foreach (var faculty in KIPFacultiesShortNames.facultiesShortNames)
            {
                if (faculty.Key == source.title)
                {
                    shortName = faculty.Value;
                }
            }

            var obj = new Faculty
            {
                FacultyID = source.id,
                FacultyName = source.title,
                FacultyShortName = shortName,
            };

            return obj;
        }
    }
}
