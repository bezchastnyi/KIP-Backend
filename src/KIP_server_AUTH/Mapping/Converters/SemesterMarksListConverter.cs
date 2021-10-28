using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_Backend.Models.Auth;
using KIP_server_Auth.Models.KhPI;

namespace KIP_server_Auth.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI SemesterMarksList model to the KIP model.
    /// </summary>
    public class SemesterMarksListConverter : ITypeConverter<SemesterMarksListKhPI, SemesterMarksList>
    {
        /// <summary>
        /// Convert model of SemesterMarksList from KhPI to KIP.
        /// </summary>
        /// <param name="source">The model of KhPI SemesterMarksList.</param>
        /// <param name = "destination">The model of KIP SemesterMarksList.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP SemesterMarksList model.</returns>
        public SemesterMarksList Convert(SemesterMarksListKhPI source, SemesterMarksList destination, ResolutionContext context)
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
                IsDebt = ConvertExtensions.StringToBool(source.if_hvost), // TODO make valid
                Date = source.data,
            };
        }
    }
}
