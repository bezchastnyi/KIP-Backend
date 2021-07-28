using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_Backend.Models.KIP.Auth;
using KIP_server_Auth.Models.KhPI;

namespace KIP_server_Auth.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI DebtList model to the KIP model.
    /// </summary>
    public class DebtListConverter : ITypeConverter<DebtListKhPI, DebtList>
    {
        /// <summary>
        /// Convert model of DebtList from KhPI to KIP.
        /// </summary>
        /// <param name="source">The model of KhPI DebtList.</param>
        /// <param name = "destination">The model of KIP DebtList.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP DebtList model.</returns>
        public DebtList Convert(DebtListKhPI source, DebtList destination, ResolutionContext context)
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
