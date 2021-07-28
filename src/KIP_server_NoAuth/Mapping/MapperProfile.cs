// <copyright file="MapperProfile.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using AutoMapper;
using KIP_Backend.Models.KIP.NoAuth;
using KIP_server_NoAuth.Mapping.Converters;
using KIP_server_NoAuth.Models.KhPI;

namespace KIP_server_NoAuth.Mapping
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
            this.CreateMap<FacultyKhPI, Faculty>().ConvertUsing<FacultyConverter>();
            this.CreateMap<CathedraKhPI, Cathedra>().ConvertUsing<CathedraConverter>();
            this.CreateMap<GroupKhPI, Group>().ConvertUsing<GroupConverter>();
            this.CreateMap<BuildingKhPI, Building>().ConvertUsing<BuildingConverter>();
            this.CreateMap<AudienceKhPI, Audience>().ConvertUsing<AudienceConverter>();
            this.CreateMap<ProfKhPI, Prof>().ConvertUsing<ProfConverter>();
            this.CreateMap<ScheduleKhPI, List<StudentSchedule>>().ConvertUsing<ScheduleByGroupConverter>();
            this.CreateMap<ScheduleKhPI, List<ProfSchedule>>().ConvertUsing<ScheduleByProfConverter>();
            this.CreateMap<ScheduleKhPI, List<AudienceSchedule>>().ConvertUsing<ScheduleByAudienceConverter>();
        }
    }
}
