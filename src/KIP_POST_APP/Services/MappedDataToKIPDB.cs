// <copyright file="MappedDataToKIPDB.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
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
        /// <summary>
        /// Gets or sets the list of faculties.
        /// </summary>
        /// <value>List of faculties.</value>
        public static HashSet<Faculty> FacultyList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of groups.
        /// </summary>
        /// <value>List of groups.</value>
        public static HashSet<Group> GroupList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of departmens.
        /// </summary>
        /// <value>List of departmens.</value>
        public static HashSet<Cathedra> CathedraList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of buildings.
        /// </summary>
        /// <value>List of buildings.</value>
        public static HashSet<Building> BuildingList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of audiences.
        /// </summary>
        /// <value>List of audiences.</value>
        public static HashSet<Audience> AudienceList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of teachers.
        /// </summary>
        /// <value>List of teachers.</value>
        public static HashSet<Prof> ProfList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of groups for an unpaired week.
        /// </summary>
        /// <value>List of schedule of groups for an unpaired week.</value>
        public static HashSet<StudentSchedule> ScheduleList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of teachers for an unpaired week.
        /// </summary>
        /// <value>List of schedule of teachers for an unpaired week.</value>
        public static HashSet<ProfSchedule> ProfScheduleList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of groups for a paired week.
        /// </summary>
        /// <value>List of schedule of groups for a paired week.</value>
        public static HashSet<StudentSchedule> Schedule2List { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of teachers for a paired week.
        /// </summary>
        /// <value>List of schedule of teachers for a paired week.</value>
        public static HashSet<ProfSchedule> ProfSchedule2List { get; set; } = null;

        /// <summary>
        /// Getting list of faculty to KIP.
        /// </summary>
        /// <returns>
        /// List of faculty.
        /// </returns>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<Faculty>> GetFacultyListKIPAsync(
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kIPFacultyList = new List<Faculty>();

            try
            {
                var facultyList = await GetDataFromKHPIDB.GetFacultyListAsync(cancellationToken);
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
        /// <param name="kipFacultyList">The KIP faculty list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<Group>> GetGroupListByFacultyKIPAsync(
            List<Faculty> kipFacultyList,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipGroupListByFaculty = new List<Group>();

            try
            {
                if (kipFacultyList != null)
                {
                    foreach (var faculty in kipFacultyList)
                    {
                        var groupList = await GetDataFromKHPIDB.GetGroupListByFacultyIdAsync(faculty.FacultyID, cancellationToken);
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
                                if (group != null)
                                {
                                    group.FacultyID = faculty.FacultyID;
                                    kipGroupListByFaculty.Add(group);
                                }
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
        /// <param name="kipFacultyList">The KIP faculty list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<Cathedra>> GetCathedraListByFacultyKIPAsync(
            List<Faculty> kipFacultyList,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipCathedraListByFaculty = new List<Cathedra>();

            try
            {
                if (kipFacultyList != null)
                {
                    foreach (var faculty in kipFacultyList)
                    {
                        var cathedraList = await GetDataFromKHPIDB.GetCathedraListByFacultyIdAsync(faculty.FacultyID, cancellationToken);
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
                                if (cathedra != null)
                                {
                                    cathedra.FacultyID = faculty.FacultyID;
                                    kipCathedraListByFaculty.Add(cathedra);
                                }
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
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<Building>> GetBuildingListKIPAsync(
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipBuildingList = new List<Building>();

            try
            {
                var buildingList = await GetDataFromKHPIDB.GetBuildingListAsync(cancellationToken);
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
        /// <param name="kipBuildingList">The KIP building list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<Audience>> GetAudienceListByBuildingKIPAsync(
            List<Building> kipBuildingList,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipAudienceListByBuilding = new List<Audience>();

            try
            {
                if (kipBuildingList != null)
                {
                    foreach (var building in kipBuildingList)
                    {
                        var audienceList = await GetDataFromKHPIDB.GetAudienceListByBuildingIdAsync(building.BuildingID, cancellationToken);
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
                                if (audience != null)
                                {
                                    audience.BuildingID = building.BuildingID;
                                    kipAudienceListByBuilding.Add(audience);
                                }
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
        /// List of teachers.
        /// </returns>
        /// <param name="kipCathedraListByFaculty">The KIP department by faculty list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<Prof>> GetProfListByCathedraKIPAsync(
            List<Cathedra> kipCathedraListByFaculty,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipProfListByCathedra = new List<Prof>();

            try
            {
                if (kipCathedraListByFaculty != null)
                {
                    foreach (var cathedra in kipCathedraListByFaculty)
                    {
                        var profList = await GetDataFromKHPIDB.GetProfListByCathedraIdAsync(cathedra.CathedraID, cancellationToken);
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
                                if (prof != null)
                                {
                                    prof.CathedraID = cathedra.CathedraID;
                                    kipProfListByCathedra.Add(prof);
                                }
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
        /// <param name="kipGroupByFaculty">The KIP group by faculty list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<StudentSchedule>> GetScheduleListByGroupAsync(
            List<Group> kipGroupByFaculty,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipScheduleByGroup = new List<StudentSchedule>();

            try
            {
                if (kipGroupByFaculty != null)
                {
                    foreach (var group in kipGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByGroupIdAsync(group.GroupID, cancellationToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            group.ScheduleIsPresent = true;
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.GroupID = group.GroupID;
                                    kipScheduleByGroup.Add(lesson);
                                }
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
        /// <param name="kipGroupByFaculty">The KIP group by faculty list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<StudentSchedule>> GetSchedule2ListByGroupAsync(
            List<Group> kipGroupByFaculty,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipSchedule2ByGroup = new List<StudentSchedule>();

            try
            {
                if (kipGroupByFaculty != null)
                {
                    foreach (var group in kipGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByGroupIdAsync(group.GroupID, cancellationToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            group.ScheduleIsPresent = true;
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.GroupID = group.GroupID;
                                    kipSchedule2ByGroup.Add(lesson);
                                }
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
        /// <param name="kipProfByCathedra">The KIP teacher by department list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<ProfSchedule>> GetScheduleListByProfAsync(
            List<Prof> kipProfByCathedra,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipScheduleByProf = new List<ProfSchedule>();

            try
            {
                if (kipProfByCathedra != null)
                {
                    foreach (var prof in kipProfByCathedra)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByProfIdAsync(prof.ProfID, cancellationToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<ProfSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            prof.ScheduleIsPresent = true;
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.ProfID = prof.ProfID;
                                    kipScheduleByProf.Add(lesson);
                                }
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
        /// <param name="kipProfByCathedra">The KIP teacher by department list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        /// <param name= "cancellationToken">The cancellation token. </param>
        public static async Task<List<ProfSchedule>> GetSchedule2ListByProfAsync(
            List<Prof> kipProfByCathedra,
            ILogger<KIP_POST_APPHostedService> logger,
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var kipSchedule2ByProf = new List<ProfSchedule>();

            try
            {
                if (kipProfByCathedra != null)
                {
                    foreach (var prof in kipProfByCathedra)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByProfIdAsync(prof.ProfID, cancellationToken);

                        if (schedule == default)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<ProfSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            prof.ScheduleIsPresent = true;
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.ProfID = prof.ProfID;
                                    kipSchedule2ByProf.Add(lesson);
                                }
                            }
                        }
                    }
                }

                ProfSchedule2List = new HashSet<ProfSchedule>(kipSchedule2ByProf);
                return kipSchedule2ByProf;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }
    }
}
