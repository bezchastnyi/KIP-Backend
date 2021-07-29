// <copyright file="MapperProfile.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using AutoMapper;
using KIP_POST_APP.Mapping.Converters;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Mapping
{
    /// <summary>
    /// Building of the profile KIP model from the KhPI.
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<FacultyKHPI, Faculty>()
                .ConvertUsing<FacultyKHPIToKIPConverter>();

            this.CreateMap<CathedraKHPI, Cathedra>()
                .ConvertUsing<CathedraKHPIToKIPConverter>();

            this.CreateMap<GroupKHPI, Group>()
                .ConvertUsing<GroupByFacultyKHPIToKIPConverter>();

            this.CreateMap<BuildingKHPI, Building>()
                .ConvertUsing<BuildingKHPIToKIPConverter>();

            this.CreateMap<AudienceKHPI, Audience>()
                .ConvertUsing<AudienceKHPIToKIPConverter>();

            this.CreateMap<ProfKHPI, Prof>()
                .ConvertUsing<ProfKHPIToKIPConverter>();

            this.CreateMap<ScheduleByGroupKHPI, List<StudentSchedule>>()
                .ConvertUsing<ScheduleByGroupKHPIToKIPConverter>();

            this.CreateMap<ScheduleByProfKHPI, List<ProfSchedule>>()
                .ConvertUsing<ScheduleByProfKHPIToKIPConverter>();

            this.CreateMap<ScheduleByAudienceKHPI, List<AudienceSchedule>>()
                .ConvertUsing<ScheduleByAudienceKHPIToKIPConverter>();
        }
    }
}
