// <copyright file="SemesterStudyingPlanConverter.cs" company="KIP">
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
    /// Convert KhPI SemesterStudyingPlan model to the KIP model.
    /// </summary>
    public class SemesterStudyingPlanConverter :
        ITypeConverter<SemesterStudyingPlanKHPI, SemesterStudyingPlan>
    {
        /// <summary>
        /// Convert model of SemesterStudyingPlan from KHPI to KIP.
        /// </summary>
        /// <param name="source">The model of KHPI SemesterStudyingPlan.</param>
        /// <param name = "destination">The model of KIP SemesterStudyingPlan.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP SemesterStudyingPlan model.</returns>
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
