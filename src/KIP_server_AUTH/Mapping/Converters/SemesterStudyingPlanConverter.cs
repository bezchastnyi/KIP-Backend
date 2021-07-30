using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_Backend.Models.Auth;
using KIP_server_Auth.Models.KhPI;

namespace KIP_server_Auth.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI SemesterStudyingPlan model to the KIP model.
    /// </summary>
    public class SemesterStudyingPlanConverter : ITypeConverter<SemesterStudyingPlanKhPI, SemesterStudyingPlan>
    {
        /// <summary>
        /// Convert model of SemesterStudyingPlan from KhPI to KIP.
        /// </summary>
        /// <param name="source">The model of KhPI SemesterStudyingPlan.</param>
        /// <param name = "destination">The model of KIP SemesterStudyingPlan.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP SemesterStudyingPlan model.</returns>
        public SemesterStudyingPlan Convert(SemesterStudyingPlanKhPI source, SemesterStudyingPlan destination, ResolutionContext context)
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
