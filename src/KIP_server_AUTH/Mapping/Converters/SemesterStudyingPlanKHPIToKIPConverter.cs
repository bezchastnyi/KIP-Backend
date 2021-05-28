// <copyright file="SemesterStudyingPlanKHPIToKIPConverter.cs" company="KIP">
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
    /// Building of the KIP SemesterStudyingPlan model from the KhPI SemesterStudyingPlan.
    /// </summary>
    public class SemesterStudyingPlanKHPIToKIPConverter : ITypeConverter<SemesterStudyingPlanKHPI, SemesterStudyingPlan>
    {
        /// <summary>
        /// Convert model of SemesterStudyingPlan from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of SemesterStudyingPlan of KIP model.
        /// </returns>
        /// <param name="source">Model of SemesterStudyingPlan KHPI.</param>
        /// <param name = "destination">Model of SemesterStudyingPlan KIP.</param>
        /// <param name= "context">The context. </param>
        public SemesterStudyingPlan Convert(SemesterStudyingPlanKHPI source, SemesterStudyingPlan destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new SemesterStudyingPlan()
            {
                SubjectId = ConvertExtensions.StringToInt(source.subj_id),
                Subject = source.subject,
                ShortCathedra = source.kabr,
                Cathedra = source.kafedra,
                Course = ConvertExtensions.StringToInt(source.kurs),
                Semester = ConvertExtensions.StringToInt(source.semestr),
                Credits = ConvertExtensions.StringToNullableFloat(source.credit),
                Audits = ConvertExtensions.StringToNullableFloat(source.audit),
                Control = source.control,
            };
        }
    }
}
