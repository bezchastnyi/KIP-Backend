// <copyright file="MappedDataToKIPDB.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KIP_POST_APP.Models.KIP;
using Microsoft.Extensions.Logging;

namespace KIP_POST_APP.Services
{
    /// <summary>
    /// Mapping data to the KIP database.
    /// </summary>
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

        /// <summary>
        /// Getting list of faculty to KIP.
        /// </summary>
        /// <returns>
        /// List of faculty.
        /// </returns>
        /// <param name="logger">Building KHPI.</param>
        /// <param name = "mapper">A g</param>
        /// <param name= "stoppingToken">A </param>
        [Obsolete]
        public static async Task<List<Faculty>> GetFacultyListKIPAsync(ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kIPFacultyList = new List<Faculty>();

            try
            {
                var facultyList = await GetDataFromKHPIDB.GetFacultyListAsync(stoppingToken);
                if (facultyList == null)
                {
                    logger.LogWarning(nameof(facultyList) + " is null");
                }
                else
                {
                    kIPFacultyList = mapper.Map<List<Faculty>>(facultyList);
                }

                FacultyList = new HashSet<Faculty>(kIPFacultyList);
                return kIPFacultyList;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting list of groups by faculty to KIP.
        /// </summary>
        /// <returns>
        /// List of groups.
        /// </returns>
        /// <param name="kipFacultyList">Faculty KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<Group>> GetGroupListByFacultyKIPAsync(
            List<Faculty> kipFacultyList, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kipGroupListByFaculty = new List<Group>();

            try
            {
                if (kipFacultyList != null)
                {
                    foreach (var faculty in kipFacultyList)
                    {
                        var groupList = await GetDataFromKHPIDB.GetGroupListByFacultyIdAsync(faculty.FacultyID, stoppingToken);
                        if (groupList == null)
                        {
                            logger.LogWarning($"{nameof(groupList)} is null: {nameof(faculty)} - {faculty.FacultyID}/{faculty.FacultyName}");
                            continue;
                        }

                        var groupList1 = mapper.Map<List<Group>>(groupList);
                        if (groupList1 != null)
                        {
                            foreach (var group in groupList1)
                            {
                                group.FacultyID = faculty.FacultyID;
                                kipGroupListByFaculty.Add(group);
                            }
                        }
                    }
                }

                GroupList = new HashSet<Group>(kipGroupListByFaculty);
                return kipGroupListByFaculty;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting list of department by faculty to KIP.
        /// </summary>
        /// <returns>
        /// List of department.
        /// </returns>
        /// <param name="kipFacultyList">Faculty KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<Cathedra>> GetCathedraListByFacultyKIPAsync(
            List<Faculty> kipFacultyList, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kipCathedraListByFaculty = new List<Cathedra>();

            try
            {
                if (kipFacultyList != null)
                {
                    foreach (var faculty in kipFacultyList)
                    {
                        var cathedraList = await GetDataFromKHPIDB.GetCathedraListByFacultyIdAsync(faculty.FacultyID, stoppingToken);
                        if (cathedraList == null)
                        {
                            logger.LogWarning($"{nameof(cathedraList)} is null: {nameof(faculty)} - {faculty.FacultyID}/{faculty.FacultyName}");
                            continue;
                        }

                        var cathedraList1 = mapper.Map<List<Cathedra>>(cathedraList);
                        if (cathedraList1 != null)
                        {
                            foreach (var cathedra in cathedraList1)
                            {
                                cathedra.FacultyID = faculty.FacultyID;
                                kipCathedraListByFaculty.Add(cathedra);
                            }
                        }
                    }
                }

                CathedraList = new HashSet<Cathedra>(kipCathedraListByFaculty);
                return kipCathedraListByFaculty;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting list of building to KIP.
        /// </summary>
        /// <returns>
        /// List of building.
        /// </returns>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<Building>> GetBuildingListKIPAsync(
            ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kipBuildingList = new List<Building>();

            try
            {
                var buildingList = await GetDataFromKHPIDB.GetBuildingListAsync(stoppingToken);
                if (buildingList == null)
                {
                    logger.LogWarning(nameof(buildingList) + " is null");
                }
                else
                {
                    kipBuildingList = mapper.Map<List<Building>>(buildingList);
                }

                BuildingList = new HashSet<Building>(kipBuildingList);
                return kipBuildingList;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting list of building audiences to KIP.
        /// </summary>
        /// <returns>
        /// List of audiences.
        /// </returns>
        /// <param name="kipBuildingList">Building KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<Audience>> GetAudienceListByBuildingKIPAsync(
            List<Building> kipBuildingList, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kipAudienceListByBuilding = new List<Audience>();

            try
            {
                if (kipBuildingList != null)
                {
                    foreach (var building in kipBuildingList)
                    {
                        var audienceList = await GetDataFromKHPIDB.GetAudienceListByBuildingIdAsync(building.BuildingID, stoppingToken);
                        if (audienceList == null)
                        {
                            logger.LogWarning($"{nameof(audienceList)} is null: {nameof(building)} - {building.BuildingID}/{building.BuildingName}");
                            continue;
                        }

                        var audienceList1 = mapper.Map<List<Audience>>(audienceList);
                        if (audienceList1 != null)
                        {
                            foreach (var audience in audienceList1)
                            {
                                audience.BuildingID = building.BuildingID;
                                kipAudienceListByBuilding.Add(audience);
                            }
                        }
                    }
                }

                AudienceList = new HashSet<Audience>(kipAudienceListByBuilding);
                return kipAudienceListByBuilding;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting list of teachers by department to KIP.
        /// </summary>
        /// <returns>
        /// List of teachers by department.
        /// </returns>
        /// <param name="kipCathedraListByFaculty">Building KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<Prof>> GetProfListByCathedraKIPAsync(
            List<Cathedra> kipCathedraListByFaculty, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kipProfListByCathedra = new List<Prof>();

            try
            {
                if (kipCathedraListByFaculty != null)
                {
                    foreach (var cathedra in kipCathedraListByFaculty)
                    {
                        var profList = await GetDataFromKHPIDB.GetProfListByCathedraIdAsync(cathedra.CathedraID, stoppingToken);
                        if (profList == null)
                        {
                            logger.LogWarning($"{nameof(profList)} is null: {nameof(cathedra)} - {cathedra.CathedraID}/{cathedra.CathedraName}");
                            continue;
                        }

                        var profList1 = mapper.Map<List<Prof>>(profList);
                        if (profList1 != null)
                        {
                            foreach (var prof in profList1)
                            {
                                prof.CathedraID = cathedra.CathedraID;
                                kipProfListByCathedra.Add(prof);
                            }
                        }
                    }
                }

                ProfList = new HashSet<Prof>(kipProfListByCathedra);
                return kipProfListByCathedra;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting schedule of group by faculty for an unpaired week to KIP.
        /// </summary>
        /// <returns>
        /// Schedule of group by faculty for an unpaired week.
        /// </returns>
        /// <param name="kipGroupByFaculty">Building KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<StudentSchedule>> GetScheduleListByGroupAsync(
            List<Group> kipGroupByFaculty, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kipScheduleByGroup = new List<StudentSchedule>();

            try
            {
                if (kipGroupByFaculty != null)
                {
                    foreach (var group in kipGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByGroupIdAsync(group.GroupID, stoppingToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                lesson.GroupID = group.GroupID;
                                kipScheduleByGroup.Add(lesson);
                            }
                        }

                    }
                }

                ScheduleList = new HashSet<StudentSchedule>(kipScheduleByGroup);
                return kipScheduleByGroup;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting schedule of group by faculty for a paired week to KIP.
        /// </summary>
        /// <returns>
        /// Schedule of group by faculty for a paired week.
        /// </returns>
        /// <param name="kipGroupByFaculty">Building KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<StudentSchedule>> GetSchedule2ListByGroupAsync(
            List<Group> kipGroupByFaculty, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper,  CancellationToken stoppingToken)
        {
            var kipSchedule2ByGroup = new List<StudentSchedule>();

            try
            {
                if (kipGroupByFaculty != null)
                {
                    foreach (var group in kipGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByGroupIdAsync(group.GroupID, stoppingToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                lesson.GroupID = group.GroupID;
                                kipSchedule2ByGroup.Add(lesson);
                            }
                        }
                    }
                }

                Schedule2List = new HashSet<StudentSchedule>(kipSchedule2ByGroup);
                return kipSchedule2ByGroup;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting schedule of teachers for an unpaired week to KIP.
        /// </summary>
        /// <returns>
        /// Schedule of teachers for an unpaired week.
        /// </returns>
        /// <param name="kipProfByCathedra">Building KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<ProfSchedule>> GetScheduleListByProfAsync(
            List<Prof> kipProfByCathedra, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
        {
            var kipScheduleByProf = new List<ProfSchedule>();

            try
            {
                if (kipProfByCathedra != null)
                {
                    foreach (var prof in kipProfByCathedra)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByProfIdAsync(prof.ProfID, stoppingToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<ProfSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                lesson.ProfID = prof.ProfID;
                                kipScheduleByProf.Add(lesson);
                            }
                        }
                    }
                }

                ProfScheduleList = new HashSet<ProfSchedule>(kipScheduleByProf);
                return kipScheduleByProf;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting schedule of teachers for a paired week to KIP.
        /// </summary>
        /// <returns>
        /// Schedule of teachers for a paired week.
        /// </returns>
        /// <param name="kipProfByCathedra">Building KHPI.</param>
        /// <param name = "logger">A g</param>
        /// <param name= "mapper">A </param>
        /// <param name= "stoppingToken">Stop token. </param>
        [Obsolete]
        public static async Task<List<ProfSchedule>> GetSchedule2ListByProfAsync(
            List<Prof> KIPProfByCathedra, ILogger<KIP_POST_APPHostedService> logger, IMapper mapper, CancellationToken stoppingToken)
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
