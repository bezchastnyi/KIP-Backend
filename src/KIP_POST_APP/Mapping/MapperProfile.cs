// <copyright file="MapperProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
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
            this.CreateMap<Faculty_KHPI, Faculty>().ConvertUsing<Faculty_KHPIToFaculty_KIPConverter>();
            this.CreateMap<Cathedra_KHPI, Cathedra>().ConvertUsing<Cathedra_KHPIToCathedra_KIPConverter>();
            this.CreateMap<Group_KHPI, Group>().ConvertUsing<GroupByFacultyId_KHPIToGroupByFaculty_KIPConverter>();
            this.CreateMap<Building_KHPI, Building>().ConvertUsing<Building_KHPIToBuilding_KIPConverter>();
            this.CreateMap<Audience_KHPI, Audience>().ConvertUsing<Audience_KHPIToAudience_KIPConverter>();
            this.CreateMap<Prof_KHPI, Prof>().ConvertUsing<Prof_KHPIToProf_KIPConverter>();
            this.CreateMap<ScheduleByGroup_KHPI, List<StudentSchedule>>().ConvertUsing<ScheduleByGroup_KHPIToListOfSchedule_KIPConverter>();
            this.CreateMap<ScheduleByProf_KHPI, List<ProfSchedule>>().ConvertUsing<ScheduleByProf_KHPIToListOfProfSchedule_KIPConverter>();
        }
    }
}
