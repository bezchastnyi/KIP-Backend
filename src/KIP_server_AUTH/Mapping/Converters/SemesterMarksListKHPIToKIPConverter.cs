// <copyright file="SemesterMarksListKHPIToKIPConverter.cs" company="KIP">
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
    /// Building of the KIP SemesterMarksList model from the KhPI SemesterMarksList.
    /// </summary>
    public class SemesterMarksListKHPIToKIPConverter : ITypeConverter<SemesterMarksListKHPI, SemesterMarksList>
    {
        /// <summary>
        /// Convert model of SemesterMarksList from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of SemesterMarksList of KIP model.
        /// </returns>
        /// <param name="source">Model of SemesterMarksList KHPI.</param>
        /// <param name = "destination">Model of SemesterMarksList KIP.</param>
        /// <param name= "context">The context. </param>
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
