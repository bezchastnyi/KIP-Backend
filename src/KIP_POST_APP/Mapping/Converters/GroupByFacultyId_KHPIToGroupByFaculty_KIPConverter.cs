// <copyright file="GroupByFacultyId_KHPIToGroupByFaculty_KIPConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Services;

namespace KIP_POST_APP.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP group model from the KhPI groups`.
    /// </summary>
    public class GroupByFacultyId_KHPIToGroupByFaculty_KIPConverter : ITypeConverter<Group_KHPI, Group>
    {
        /// <summary>
        /// Convert group names.
        /// </summary>
        /// <returns>
        /// Group names.
        /// </returns>
        /// <param name="source">r</param>
        /// <param name="destination">t</param>
        /// <param name="context">t</param>
        public Group Convert(Group_KHPI source, Group destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new Group
            {
                GroupID = source.id,
                GroupName = source.title,
                Course = source.course,
            };

            return obj;
        }
    }
}
