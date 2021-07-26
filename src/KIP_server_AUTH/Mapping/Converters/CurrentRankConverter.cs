// <copyright file="CurrentRankConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_server_Auth.Models.KHPI;
using KIP_server_Auth.Models.KIP;

namespace KIP_server_Auth.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI CurrentRank model to the KIP model.
    /// </summary>
    public class CurrentRankConverter : ITypeConverter<CurrentRankKHPI, CurrentRank>
    {
        /// <summary>
        /// Convert model of CurrentRank from KHPI to KIP.
        /// </summary>
        /// <param name="source">The model of KHPI CurrentRank.</param>
        /// <param name = "destination">The model of KIP CurrentRank.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP CurrentRank model.</returns>
        public CurrentRank Convert(CurrentRankKHPI source, CurrentRank destination, ResolutionContext context)
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
