using AutoMapper;
using KIP_POST_APP.Mapping.Converters;
using KIP_POST_APP.Models.KHPIDB;
using KIP_POST_APP.Models.KIPDB;
using System.Collections.Generic;

namespace KIP_POST_APP.Mapping
{
    public class MapperProfile : Profile
    {
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
