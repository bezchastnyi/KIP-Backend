// <copyright file="MapService.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Models.KIP;
using KIP_server_NoAuth.V1.Controllers;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.Services
{
    /// <summary>
    /// Mapping data to the KIP database.
    /// </summary>
    public static class MapService
    {
        /// <summary>
        /// Gets or sets the list of faculties.
        /// </summary>
        public static HashSet<Faculty> FacultyList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of groups.
        /// </summary>
        public static HashSet<Group> GroupList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of departmens.
        /// </summary>
        public static HashSet<Cathedra> CathedraList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of buildings.
        /// </summary>
        public static HashSet<Building> BuildingList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of audiences.
        /// </summary>
        public static HashSet<Audience> AudienceList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of teachers.
        /// </summary>
        public static HashSet<Prof> ProfList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of groups for an unpaired week.
        /// </summary>
        public static HashSet<StudentSchedule> GroupScheduleList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of groups for a paired week.
        /// </summary>
        public static HashSet<StudentSchedule> GroupSchedule2List { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of teachers for an unpaired week.
        /// </summary>
        public static HashSet<ProfSchedule> ProfScheduleList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of teachers for a paired week.
        /// </summary>
        public static HashSet<ProfSchedule> ProfSchedule2List { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of audience for a unpaired week.
        /// </summary>
        public static HashSet<AudienceSchedule> AudienceScheduleList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of schedule of audience for a paired week.
        /// </summary>
        public static HashSet<AudienceSchedule> AudienceSchedule2List { get; set; } = null;

        /// <summary>
        /// Getting list of faculty to KIP.
        /// </summary>
        /// <returns>List of faculty.</returns>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Faculty>> GetFacultiesAsync(ILogger<DbUpdateController> logger, IMapper mapper)
        {
            var facultyList = await ReceiveService.GetFacultiesAsync(logger);
            if (facultyList == null)
            {
                logger.LogWarning(nameof(facultyList) + " is null");
                return null;
            }

            var list = mapper.Map<List<Faculty>>(facultyList);
            FacultyList = new HashSet<Faculty>(list);
            return FacultyList;
        }

        /// <summary>
        /// Getting list of groups by faculty to KIP.
        /// </summary>
        /// <returns>List of groups.</returns>
        /// <param name="facultyList">The KIP faculty list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Group>> GetGroupsAsync(HashSet<Faculty> facultyList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (facultyList == null)
            {
                return null;
            }

            GroupList = new HashSet<Group>();
            foreach (var f in facultyList)
            {
                var groupList = await ReceiveService.GetGroupsAsync(f.FacultyID, logger);
                if (groupList == null)
                {
                    logger.LogWarning($"{nameof(groupList)} is null: {nameof(f)} - {f.FacultyID}/{f.FacultyName}");
                    continue;
                }

                var kipGroupList = mapper.Map<List<Group>>(groupList);
                if (kipGroupList != null)
                {
                    foreach (var g in kipGroupList)
                    {
                        if (g != null)
                        {
                            g.FacultyID = f.FacultyID;
                            GroupList.Add(g);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(groupList)} is null: {nameof(f)} - {f.FacultyID}/{f.FacultyName}");
            }

            return GroupList;
        }

        /// <summary>
        /// Getting list of department by faculty to KIP.
        /// </summary>
        /// <returns>List of department.</returns>
        /// <param name="facultyList">The KIP faculty list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Cathedra>> GetCathedrasAsync(HashSet<Faculty> facultyList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (facultyList == null)
            {
                return null;
            }

            CathedraList = new HashSet<Cathedra>();
            foreach (var f in facultyList)
            {
                var cathedraList = await ReceiveService.GetCathedrasAsync(f.FacultyID, logger);
                if (cathedraList == null)
                {
                    logger.LogWarning($"{nameof(cathedraList)} is null: {nameof(f)} - {f.FacultyID}/{f.FacultyName}");
                    continue;
                }

                var kipCathedraList = mapper.Map<List<Cathedra>>(cathedraList);
                if (kipCathedraList != null)
                {
                    foreach (var c in kipCathedraList)
                    {
                        if (c != null)
                        {
                            c.FacultyID = f.FacultyID;
                            CathedraList.Add(c);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(kipCathedraList)} is null: {nameof(f)} - {f.FacultyID}/{f.FacultyName}");
            }

            return CathedraList;
        }

        /// <summary>
        /// Getting list of building to KIP.
        /// </summary>
        /// <returns>List of building.</returns>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Building>> GetBuildingsAsync(ILogger<DbUpdateController> logger, IMapper mapper)
        {
            var buildingList = await ReceiveService.GetBuildingsAsync(logger);
            if (buildingList == null)
            {
                logger.LogWarning(nameof(buildingList) + " is null");
                return null;
            }

            var list = mapper.Map<List<Building>>(buildingList);
            BuildingList = new HashSet<Building>(list);
            return BuildingList;
        }

        /// <summary>
        /// Getting list of building audiences to KIP.
        /// </summary>
        /// <returns>List of audiences.</returns>
        /// <param name="buildingList">The KIP building list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Audience>> GetAudiencesAsync(HashSet<Building> buildingList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (buildingList == null)
            {
                return null;
            }

            AudienceList = new HashSet<Audience>();
            foreach (var b in buildingList)
            {
                var audienceList = await ReceiveService.GetAudiencesAsync(b.BuildingID, logger);
                if (audienceList == null)
                {
                    logger.LogWarning($"{nameof(audienceList)} is null: {nameof(b)} - {b.BuildingID}/{b.BuildingName}");
                    continue;
                }

                var kipAudienceList = mapper.Map<List<Audience>>(audienceList);
                if (kipAudienceList != null)
                {
                    foreach (var a in kipAudienceList)
                    {
                        if (a != null)
                        {
                            a.BuildingID = b.BuildingID;
                            AudienceList.Add(a);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(kipAudienceList)} is null: {nameof(b)} - {b.BuildingID}/{b.BuildingName}");
            }

            return AudienceList;
        }

        /// <summary>
        /// Getting list of teachers by department to KIP.
        /// </summary>
        /// <returns>List of teachers.</returns>
        /// <param name="cathedraList">The KIP department by faculty list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Prof>> GetProfsAsync(HashSet<Cathedra> cathedraList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (cathedraList == null)
            {
                return null;
            }

            ProfList = new HashSet<Prof>();
            foreach (var c in cathedraList)
            {
                var profList = await ReceiveService.GetProfsAsync(c.CathedraID, logger);
                if (profList == null)
                {
                    logger.LogWarning($"{nameof(profList)} is null: {nameof(c)} - {c.CathedraID}/{c.CathedraName}");
                    continue;
                }

                var kipProfList = mapper.Map<List<Prof>>(profList);
                if (kipProfList != null)
                {
                    foreach (var p in kipProfList)
                    {
                        if (p != null)
                        {
                            p.CathedraID = c.CathedraID;
                            ProfList.Add(p);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(kipProfList)} is null: {nameof(c)} - {c.CathedraID}/{c.CathedraName}");
            }

            return ProfList;
        }

        /// <summary>
        /// Getting schedule of group by faculty for an unpaired week to KIP.
        /// </summary>
        /// <returns>Schedule of group by faculty for an unpaired week.</returns>
        /// <param name="groupList">The KIP group by faculty list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<StudentSchedule>> GetScheduleByGroupAsync(HashSet<Group> groupList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (groupList == null)
            {
                return null;
            }

            GroupScheduleList = new HashSet<StudentSchedule>();
            foreach (var g in groupList)
            {
                var schedule = await ReceiveService.GetScheduleByGroupAsync(g.GroupID, logger);
                if (schedule == null)
                {
                    logger.LogWarning($"{nameof(schedule)} is null: {nameof(g)} - {g.GroupID}/{g.GroupName}");
                    continue;
                }

                var kipSchedule = mapper.Map<List<StudentSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule)
                    {
                        if (l != null)
                        {
                            l.GroupID = g.GroupID;
                            g.ScheduleIsPresent[(int)l.Day] = true;

                            GroupScheduleList.Add(l);
                        }
                    }
                }
                else
                {
                    logger.LogWarning($"{nameof(kipSchedule)} is null: {nameof(g)} - {g.GroupID}/{g.GroupName}");
                }
            }

            return GroupScheduleList;
        }

        /// <summary>
        /// Getting schedule of group by faculty for a paired week to KIP.
        /// </summary>
        /// <returns>Schedule of group by faculty for a paired week.</returns>
        /// <param name="groupList">The KIP group by faculty list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<StudentSchedule>> GetSchedule2ByGroupAsync(HashSet<Group> groupList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (groupList == null)
            {
                return null;
            }

            GroupSchedule2List = new HashSet<StudentSchedule>();
            foreach (var g in groupList)
            {
                var schedule = await ReceiveService.GetSchedule2ByGroupAsync(g.GroupID, logger);
                if (schedule == null)
                {
                    logger.LogWarning($"{nameof(schedule)} is null: {nameof(g)} - {g.GroupID}/{g.GroupName}");
                    continue;
                }

                var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                if (schedule1 != null)
                {
                    foreach (var l in schedule1)
                    {
                        if (l != null)
                        {
                            l.GroupID = g.GroupID;
                            g.ScheduleIsPresent[(int)l.Day] = true;

                            GroupSchedule2List.Add(l);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(schedule1)} is null: {nameof(g)} - {g.GroupID}/{g.GroupName}");
            }

            return GroupSchedule2List;
        }

        /// <summary>
        /// Getting schedule of teachers for an unpaired week to KIP.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="profList">The KIP teacher by department list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<ProfSchedule>> GetScheduleByProfAsync(HashSet<Prof> profList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (profList == null)
            {
                return null;
            }

            ProfScheduleList = new HashSet<ProfSchedule>();
            foreach (var p in profList)
            {
                var schedule = await ReceiveService.GetScheduleByProfAsync(p.ProfID, logger);
                if (schedule == null)
                {
                    logger.LogWarning($"{nameof(schedule)} is null: {nameof(p)} - {p.ProfID}/{p.ProfSurname}");
                    continue;
                }

                var kipSchedule = mapper.Map<List<ProfSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule)
                    {
                        if (l != null)
                        {
                            l.ProfID = p.ProfID;
                            p.ScheduleIsPresent[(int)l.Day] = true;

                            ProfScheduleList.Add(l);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(kipSchedule)} is null: {nameof(p)} - {p.ProfID}/{p.ProfSurname}");
            }

            return ProfScheduleList;
        }

        /// <summary>
        /// Getting schedule of teachers for a paired week to KIP.
        /// </summary>
        /// <returns>Schedule of teachers for a paired week.</returns>
        /// <param name="profList">The KIP teacher by department list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<ProfSchedule>> GetSchedule2ByProfAsync(HashSet<Prof> profList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (profList == null)
            {
                return null;
            }

            ProfSchedule2List = new HashSet<ProfSchedule>();
            foreach (var p in profList)
            {
                var schedule = await ReceiveService.GetSchedule2ByProfAsync(p.ProfID, logger);

                if (schedule == null)
                {
                    logger.LogWarning($"{nameof(schedule)} is null: {nameof(p)} - {p.ProfID}/{p.ProfSurname}");
                    continue;
                }

                var kipSchedule = mapper.Map<List<ProfSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule)
                    {
                        if (l != null)
                        {
                            l.ProfID = p.ProfID;
                            p.ScheduleIsPresent[(int)l.Day] = true;

                            ProfSchedule2List.Add(l);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(kipSchedule)} is null: {nameof(p)} - {p.ProfID}/{p.ProfSurname}");
            }

            return ProfSchedule2List;
        }

        /// <summary>
        /// Getting schedule of audience for an unpaired week to KIP.
        /// </summary>
        /// <returns>Schedule of audience for an unpaired week.</returns>
        /// <param name="audienceList">The KIP teacher by department list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<AudienceSchedule>> GetScheduleByAudienceAsync(HashSet<Audience> audienceList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (audienceList == null)
            {
                return null;
            }

            AudienceScheduleList = new HashSet<AudienceSchedule>();
            foreach (var a in audienceList)
            {
                var schedule = await ReceiveService.GetScheduleByAudienceAsync(a.AudienceID, logger);
                if (schedule == null)
                {
                    logger.LogWarning($"{nameof(schedule)} is null: {nameof(a)} - {a.AudienceID}/{a.AudienceName}");
                    continue;
                }

                var kipSchedule = mapper.Map<List<AudienceSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule)
                    {
                        if (l != null)
                        {
                            l.BuildingID = a.BuildingID;
                            l.AudienceID = a.AudienceID;
                            a.ScheduleIsPresent[(int)l.Day] = true;

                            AudienceScheduleList.Add(l);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(kipSchedule)} is null: {nameof(a)} - {a.AudienceID}/{a.AudienceName}");
            }

            return AudienceScheduleList;
        }

        /// <summary>
        /// Getting schedule of audience for a paired week to KIP.
        /// </summary>
        /// <returns>Schedule of audience for a paired week.</returns>
        /// <param name="audienceList">The KIP teacher by department list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<AudienceSchedule>> GetSchedule2ByAudienceAsync(HashSet<Audience> audienceList, ILogger<DbUpdateController> logger, IMapper mapper)
        {
            if (audienceList == null)
            {
                return null;
            }

            AudienceSchedule2List = new HashSet<AudienceSchedule>();
            foreach (var a in audienceList)
            {
                var schedule = await ReceiveService.GetSchedule2ByAudienceAsync(a.AudienceID, logger);
                if (schedule == null)
                {
                    logger.LogWarning($"{nameof(schedule)} is null: {nameof(a)} - {a.AudienceID}/{a.AudienceName}");
                    continue;
                }

                var kipSchedule = mapper.Map<List<AudienceSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule)
                    {
                        if (l != null)
                        {
                            l.BuildingID = a.BuildingID;
                            l.AudienceID = a.AudienceID;
                            a.ScheduleIsPresent[(int)l.Day] = true;

                            AudienceSchedule2List.Add(l);
                        }
                    }

                    continue;
                }

                logger.LogWarning($"{nameof(kipSchedule)} is null: {nameof(a)} - {a.AudienceID}/{a.AudienceName}");
            }

            return AudienceSchedule2List;
        }
    }
}
