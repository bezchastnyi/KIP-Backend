// <copyright file="Cathedra_KHPIToCathedra_KIPConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP department model from the KhPI departments.
    /// </summary>
    public class Cathedra_KHPIToCathedra_KIPConverter : ITypeConverter<Cathedra_KHPI, Cathedra>
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
        public Cathedra Convert(Cathedra_KHPI source, Cathedra destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new Cathedra
            {
                CathedraID = source.Id,
                CathedraName = source.Title,
            };

            return obj;
        }
    }
}
