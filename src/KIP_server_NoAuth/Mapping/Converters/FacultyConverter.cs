using System;
using System.Linq;
using AutoMapper;
using KIP_Backend.Constants;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.Constants;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP faculty model from the KhPI faculty.
    /// </summary>
    public class FacultyConverter : ITypeConverter<FacultyKhPI, Faculty>
    {
        /// <summary>
        /// Convert model of faculty from KhPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of faculty of model faculty KIP.
        /// </returns>
        /// <param name="source">Model of faculty KhPI.</param>
        /// <param name="destination">Model of faculty KIP.</param>
        /// <param name="context">The context. </param>
        public Faculty Convert(FacultyKhPI source, Faculty destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            var shortName = FacultiesShortNames.ShortNames
                .FirstOrDefault(f => source.title.Contains(f.Key)).Value;

            return new Faculty
            {
                FacultyId = source.id,
                FacultyName = source.title,
                FacultyShortName = shortName,
            };
        }
    }
}
