using AutoMapper;
using KIP_POST_APP.Models.KIPDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KIP_POST_APP.Services
{
    public static class MappedDataToKIPDB
    {
        public static List<Faculty> FacultyList = null;
        public static List<Group> GroupList = null;
        public static List<Cathedra> CathedraList = null;
        public static List<Building> BuildingList = null;
        public static List<Audience> AudienceList = null;
        public static List<Prof> ProfList = null;
        public static List<StudentSchedule> ScheduleList = null;
        public static List<ProfSchedule> ProfScheduleList = null;


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

                FacultyList = KIPFacultyList;
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
                            logger.LogWarning(nameof(groupList) + " is null");
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

                GroupList = KIPGroupListByFaculty;
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
                            logger.LogWarning(nameof(cathedraList) + " is null");
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

                CathedraList = KIPCathedraListByFaculty;
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

                BuildingList = KIPBuildingList;
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
                            logger.LogWarning(nameof(audienceList) + " is null");
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

                AudienceList = KIPAudienceListByBuilding;
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
                            logger.LogWarning(nameof(profList) + " is null");
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

                ProfList = KIPProfListByCathedra;
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
                            logger.LogWarning(nameof(schedule) + " is null");
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

                ScheduleList = KIPScheduleByGroup;
                return KIPScheduleByGroup;
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
                            logger.LogWarning(nameof(schedule) + " is null");
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

                ProfScheduleList = KIPScheduleByProf;
                return KIPScheduleByProf;
            }

            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }
            return null;
        }
    }
}
