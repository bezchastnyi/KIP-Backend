using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.V1.Controllers;
using Microsoft.Extensions.Logging;

namespace KIP_server_NoAuth.Services
{
    /// <summary>
    /// Mapping data to the KIP database.
    /// </summary>
    public static class MapService
    {
        private const string NullListOfObjectsWarningLog = "[method: {0}] [operation: {1}] List of {2} is null";
        private const string NullObjectWarningLog = "[method: {0}] [operation: {1}] {2} is null ({3} - ({4}) {5})";

        /// <summary>
        /// Gets or sets the list of faculties.
        /// </summary>
        public static HashSet<Faculty> FacultyList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of groups.
        /// </summary>
        public static HashSet<Group> GroupList { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of departments.
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
        public static async Task<HashSet<Faculty>> GetFacultiesAsync(ILogger logger, IMapper mapper)
        {
            var facultyList = await GetService.GetFacultiesAsync(logger);
            if (facultyList == null)
            {
                logger.LogWarning(string.Format(NullListOfObjectsWarningLog, nameof(GetFacultiesAsync), "get", nameof(Faculty)));
                return null;
            }

            var list = mapper.Map<List<Faculty>>(facultyList);
            if (list == null)
            {
                logger.LogWarning(string.Format(NullListOfObjectsWarningLog, nameof(GetFacultiesAsync), "map", nameof(Faculty)));
                return null;
            }

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
        public static async Task<HashSet<Group>> GetGroupsAsync(HashSet<Faculty> facultyList, ILogger logger, IMapper mapper)
        {
            if (facultyList == null)
            {
                return null;
            }

            GroupList = new HashSet<Group>();
            var stringBuilder = new StringBuilder();

            foreach (var f in facultyList)
            {
                var groupList = await GetService.GetGroupsAsync(f.FacultyId, logger);
                if (groupList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetGroupsAsync), "get", nameof(Group), nameof(Faculty), f.FacultyId, f.FacultyName));
                    continue;
                }

                var kipGroupList = mapper.Map<List<Group>>(groupList);
                if (kipGroupList != null)
                {
                    foreach (var g in kipGroupList.Where(g => g != null))
                    {
                        g.FacultyId = f.FacultyId;
                        GroupList.Add(g);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetGroupsAsync), "map", nameof(Group), nameof(Faculty), f.FacultyId, f.FacultyName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<Cathedra>> GetCathedrasAsync(HashSet<Faculty> facultyList, ILogger logger, IMapper mapper)
        {
            if (facultyList == null)
            {
                return null;
            }

            CathedraList = new HashSet<Cathedra>();
            var stringBuilder = new StringBuilder();

            foreach (var f in facultyList)
            {
                var cathedraList = await GetService.GetCathedrasAsync(f.FacultyId, logger);
                if (cathedraList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetCathedrasAsync), "get", nameof(Group), nameof(Faculty), f.FacultyId, f.FacultyName));
                    continue;
                }

                var kipCathedraList = mapper.Map<List<Cathedra>>(cathedraList);
                if (kipCathedraList != null)
                {
                    foreach (var c in kipCathedraList.Where(c => c != null))
                    {
                        c.FacultyId = f.FacultyId;
                        CathedraList.Add(c);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetCathedrasAsync), nameof(Group), "map", nameof(Faculty), f.FacultyId, f.FacultyName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return CathedraList;
        }

        /// <summary>
        /// Getting list of building to KIP.
        /// </summary>
        /// <returns>List of building.</returns>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Building>> GetBuildingsAsync(ILogger logger, IMapper mapper)
        {
            var buildingList = await GetService.GetBuildingsAsync(logger);
            if (buildingList == null)
            {
                logger.LogWarning(string.Format(NullListOfObjectsWarningLog, nameof(GetBuildingsAsync), "get", nameof(Building)));
                return null;
            }

            var list = mapper.Map<List<Building>>(buildingList);
            if (list == null)
            {
                logger.LogWarning(string.Format(NullListOfObjectsWarningLog, nameof(GetBuildingsAsync), "map", nameof(Building)));
                return null;
            }

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
        public static async Task<HashSet<Audience>> GetAudiencesAsync(HashSet<Building> buildingList, ILogger logger, IMapper mapper)
        {
            if (buildingList == null)
            {
                return null;
            }

            AudienceList = new HashSet<Audience>();
            var stringBuilder = new StringBuilder();

            foreach (var b in buildingList)
            {
                var audienceList = await GetService.GetAudiencesAsync(b.BuildingId, logger);
                if (audienceList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetAudiencesAsync), "get", nameof(Audience), nameof(Building), b.BuildingId, b.BuildingName));
                    continue;
                }

                var kipAudienceList = mapper.Map<List<Audience>>(audienceList);
                if (kipAudienceList != null)
                {
                    foreach (var a in kipAudienceList.Where(a => a != null))
                    {
                        a.BuildingId = b.BuildingId;
                        AudienceList.Add(a);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetAudiencesAsync), "map", nameof(Audience), nameof(Building), b.BuildingId, b.BuildingName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<Prof>> GetProfsAsync(HashSet<Cathedra> cathedraList, ILogger logger, IMapper mapper)
        {
            // TODO check duplicates
            if (cathedraList == null)
            {
                return null;
            }

            ProfList = new HashSet<Prof>();
            var stringBuilder = new StringBuilder();

            foreach (var c in cathedraList)
            {
                var profList = await GetService.GetProfsAsync(c.CathedraId, logger);
                if (profList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetProfsAsync), "get", nameof(Prof), nameof(Cathedra), c.CathedraId, c.CathedraName));
                    continue;
                }

                var kipProfList = mapper.Map<List<Prof>>(profList);
                if (kipProfList != null)
                {
                    foreach (var p in kipProfList.Where(p => p != null))
                    {
                        p.CathedraId = c.CathedraId;
                        ProfList.Add(p);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetProfsAsync), "map", nameof(Prof), nameof(Cathedra), c.CathedraId, c.CathedraName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<StudentSchedule>> GetScheduleByGroupAsync(HashSet<Group> groupList, ILogger logger, IMapper mapper)
        {
            if (groupList == null)
            {
                return null;
            }

            GroupScheduleList = new HashSet<StudentSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var g in groupList)
            {
                var schedule = await GetService.GetScheduleByGroupAsync(g.GroupId, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByGroupAsync), "get", nameof(StudentSchedule) + " [paired]", nameof(Group), g.GroupId, g.GroupName));
                    continue;
                }

                var kipSchedule = mapper.Map<List<StudentSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(l => l != null))
                    {
                        l.GroupId = g.GroupId;
                        g.ScheduleIsPresent[(int)l.Day] = true;

                        GroupScheduleList.Add(l);
                    }
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByGroupAsync), "map", nameof(StudentSchedule) + " [paired]", nameof(Group), g.GroupId, g.GroupName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<StudentSchedule>> GetSchedule2ByGroupAsync(HashSet<Group> groupList, ILogger logger, IMapper mapper)
        {
            if (groupList == null)
            {
                return null;
            }

            GroupSchedule2List = new HashSet<StudentSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var g in groupList)
            {
                var schedule = await GetService.GetSchedule2ByGroupAsync(g.GroupId, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetSchedule2ByGroupAsync), "get", nameof(StudentSchedule) + " [unpaired]", nameof(Group), g.GroupId, g.GroupName));
                    continue;
                }

                var schedule1 = mapper.Map<List<StudentSchedule>>(schedule);
                if (schedule1 != null)
                {
                    foreach (var l in schedule1.Where(l => l != null))
                    {
                        l.GroupId = g.GroupId;
                        g.ScheduleIsPresent[(int)l.Day] = true;

                        GroupSchedule2List.Add(l);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetSchedule2ByGroupAsync), "map", nameof(StudentSchedule) + " [unpaired]", nameof(Group), g.GroupId, g.GroupName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<ProfSchedule>> GetScheduleByProfAsync(HashSet<Prof> profList, ILogger logger, IMapper mapper)
        {
            if (profList == null)
            {
                return null;
            }

            ProfScheduleList = new HashSet<ProfSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var p in profList)
            {
                var schedule = await GetService.GetScheduleByProfAsync(p.ProfId, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetSchedule2ByProfAsync), "get", nameof(ProfSchedule) + " [paired]", nameof(Prof), p.ProfId, p.ProfSurname));
                    continue;
                }

                var kipSchedule = mapper.Map<List<ProfSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(l => l != null))
                    {
                        l.ProfId = p.ProfId;
                        p.ScheduleIsPresent[(int)l.Day] = true;

                        ProfScheduleList.Add(l);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByProfAsync), "map", nameof(ProfSchedule) + " [paired]", nameof(Prof), p.ProfId, p.ProfSurname));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<ProfSchedule>> GetSchedule2ByProfAsync(HashSet<Prof> profList, ILogger logger, IMapper mapper)
        {
            if (profList == null)
            {
                return null;
            }

            ProfSchedule2List = new HashSet<ProfSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var p in profList)
            {
                var schedule = await GetService.GetSchedule2ByProfAsync(p.ProfId, logger);

                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetSchedule2ByProfAsync), "get", nameof(ProfSchedule) + " [unpaired]", nameof(Prof), p.ProfId, p.ProfSurname));
                    continue;
                }

                var kipSchedule = mapper.Map<List<ProfSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(l => l != null))
                    {
                        l.ProfId = p.ProfId;
                        p.ScheduleIsPresent[(int)l.Day] = true;

                        ProfSchedule2List.Add(l);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetSchedule2ByProfAsync), "map", nameof(ProfSchedule) + " [unpaired]", nameof(Prof), p.ProfId, p.ProfSurname));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<AudienceSchedule>> GetScheduleByAudienceAsync(HashSet<Audience> audienceList, ILogger logger, IMapper mapper)
        {
            if (audienceList == null)
            {
                return null;
            }

            AudienceScheduleList = new HashSet<AudienceSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var a in audienceList)
            {
                var schedule = await GetService.GetScheduleByAudienceAsync(a.AudienceId, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByAudienceAsync), "get", nameof(AudienceSchedule) + " [paired]", nameof(Audience), a.AudienceId, a.AudienceName));
                    continue;
                }

                var kipSchedule = mapper.Map<List<AudienceSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(l => l != null))
                    {
                        l.BuildingId = a.BuildingId;
                        l.AudienceId = a.AudienceId;
                        a.ScheduleIsPresent[(int)l.Day] = true;

                        AudienceScheduleList.Add(l);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByAudienceAsync), "map", nameof(AudienceSchedule) + " [paired]", nameof(Audience), a.AudienceId, a.AudienceName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
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
        public static async Task<HashSet<AudienceSchedule>> GetSchedule2ByAudienceAsync(HashSet<Audience> audienceList, ILogger logger, IMapper mapper)
        {
            if (audienceList == null)
            {
                return null;
            }

            AudienceSchedule2List = new HashSet<AudienceSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var a in audienceList)
            {
                var schedule = await GetService.GetSchedule2ByAudienceAsync(a.AudienceId, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetSchedule2ByAudienceAsync), "get", nameof(AudienceSchedule) + " [unpaired]", nameof(Audience), a.AudienceId, a.AudienceName));
                    continue;
                }

                var kipSchedule = mapper.Map<List<AudienceSchedule>>(schedule);
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(l => l != null))
                    {
                        l.BuildingId = a.BuildingId;
                        l.AudienceId = a.AudienceId;
                        a.ScheduleIsPresent[(int)l.Day] = true;

                        AudienceSchedule2List.Add(l);
                    }

                    continue;
                }

                stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetSchedule2ByAudienceAsync), "map", nameof(AudienceSchedule) + " [unpaired]", nameof(Audience), a.AudienceId, a.AudienceName));
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return AudienceSchedule2List;
        }
    }
}
