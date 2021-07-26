// <copyright file="SemesterMarksListConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_server_Auth.Models.KHPI;
using KIP_server_Auth.Models.KIP;

namespace KIP_server_Auth.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI SemesterMarksList model to the KIP model.
    /// </summary>
    public class SemesterMarksListConverter :
        ITypeConverter<SemesterMarksListKHPI, SemesterMarksList>
    {
        /// <summary>
        /// Convert model of SemesterMarksList from KHPI to KIP.
        /// </summary>
        /// <param name="source">The model of KHPI SemesterMarksList.</param>
        /// <param name = "destination">The model of KIP SemesterMarksList.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP SemesterMarksList model.</returns>
        public SemesterMarksList Convert(SemesterMarksListKHPI source, SemesterMarksList destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new SemesterMarksList()
            {
                SubjectId = ConvertExtensions.StringToInt(source.subj_id),
                Subject = source.subject,
                Prof = source.prepod,
                ShortCathedra = source.kabr,
                Cathedra = source.kafedra,
                ShortMark = ConvertExtensions.StringToNullableInt(source.oc_short),
                NationalMark = source.oc_naz,
                FullMark = ConvertExtensions.StringToNullableInt(source.oc_bol),
                ECTSMark = source.oc_ects,
                Credits = ConvertExtensions.StringToNullableFloat(source.credit),
                Control = source.control,
                IsDebt = ConvertExtensions.StringToBool(source.if_hvost),
                Date = source.data,
            };
        }
    }
}
