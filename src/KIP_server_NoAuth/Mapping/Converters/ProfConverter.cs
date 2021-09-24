using System;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP teachers model from the KhPI teachers.
    /// </summary>
    public class ProfConverter : ITypeConverter<ProfKhPI, Prof>
    {
        /// <summary>
        /// Convert model of teachers from KhPI to KIP.
        /// </summary>
        /// <returns>Object of teachers of model teachers KIP.</returns>
        /// <param name="source">Model of teachers KhPI.</param>
        /// <param name = "destination">Model of teachers KIP.</param>
        /// <param name= "context">The context. </param>
        public Prof Convert(ProfKhPI source, Prof destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            var list = Regex.Split(source.title, @"\s{1,}|_#./!(),-");

            var profSurname = string.Empty;
            var profName = string.Empty;
            var profPatronymic = string.Empty;

            switch (list.Length)
            {
                case 0:
                    return null;

                case 1:
                    profSurname = list.First();
                    break;

                case 2:
                    profSurname = list.First();
                    profName = list[1];
                    break;

                default:
                    profSurname = list.First();
                    profName = list[1];
                    profPatronymic = list[2];
                    break;
            }

            if (profSurname == string.Empty)
            {
                return null;
            }

            return new Prof
            {
                ProfId = source.id,
                ProfSurname = profSurname,
                ProfName = profName,
                ProfPatronymic = profPatronymic,
            };
        }
    }
}
