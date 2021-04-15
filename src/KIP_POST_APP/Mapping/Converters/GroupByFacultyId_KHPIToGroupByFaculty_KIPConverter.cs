// <copyright file="GroupByFacultyId_KHPIToGroupByFaculty_KIPConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP group model from the KhPI groups`.
    /// </summary>
    public class GroupByFacultyId_KHPIToGroupByFaculty_KIPConverter : ITypeConverter<Group_KHPI, Group>
    {
        /// <summary>
        /// Convert model of group from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of group of model group KIP.
        /// </returns>
        /// <param name="source">Model of group KHPI.</param>
        /// <param name = "destination">Model of group KIP.</param>
        /// <param name= "context">The context. </param>
        public Group Convert(Group_KHPI source, Group destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new Group
            {
                GroupID = source.Id,
                GroupName = source.Title,
                Course = source.Course,
            };

            return obj;
        }
    }
}
