using System;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using KIP_Backend.Constants;
using KIP_Backend.Extensions;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.Constants;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// BuildingConverter.
    /// </summary>
    public class BuildingConverter : ITypeConverter<BuildingKhPI, Building>
    {
        private static readonly Regex RegexPattern = new Regex(@"[\d[0-9]{0,4}]");

        /// <summary>
        /// Convert model of building from KhPI to KIP.
        /// </summary>
        /// <returns>Object of building of model building KIP.</returns>
        /// <param name="source">Model of building KhPI.</param>
        /// <param name="destination">Model of building KIP.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">source.</exception>
        public Building Convert(BuildingKhPI source, Building destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(source.title))
            {
                return null;
            }

            var shortName = BuildingShortNames.ShortNames
                .FirstOrDefault(b => source.title.Contains(b.Key)).Value;

            return new Building
            {
                BuildingId = source.id,
                BuildingName = ConvertExtensions.FixTitle(source.title),
                BuildingShortName = shortName,
                NumberOfAudiences = SearchNumberOfAudiences(source.title),
            };
        }

        private static int? SearchNumberOfAudiences(string title)
        {
            var matches = RegexPattern.Matches(title);
            if (matches.Count == 0)
            {
                return null;
            }

            return int.Parse(matches.First().Value.Trim('[', ']'));
        }
    }
}
