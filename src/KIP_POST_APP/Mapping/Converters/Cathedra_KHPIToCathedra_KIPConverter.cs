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
        /// Convert department names.
        /// </summary>
        /// <returns>
        /// Name of department.
        /// </returns>
        /// <param name="source">Building KHPI.</param>
        /// <param name = "destination">A g</param>
        /// <param name= "context">A </param>
        public Cathedra Convert(Cathedra_KHPI source, Cathedra destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new Cathedra
            {
                CathedraID = source.id,
                CathedraName = source.title,
            };

            return obj;
        }
    }
}
