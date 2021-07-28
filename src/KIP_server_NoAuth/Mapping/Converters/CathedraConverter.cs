// <copyright file="CathedraConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Models.KIP.NoAuth;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// CathedraConverter.
    /// </summary>
    public class CathedraConverter : ITypeConverter<CathedraKhPI, Cathedra>
    {
        /// <summary>
        /// Convert model of department from KhPI to KIP.
        /// </summary>
        /// <returns>Object of department of model department KIP.</returns>
        /// <param name="source">Model of department KhPI.</param>
        /// <param name = "destination">Model of department KIP.</param>
        /// <param name= "context">The context. </param>
        /// <exception cref="ArgumentNullException">source.</exception>
        public Cathedra Convert(CathedraKhPI source, Cathedra destination, ResolutionContext context)
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
                CathedraId = source.id,
                CathedraName = source.title,
            };
        }
    }
}
