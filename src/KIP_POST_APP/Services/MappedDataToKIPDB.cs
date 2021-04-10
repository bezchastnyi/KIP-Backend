using AutoMapper;
using KIP_POST_APP.Models.KIP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KIP_POST_APP.Services
{
    public static class MappedDataToKIPDB
    {
        public static HashSet<Faculty> FacultyList = null;
        public static HashSet<Group> GroupList = null;
        public static HashSet<Cathedra> CathedraList = null;
        public static HashSet<Building> BuildingList = null;
        public static HashSet<Audience> AudienceList = null;
        public static HashSet<Prof> ProfList = null;
        public static HashSet<StudentSchedule> ScheduleList = null;
        public static HashSet<ProfSchedule> ProfScheduleList = null;
        public static HashSet<StudentSchedule> Schedule2List = null;
        public static HashSet<ProfSchedule> ProfSchedule2List = null;


        [Obsolete]
        public static async Task<List<Faculty>> GetFacultyListKIPAsync(ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, 
                                                                           CancellationToken stoppingToken)
        {
            var KIPFacultyList = new List<Faculty>();

            try
            {
                var facultyList = await GetDataFromKHPIDB.GetFacultyListAsync(stoppingToken);
                if (facultyList == null)
                {
                    logger.LogWarning(nameof(facultyList) + " is null");
                }
                else
                {
                    KIPFacultyList = mapper.Map<List<Faculty>>(facultyList);
                }

                FacultyList = new HashSet<Faculty>(KIPFacultyList);
                return KIPFacultyList;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<Group>> GetGroupListByFacultyKIPAsync(List<Faculty> KIPFacultyList,
                                                                                ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                CancellationToken stoppingToken)
        {
            var KIPGroupListByFaculty = new List<Group>();

            try
            {
                if (KIPFacultyList != null)
                {
                    foreach (var faculty in KIPFacultyList)
                    {
                        var groupList = await GetDataFromKHPIDB.GetGroupListByFacultyIdAsync(faculty.FacultyID, stoppingToken);
                        if (groupList == null)
                        {
                            logger.LogWarning($"{nameof(groupList)} is null: {nameof(faculty)} - {faculty.FacultyID}/{faculty.FacultyName}");
                            continue;
                        }

                        var GroupList = mapper.Map<List<Group>>(groupList);
                        if (GroupList != null)
                        {
                            foreach (var group in GroupList)
                            {
                                group.FacultyID = faculty.FacultyID;
                                KIPGroupListByFaculty.Add(group);
                            }
                        }
                    }
                }

                GroupList = new HashSet<Group>(KIPGroupListByFaculty);
                return KIPGroupListByFaculty;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<Cathedra>> GetCathedraListByFacultyKIPAsync(List<Faculty> KIPFacultyList,
                                                                                      ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                      CancellationToken stoppingToken)
        {
            var KIPCathedraListByFaculty = new List<Cathedra>();

            try
            {
                if (KIPFacultyList != null)
                {
                    foreach (var faculty in KIPFacultyList)
                    {
                        var cathedraList = await GetDataFromKHPIDB.GetCathedraListByFacultyIdAsync(faculty.FacultyID, stoppingToken);
                        if (cathedraList == null)
                        {
                            logger.LogWarning($"{nameof(cathedraList)} is null: {nameof(faculty)} - {faculty.FacultyID}/{faculty.FacultyName}");
                            continue;
                        }

                        var CathedraList = mapper.Map<List<Cathedra>>(cathedraList);
                        if (CathedraList != null)
                        {
                            foreach (var cathedra in CathedraList)
                            {
                                cathedra.FacultyID = faculty.FacultyID;
                                KIPCathedraListByFaculty.Add(cathedra);
                            }
                        }
                    }
                }

                CathedraList = new HashSet<Cathedra>(KIPCathedraListByFaculty);
                return KIPCathedraListByFaculty;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<Building>> GetBuildingListKIPAsync(ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                             CancellationToken stoppingToken)
        {
            var KIPBuildingList = new List<Building>();

            try
            {
                var buildingList = await GetDataFromKHPIDB.GetBuildingListAsync(stoppingToken);
                if (buildingList == null)
                {
                    logger.LogWarning(nameof(buildingList) + " is null");
                }
                else
                {
                    KIPBuildingList = mapper.Map<List<Building>>(buildingList);
                }

                BuildingList = new HashSet<Building>(KIPBuildingList);
                return KIPBuildingList;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<Audience>> GetAudienceListByBuildingKIPAsync(List<Building> KIPBuildingList,
                                                                                       ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                       CancellationToken stoppingToken)
        {
            var KIPAudienceListByBuilding = new List<Audience>();

            try
            {
                if (KIPBuildingList != null)
                {
                    foreach (var building in KIPBuildingList)
                    {
                        var audienceList = await GetDataFromKHPIDB.GetAudienceListByBuildingIdAsync(building.BuildingID, stoppingToken);
                        if (audienceList == null)
                        {
                            logger.LogWarning($"{nameof(audienceList)} is null: {nameof(building)} - {building.BuildingID}/{building.BuildingName}");
                            continue;
                        }

                        var AudienceList = mapper.Map<List<Audience>>(audienceList);
                        if (AudienceList != null)
                        {
                            foreach (var audience in AudienceList)
                            {
                                audience.BuildingID = building.BuildingID;
                                KIPAudienceListByBuilding.Add(audience);
                            }
                        }
                    }
                }

                AudienceList = new HashSet<Audience>(KIPAudienceListByBuilding);
                return KIPAudienceListByBuilding;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<Prof>> GetProfListByCathedraKIPAsync(List<Cathedra> KIPCathedraListByFaculty,
                                                                                   ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                   CancellationToken stoppingToken)
        {
            var KIPProfListByCathedra = new List<Prof>();

            try
            {
                if (KIPCathedraListByFaculty != null)
                {
                    foreach (var cathedra in KIPCathedraListByFaculty)
                    {
                        var profList = await GetDataFromKHPIDB.GetProfListByCathedraIdAsync(cathedra.CathedraID, stoppingToken);
                        if (profList == null)
                        {
                            logger.LogWarning($"{nameof(profList)} is null: {nameof(cathedra)} - {cathedra.CathedraID}/{cathedra.CathedraName}");
                            continue;
                        }

                        var ProfList = mapper.Map<List<Prof>>(profList);
                        if (ProfList != null)
                        {
                            foreach (var prof in ProfList)
                            {
                                prof.CathedraID = cathedra.CathedraID;
                                KIPProfListByCathedra.Add(prof);
                            }
                        }
                    }
                }

                ProfList = new HashSet<Prof>(KIPProfListByCathedra);
                return KIPProfListByCathedra;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<StudentSchedule>> GetScheduleListByGroupAsync(List<Group> KIPGroupByFaculty,
                                                                                    ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                    CancellationToken stoppingToken)
        {
            var KIPScheduleByGroup = new List<StudentSchedule>();

            try
            {
                if (KIPGroupByFaculty != null)
                {
                    foreach (var group in KIPGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByGroupIdAsync(group.GroupID, stoppingToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var Schedule = mapper.Map<List<StudentSchedule>>(schedule);
                        if (Schedule != null)
                        {
                            foreach (var lesson in Schedule)
                            {
                                lesson.GroupID = group.GroupID;
                                KIPScheduleByGroup.Add(lesson);
                            }
                        }

                    }
                }

                ScheduleList = new HashSet<StudentSchedule>(KIPScheduleByGroup);
                return KIPScheduleByGroup;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<StudentSchedule>> GetSchedule2ListByGroupAsync(List<Group> KIPGroupByFaculty,
                                                                                     ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                     CancellationToken stoppingToken)
        {
            var KIPSchedule2ByGroup = new List<StudentSchedule>();

            try
            {
                if (KIPGroupByFaculty != null)
                {
                    foreach (var group in KIPGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByGroupIdAsync(group.GroupID, stoppingToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var Schedule = mapper.Map<List<StudentSchedule>>(schedule);
                        if (Schedule != null)
                        {
                            foreach (var lesson in Schedule)
                            {
                                lesson.GroupID = group.GroupID;
                                KIPSchedule2ByGroup.Add(lesson);
                            }
                        }

                    }
                }

                Schedule2List = new HashSet<StudentSchedule>(KIPSchedule2ByGroup);
                return KIPSchedule2ByGroup;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<ProfSchedule>> GetScheduleListByProfAsync(List<Prof> KIPProfByCathedra,
                                                                                ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                CancellationToken stoppingToken)
        {
            var KIPScheduleByProf = new List<ProfSchedule>();

            try
            {
                if (KIPProfByCathedra != null)
                {
                    foreach (var prof in KIPProfByCathedra)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByProfIdAsync(prof.ProfID, stoppingToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
                            continue;
                        }

                        var Schedule = mapper.Map<List<ProfSchedule>>(schedule);
                        if (Schedule != null)
                        {
                            foreach (var lesson in Schedule)
                            {
                                lesson.ProfID = prof.ProfID;
                                KIPScheduleByProf.Add(lesson);
                            }
                        }

                    }
                }

                ProfScheduleList = new HashSet<ProfSchedule>(KIPScheduleByProf);
                return KIPScheduleByProf;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [Obsolete]
        public static async Task<List<ProfSchedule>> GetSchedule2ListByProfAsync(List<Prof> KIPProfByCathedra,
                                                                                 ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,
                                                                                 CancellationToken stoppingToken)
        {
            var KIPSchedule2ByProf = new List<ProfSchedule>();

            try
            {
                if (KIPProfByCathedra != null)
                {
                    foreach (var prof in KIPProfByCathedra)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByProfIdAsync(prof.ProfID, stoppingToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
                            continue;
                        }

                        var Schedule = mapper.Map<List<ProfSchedule>>(schedule);
                        if (Schedule != null)
                        {
                            foreach (var lesson in Schedule)
                            {
                                lesson.ProfID = prof.ProfID;
                                KIPSchedule2ByProf.Add(lesson);
                            }
                        }

                    }
                }

                ProfSchedule2List = new HashSet<ProfSchedule>(KIPSchedule2ByProf);
                return KIPSchedule2ByProf;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }
    }
}
