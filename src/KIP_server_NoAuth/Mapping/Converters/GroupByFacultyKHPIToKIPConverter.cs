// <copyright file="GroupByFacultyKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Models.KIP;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP group model from the KhPI groups`.
    /// </summary>
    public class GroupByFacultyKHPIToKIPConverter : ITypeConverter<GroupKHPI, Group>
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
        public Group Convert(GroupKHPI source, Group destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            return new Group
            {
                GroupID = source.id,
                GroupName = source.title,
                Course = source.course,
            };
        }
    }
}
