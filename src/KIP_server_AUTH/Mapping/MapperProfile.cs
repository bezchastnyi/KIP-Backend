// <copyright file="MapperProfile.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using AutoMapper;
using KIP_server_AUTH.Mapping.Converters;
using KIP_server_AUTH.Models.KHPI;
using KIP_server_AUTH.Models.KIP;

namespace KIP_server_AUTH.Mapping
{
    /// <summary>
    /// MapperProfile.
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<PersonalInformationKHPI, PersonalInformation>()
                .ConvertUsing<PersonalInformationConverter>();

            this.CreateMap<SemesterMarksListKHPI, SemesterMarksList>()
                .ConvertUsing<SemesterMarksListConverter>();

            this.CreateMap<CurrentRankKHPI, CurrentRank>()
                .ConvertUsing<CurrentRankConverter>();

            this.CreateMap<DebtListKHPI, DebtList>()
                .ConvertUsing<DebtListConverter>();

            this.CreateMap<SemesterStudyingPlanKHPI, SemesterStudyingPlan>()
                .ConvertUsing<SemesterStudyingPlanConverter>();
        }
    }
}
