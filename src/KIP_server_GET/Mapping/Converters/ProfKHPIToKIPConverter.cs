// <copyright file="ProfKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Text.RegularExpressions;
using AutoMapper;
using KIP_server_GET.Models.KHPI;
using KIP_server_GET.Models.KIP;

namespace KIP_server_GET.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP teachers model from the KhPI teachers.
    /// </summary>
    public class ProfKHPIToKIPConverter : ITypeConverter<ProfKHPI, Prof>
    {
        /// <summary>
        /// Convert model of teachers from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of teachers of model teachers KIP.
        /// </returns>
        /// <param name="source">Model of teachers KHPI.</param>
        /// <param name = "destination">Model of teachers KIP.</param>
        /// <param name= "context">The context. </param>
        public Prof Convert(ProfKHPI source, Prof destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            var list = Regex.Split(source.title, @"\s{1,}|_#./!(),-");

            var profSurname = string.Empty;
            var profName = string.Empty;
            var profPatronymic = string.Empty;

            if (list.Length > 2)
            {
                profSurname = list[0];
                profName = list[1];
                profPatronymic = list[2];
            }

            if (profSurname == string.Empty)
            {
                return null;
            }

            return new Prof
            {
                ProfID = source.id,
                ProfSurname = profSurname,
                ProfName = profName,
                ProfPatronymic = profPatronymic,
            };
        }
    }
}
