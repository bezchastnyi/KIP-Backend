// <copyright file="CurrentRankKHPIToKIPConverter.cs" company="KIP">
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
    /// Building of the KIP CurrentRank model from the KhPI CurrentRank.
    /// </summary>
    public class CurrentRankKHPIToKIPConverter : ITypeConverter<CurrentRankKHPI, CurrentRank>
    {
        /// <summary>
        /// Convert model of CurrentRank from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of CurrentRank of KIP model.
        /// </returns>
        /// <param name="source">Model of CurrentRank KHPI.</param>
        /// <param name = "destination">Model of CurrentRank KIP.</param>
        /// <param name= "context">The context. </param>
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
                FullRankMark = ConvertExtensions.StringToNullableInt(source.sbal100),
                ShortRankMark = ConvertExtensions.StringToNullableInt(source.sbal5),
                RankFormula = source.rating,
            };
        }
    }
}
