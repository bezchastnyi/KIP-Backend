// <copyright file="DebtListConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_server_AUTH.Models.KHPI;
using KIP_server_AUTH.Models.KIP;

namespace KIP_server_AUTH.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI DebtList model to the KIP model.
    /// </summary>
    public class DebtListConverter : ITypeConverter<DebtListKHPI, DebtList>
    {
        /// <summary>
        /// Convert model of DebtList from KHPI to KIP.
        /// </summary>
        /// <param name="source">The model of KHPI DebtList.</param>
        /// <param name = "destination">The model of KIP DebtList.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP DebtList model.</returns>
        public DebtList Convert(DebtListKHPI source, DebtList destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new DebtList()
            {
                SubjectId = ConvertExtensions.StringToInt(source.subj_id),
                Subject = source.subject,
                Prof = source.prepod,
                ShortCathedra = source.kabr,
                Cathedra = source.kafedra,
                Course = ConvertExtensions.StringToInt(source.kurs),
                Semester = ConvertExtensions.StringToInt(source.semestr),
                Credits = ConvertExtensions.StringToNullableFloat(source.credit),
                Control = source.control,
                Date = source.data,
            };
        }
    }
}
