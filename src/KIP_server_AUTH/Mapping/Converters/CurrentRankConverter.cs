using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_Backend.Models.KIP.Auth;
using KIP_server_Auth.Models.KhPI;

namespace KIP_server_Auth.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI CurrentRank model to the KIP model.
    /// </summary>
    public class CurrentRankConverter : ITypeConverter<CurrentRankKhPI, CurrentRank>
    {
        /// <summary>
        /// Convert model of CurrentRank from KhPI to KIP.
        /// </summary>
        /// <param name="source">The model of KhPI CurrentRank.</param>
        /// <param name = "destination">The model of KIP CurrentRank.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP CurrentRank model.</returns>
        public CurrentRank Convert(CurrentRankKhPI source, CurrentRank destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new CurrentRank()
            {
                Rank = ConvertExtensions.StringToInt(source.n),
                StudentId = ConvertExtensions.StringToInt(source.studid),
                FIO = source.fio,
                Group = source.group,
                FullRankMark = ConvertExtensions.StringToNullableFloat(source.sbal100),
                ShortRankMark = ConvertExtensions.StringToNullableFloat(source.sbal5),
                RankFormula = source.rating,
            };
        }
    }
}
