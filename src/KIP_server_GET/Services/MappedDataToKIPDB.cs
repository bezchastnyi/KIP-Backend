// <copyright file="MappedDataToKIPDB.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_server_GET.Models.KIP;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Logging;

namespace KIP_server_GET.Services
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
        /// Gets or sets the list of schedule of audience for a unpaired week.
        /// </summary>
        /// <value>List of schedule of audience for a unpaired week.</value>
        public static HashSet<AudienceSchedule> AudienceScheduleList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of audience for a paired week.
        /// </summary>
        /// <value>List of schedule of audience for a paired week.</value>
        public static HashSet<AudienceSchedule> AudienceSchedule2List { get; set; } = null;

        /// <summary>
        /// Getting list of faculty to KIP.
        /// </summary>
        /// <returns>
        /// List of faculty.
        /// </returns>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        public static async Task<List<Faculty>> GetFacultyListKIPAsync(
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kIPFacultyList = new List<Faculty>();

            try
            {
                var facultyList = await GetDataFromKHPIDB.GetFacultyListAsync();
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
        public static async Task<List<Group>> GetGroupListByFacultyKIPAsync(
            List<Faculty> kipFacultyList,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipGroupListByFaculty = new List<Group>();

            try
            {
                if (kipFacultyList != null)
                {
                    foreach (var faculty in kipFacultyList)
                    {
                        var groupList = await GetDataFromKHPIDB.GetGroupListByFacultyIdAsync(faculty.FacultyID);
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
                        else
                        {
                            logger.LogWarning($"{nameof(groupList1)} is null: {nameof(faculty)} - {faculty.FacultyID}/{faculty.FacultyName}");
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
        public static async Task<List<Cathedra>> GetCathedraListByFacultyKIPAsync(
            List<Faculty> kipFacultyList,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipCathedraListByFaculty = new List<Cathedra>();

            try
            {
                if (kipFacultyList != null)
                {
                    foreach (var faculty in kipFacultyList)
                    {
                        var cathedraList = await GetDataFromKHPIDB.GetCathedraListByFacultyIdAsync(faculty.FacultyID);
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
                        else
                        {
                            logger.LogWarning($"{nameof(cathedraList1)} is null: {nameof(faculty)} - {faculty.FacultyID}/{faculty.FacultyName}");
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
        public static async Task<List<Building>> GetBuildingListKIPAsync(
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipBuildingList = new List<Building>();

            try
            {
                var buildingList = await GetDataFromKHPIDB.GetBuildingListAsync();
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
        public static async Task<List<Audience>> GetAudienceListByBuildingKIPAsync(
            List<Building> kipBuildingList,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipAudienceListByBuilding = new List<Audience>();

            try
            {
                if (kipBuildingList != null)
                {
                    foreach (var building in kipBuildingList)
                    {
                        var audienceList = await GetDataFromKHPIDB.GetAudienceListByBuildingIdAsync(building.BuildingID);
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
                        else
                        {
                            logger.LogWarning($"{nameof(audienceList1)} is null: {nameof(building)} - {building.BuildingID}/{building.BuildingName}");
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
        public static async Task<List<Prof>> GetProfListByCathedraKIPAsync(
            List<Cathedra> kipCathedraListByFaculty,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipProfListByCathedra = new List<Prof>();

            try
            {
                if (kipCathedraListByFaculty != null)
                {
                    foreach (var cathedra in kipCathedraListByFaculty)
                    {
                        var profList = await GetDataFromKHPIDB.GetProfListByCathedraIdAsync(cathedra.CathedraID);
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

                                    /*
                                    prof.CathedraID = new List<int>()
                                    {
                                        cathedra.CathedraID,
                                    };

                                    if (kipProfListByCathedra.Count > 0)
                                    {
                                        var exists = false;

                                        foreach (var prof_ in kipProfListByCathedra)
                                        {
                                            if (prof.ProfSurname == prof_.ProfSurname)
                                            {
                                                prof_.CathedraID.Add(cathedra.CathedraID);
                                                exists = true;
                                                break;
                                            }
                                        }

                                        if (!exists)
                                        {
                                            kipProfListByCathedra.Add(prof);
                                        }
                                    }
                                    else
                                    {
                                        kipProfListByCathedra.Add(prof);
                                    }
                                    */
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning($"{nameof(profList1)} is null: {nameof(cathedra)} - {cathedra.CathedraID}/{cathedra.CathedraName}");
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
        public static async Task<List<StudentSchedule>> GetScheduleListByGroupAsync(
            List<Group> kipGroupByFaculty,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipScheduleByGroup = new List<StudentSchedule>();

            try
            {
                if (kipGroupByFaculty != null)
                {
                    foreach (var group in kipGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByGroupIdAsync(group.GroupID);

                        if (schedule == null)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.GroupID = group.GroupID;
                                    group.ScheduleIsPresent[(int)lesson.Day] = true;

                                    kipScheduleByGroup.Add(lesson);
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning($"{nameof(schedule1)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
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
        public static async Task<List<StudentSchedule>> GetSchedule2ListByGroupAsync(
            List<Group> kipGroupByFaculty,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipSchedule2ByGroup = new List<StudentSchedule>();

            try
            {
                if (kipGroupByFaculty != null)
                {
                    foreach (var group in kipGroupByFaculty)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByGroupIdAsync(group.GroupID);

                        if (schedule == null)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.GroupID = group.GroupID;
                                    group.ScheduleIsPresent[(int)lesson.Day] = true;

                                    kipSchedule2ByGroup.Add(lesson);
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning($"{nameof(schedule1)} is null: {nameof(group)} - {group.GroupID}/{group.GroupName}");
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
        public static async Task<List<ProfSchedule>> GetScheduleListByProfAsync(
            List<Prof> kipProfByCathedra,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipScheduleByProf = new List<ProfSchedule>();

            try
            {
                if (kipProfByCathedra != null)
                {
                    foreach (var prof in kipProfByCathedra)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByProfIdAsync(prof.ProfID);

                        if (schedule == null)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<ProfSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.ProfID = prof.ProfID;
                                    prof.ScheduleIsPresent[(int)lesson.Day] = true;

                                    kipScheduleByProf.Add(lesson);
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning($"{nameof(schedule1)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
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
        public static async Task<List<ProfSchedule>> GetSchedule2ListByProfAsync(
            List<Prof> kipProfByCathedra,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipSchedule2ByProf = new List<ProfSchedule>();

            try
            {
                if (kipProfByCathedra != null)
                {
                    foreach (var prof in kipProfByCathedra)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByProfIdAsync(prof.ProfID);

                        if (schedule == null)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<ProfSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.ProfID = prof.ProfID;
                                    prof.ScheduleIsPresent[(int)lesson.Day] = true;

                                    kipSchedule2ByProf.Add(lesson);
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning($"{nameof(schedule1)} is null: {nameof(prof)} - {prof.ProfID}/{prof.ProfSurname}");
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

        /// <summary>
        /// Getting schedule of audience for an unpaired week to KIP.
        /// </summary>
        /// <returns>
        /// Schedule of audience for an unpaired week.
        /// </returns>
        /// <param name="kipAudienceByBuilding">The KIP teacher by department list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        public static async Task<List<AudienceSchedule>> GetScheduleListByAudienceAsync(
            List<Audience> kipAudienceByBuilding,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipScheduleByAudience = new List<AudienceSchedule>();

            try
            {
                if (kipAudienceByBuilding != null)
                {
                    foreach (var audience in kipAudienceByBuilding)
                    {
                        var schedule = await GetDataFromKHPIDB.GetScheduleByAudienceIdAsync(audience.AudienceID);

                        if (schedule == null)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: " +
                                $"{nameof(audience)} - {audience.AudienceID}/{audience.AudienceName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<AudienceSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.BuildingID = audience.BuildingID;
                                    lesson.AudienceID = audience.AudienceID;
                                    audience.ScheduleIsPresent[(int)lesson.Day] = true;

                                    kipScheduleByAudience.Add(lesson);
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning($"{nameof(schedule1)} is null: " +
                                $"{nameof(audience)} - {audience.AudienceID}/{audience.AudienceName}");
                        }
                    }
                }

                AudienceScheduleList = new HashSet<AudienceSchedule>(kipScheduleByAudience);
                return kipScheduleByAudience;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Getting schedule of audience for a paired week to KIP.
        /// </summary>
        /// <returns>
        /// Schedule of audience for a paired week.
        /// </returns>
        /// <param name="kipAudienceByBuilding">The KIP teacher by department list.</param>
        /// <param name = "logger">The logger.</param>
        /// <param name= "mapper">The mapper. </param>
        public static async Task<List<AudienceSchedule>> GetSchedule2ListByAudienceAsync(
            List<Audience> kipAudienceByBuilding,
            ILogger<DbUpdateController> logger,
            IMapper mapper)
        {
            var kipSchedule2ByAudience = new List<AudienceSchedule>();

            try
            {
                if (kipAudienceByBuilding != null)
                {
                    foreach (var audience in kipAudienceByBuilding)
                    {
                        var schedule = await GetDataFromKHPIDB.GetSchedule2ByAudienceIdAsync(audience.AudienceID);

                        if (schedule == null)
                        {
                            logger.LogWarning($"{nameof(schedule)} is null: " +
                                $"{nameof(audience)} - {audience.AudienceID}/{audience.AudienceName}");
                            continue;
                        }

                        var schedule1 = mapper.Map<List<AudienceSchedule>>(schedule);
                        if (schedule1 != null)
                        {
                            foreach (var lesson in schedule1)
                            {
                                if (lesson != null)
                                {
                                    lesson.BuildingID = audience.BuildingID;
                                    lesson.AudienceID = audience.AudienceID;
                                    audience.ScheduleIsPresent[(int)lesson.Day] = true;

                                    kipSchedule2ByAudience.Add(lesson);
                                }
                            }
                        }
                        else
                        {
                            logger.LogWarning($"{nameof(schedule1)} is null: " +
                                $"{nameof(audience)} - {audience.AudienceID}/{audience.AudienceName}");
                        }
                    }
                }

                AudienceSchedule2List = new HashSet<AudienceSchedule>(kipSchedule2ByAudience);
                return kipSchedule2ByAudience;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + ": " + e.StackTrace);
            }

            return null;
        }
    }
}
