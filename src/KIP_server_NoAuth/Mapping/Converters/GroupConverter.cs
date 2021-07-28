// <copyright file="GroupConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Models.KIP.NoAuth;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// GroupConverter.
    /// </summary>
    public class GroupConverter : ITypeConverter<GroupKhPI, Group>
    {
        /// <summary>
        /// Convert model of group from KhPI to KIP.
        /// </summary>
        /// <returns>Object of group of model group KIP.</returns>
        /// <param name="source">Model of group KhPI.</param>
        /// <param name="destination">Model of group KIP.</param>
        /// <param name="context">The context. </param>
        public Group Convert(GroupKhPI source, Group destination, ResolutionContext context)
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
                GroupId = source.id,
                GroupName = source.title,
                Course = source.course,
            };
        }
    }
}
