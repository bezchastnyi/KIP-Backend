// <copyright file="DebtListKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_server_AUTH.Extensions;
using KIP_server_AUTH.Models.KHPI;
using KIP_server_AUTH.Models.KIP;

namespace KIP_server_AUTH.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP DebtList model from the KhPI DebtList.
    /// </summary>
    public class DebtListKHPIToKIPConverter : ITypeConverter<DebtListKHPI, DebtList>
    {
        /// <summary>
        /// Convert model of DebtList from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of DebtList of KIP model.
        /// </returns>
        /// <param name="source">Model of DebtList KHPI.</param>
        /// <param name = "destination">Model of DebtList KIP.</param>
        /// <param name= "context">The context. </param>
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
