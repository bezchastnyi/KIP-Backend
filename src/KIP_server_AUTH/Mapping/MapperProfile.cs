// <copyright file="MapperProfile.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using AutoMapper;
using KIP_Backend.Models.KIP.Auth;
using KIP_server_Auth.Mapping.Converters;
using KIP_server_Auth.Models.KhPI;

namespace KIP_server_Auth.Mapping
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
            this.CreateMap<PersonalInformationKhPI, PersonalInformation>().ConvertUsing<PersonalInformationConverter>();
            this.CreateMap<SemesterMarksListKhPI, SemesterMarksList>().ConvertUsing<SemesterMarksListConverter>();
            this.CreateMap<CurrentRankKhPI, CurrentRank>().ConvertUsing<CurrentRankConverter>();
            this.CreateMap<DebtListKhPI, DebtList>().ConvertUsing<DebtListConverter>();
            this.CreateMap<SemesterStudyingPlanKhPI, SemesterStudyingPlan>().ConvertUsing<SemesterStudyingPlanConverter>();
        }
    }
}
