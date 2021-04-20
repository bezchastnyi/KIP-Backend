// <copyright file="FacultyKHPIToKIPConverter.cs" company="KIP">
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
    /// Building of the KIP faculty model from the KhPI faculty.
    /// </summary>
    public class FacultyKHPIToKIPConverter : ITypeConverter<FacultyKHPI, Faculty>
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
        public Faculty Convert(FacultyKHPI source, Faculty destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var shortName = string.Empty;
            foreach (var faculty in KIPFacultiesShortNames.FacultiesShortNames)
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
