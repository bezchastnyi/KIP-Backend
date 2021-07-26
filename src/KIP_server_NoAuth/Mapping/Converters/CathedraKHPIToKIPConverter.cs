// <copyright file="CathedraKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Models.KIP;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP department model from the KhPI departments.
    /// </summary>
    public class CathedraKHPIToKIPConverter : ITypeConverter<CathedraKHPI, Cathedra>
    {
        /// <summary>
        /// Convert model of department from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of department of model department KIP.
        /// </returns>
        /// <param name="source">Model of department KHPI.</param>
        /// <param name = "destination">Model of department KIP.</param>
        /// <param name= "context">The context. </param>
        public Cathedra Convert(CathedraKHPI source, Cathedra destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            return new Cathedra
            {
                CathedraID = source.id,
                CathedraName = source.title,
            };
        }
    }
}
