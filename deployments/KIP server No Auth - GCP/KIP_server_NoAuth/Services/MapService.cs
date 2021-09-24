using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.Mapping;
using KIP_server_NoAuth.Models.Helpers;
using KIP_server_NoAuth.Models.KhPI;
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
        /// Getting list of faculty to KIP.
        /// </summary>
        /// <returns>List of faculty.</returns>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<HashSet<Faculty>> GetFacultiesAsync(ILogger logger, IMapper mapper)
        {
            var facultyList = await GetService.GetCollectionOfDataAsync<FacultyKhPI>(logger);
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

            return list.Where(o => o != null).ToHashSet();
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

            var groups = new HashSet<Group>();
            var stringBuilder = new StringBuilder();

            foreach (var f in facultyList)
            {
                var groupList = await GetService.GetCollectionOfDataAsync<GroupKhPI>(logger, f.FacultyId);
                if (groupList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetGroupsAsync), "get", nameof(Group), nameof(Faculty), f.FacultyId, f.FacultyName));
                    continue;
                }

                var kipGroupList = mapper.Map<List<Group>>(groupList);
                if (kipGroupList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetGroupsAsync), "map", nameof(Group), nameof(Faculty), f.FacultyId, f.FacultyName));
                    continue;
                }

                foreach (var g in kipGroupList.Where(o => o != null))
                {
                    g.FacultyId = f.FacultyId;
                    groups.Add(g);
                }
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return groups;
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

            var cathedras = new HashSet<Cathedra>();
            var stringBuilder = new StringBuilder();

            foreach (var f in facultyList)
            {
                var cathedraList = await GetService.GetCollectionOfDataAsync<CathedraKhPI>(logger, f.FacultyId);
                if (cathedraList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetCathedrasAsync), "get", nameof(Group), nameof(Faculty), f.FacultyId, f.FacultyName));
                    continue;
                }

                var kipCathedraList = mapper.Map<List<Cathedra>>(cathedraList);
                if (kipCathedraList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetCathedrasAsync), nameof(Group), "map", nameof(Faculty), f.FacultyId, f.FacultyName));
                    continue;
                }

                foreach (var c in kipCathedraList.Where(o => o != null))
                {
                    c.FacultyId = f.FacultyId;
                    cathedras.Add(c);
                }
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return cathedras;
        }

        /// <summary>
        /// Getting list of building to KIP.
        /// </summary>
        /// <returns>List of building.</returns>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public static async Task<HashSet<Building>> GetBuildingsAsync(ILogger logger, IMapper mapper)
        {
            var buildingList = await GetService.GetCollectionOfDataAsync<BuildingKhPI>(logger);
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

            return list.Where(o => o != null).ToHashSet();
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

            var audiences = new HashSet<Audience>();
            var stringBuilder = new StringBuilder();

            foreach (var b in buildingList)
            {
                var audienceList = await GetService.GetCollectionOfDataAsync<AudienceKhPI>(logger, b.BuildingId);
                if (audienceList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetAudiencesAsync), "get", nameof(Audience), nameof(Building), b.BuildingId, b.BuildingName));
                    continue;
                }

                var kipAudienceList = mapper.Map<List<Audience>>(audienceList);
                if (kipAudienceList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetAudiencesAsync), "map", nameof(Audience), nameof(Building), b.BuildingId, b.BuildingName));
                    continue;
                }

                foreach (var a in kipAudienceList.Where(o => o != null))
                {
                    a.BuildingId = b.BuildingId;
                    audiences.Add(a);
                }
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return audiences;
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

            var profs = new HashSet<Prof>();
            var stringBuilder = new StringBuilder();

            foreach (var c in cathedraList)
            {
                var profList = await GetService.GetCollectionOfDataAsync<ProfKhPI>(logger, c.CathedraId);
                if (profList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetProfsAsync), "get", nameof(Prof), nameof(Cathedra), c.CathedraId, c.CathedraName));
                    continue;
                }

                var kipProfList = mapper.Map<List<Prof>>(profList);
                if (kipProfList == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetProfsAsync), "map", nameof(Prof), nameof(Cathedra), c.CathedraId, c.CathedraName));
                    continue;
                }

                foreach (var p in kipProfList.Where(o => o != null))
                {
                    p.CathedraId = c.CathedraId;
                    profs.Add(p);
                }
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return profs;
        }

        /// <summary>
        /// Getting schedule of group by faculty for an unpaired week to KIP.
        /// </summary>
        /// <returns>Schedule of group by faculty for an unpaired week.</returns>
        /// <param name="groupList">The KIP group by faculty list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<(HashSet<StudentSchedule> schedule0, HashSet<StudentSchedule> schedule1)>
            GetScheduleByGroupAsync(HashSet<Group> groupList, ILogger logger, IMapper mapper)
        {
            if (groupList == null)
            {
                return (null, null);
            }

            var groupSchedule = new HashSet<StudentSchedule>();
            var groupSchedule2 = new HashSet<StudentSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var g in groupList)
            {
                // UnPaired
                var schedule = await GetService.GetScheduleAsync(g.GroupId, ScheduleType.GroupSchedule, Week.UnPaired, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByGroupAsync), "get", nameof(StudentSchedule) + " [paired]", nameof(Group), g.GroupId, g.GroupName));
                    continue;
                }

                var kipSchedule = mapper.Map<List<StudentSchedule>>(schedule, opts => opts.SetWeekValue(Week.UnPaired));
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(o => o != null))
                    {
                        l.GroupId = g.GroupId;
                        g.ScheduleIsPresent[(int)l.Day] = true;

                        groupSchedule.Add(l);
                    }
                }
                else
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByGroupAsync), "map", nameof(StudentSchedule) + " [paired]", nameof(Group), g.GroupId, g.GroupName));
                }

                // Paired
                schedule = await GetService.GetScheduleAsync(g.GroupId, ScheduleType.GroupSchedule, Week.Paired, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByGroupAsync), "get", nameof(StudentSchedule) + " [unpaired]", nameof(Group), g.GroupId, g.GroupName));
                    continue;
                }

                kipSchedule = mapper.Map<List<StudentSchedule>>(schedule, opts => opts.SetWeekValue(Week.Paired));
                if (kipSchedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByGroupAsync), "map", nameof(StudentSchedule) + " [unpaired]", nameof(Group), g.GroupId, g.GroupName));
                    continue;
                }

                foreach (var l in kipSchedule.Where(o => o != null))
                {
                    l.GroupId = g.GroupId;
                    g.ScheduleIsPresent[(int)l.Day] = true;

                    groupSchedule2.Add(l);
                }
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return (groupSchedule, groupSchedule2);
        }

        /// <summary>
        /// Getting schedule of teachers for an unpaired week to KIP.
        /// </summary>
        /// <returns>Schedule of teachers for an unpaired week.</returns>
        /// <param name="profList">The KIP teacher by department list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper. </param>
        public static async Task<(HashSet<ProfSchedule> ProfScheduleList, HashSet<ProfSchedule> ProfSchedule2List)>
            GetScheduleByProfAsync(HashSet<Prof> profList, ILogger logger, IMapper mapper)
        {
            if (profList == null)
            {
                return (null, null);
            }

            var profSchedule = new HashSet<ProfSchedule>();
            var profSchedule2 = new HashSet<ProfSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var p in profList)
            {
                // UnPaired
                var schedule = await GetService.GetScheduleAsync(p.ProfId, ScheduleType.ProfSchedule, Week.UnPaired, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByProfAsync), "get", nameof(ProfSchedule) + " [paired]", nameof(Prof), p.ProfId, p.ProfSurname));
                    continue;
                }

                var kipSchedule = mapper.Map<List<ProfSchedule>>(schedule, opts => opts.SetWeekValue(Week.UnPaired));
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(o => o != null))
                    {
                        l.ProfId = p.ProfId;
                        p.ScheduleIsPresent[(int)l.Day] = true;

                        profSchedule.Add(l);
                    }
                }
                else
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByProfAsync), "map", nameof(ProfSchedule) + " [paired]", nameof(Prof), p.ProfId, p.ProfSurname));
                }

                // Paired
                schedule = await GetService.GetScheduleAsync(p.ProfId, ScheduleType.ProfSchedule, Week.Paired, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByProfAsync), "get", nameof(ProfSchedule) + " [unpaired]", nameof(Prof), p.ProfId, p.ProfSurname));
                    continue;
                }

                kipSchedule = mapper.Map<List<ProfSchedule>>(schedule, opts => opts.SetWeekValue(Week.Paired));
                if (kipSchedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByProfAsync), "map", nameof(ProfSchedule) + " [unpaired]", nameof(Prof), p.ProfId, p.ProfSurname));
                    continue;
                }

                foreach (var l in kipSchedule.Where(o => o != null))
                {
                    l.ProfId = p.ProfId;
                    p.ScheduleIsPresent[(int)l.Day] = true;

                    profSchedule2.Add(l);
                }
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return (profSchedule, profSchedule2);
        }

        /// <summary>
        /// Getting schedule of audience for an unpaired week to KIP.
        /// </summary>
        /// <returns>Schedule of audience for an unpaired week.</returns>
        /// <param name="audienceList">The KIP teacher by department list.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public static async Task<(HashSet<AudienceSchedule> AudienceScheduleList, HashSet<AudienceSchedule> AudienceSchedule2List)>
            GetScheduleByAudienceAsync(HashSet<Audience> audienceList, ILogger logger, IMapper mapper)
        {
            if (audienceList == null)
            {
                return (null, null);
            }

            var audienceSchedule = new HashSet<AudienceSchedule>();
            var audienceSchedule2 = new HashSet<AudienceSchedule>();
            var stringBuilder = new StringBuilder();

            foreach (var a in audienceList)
            {
                // UnPaired
                var schedule = await GetService.GetScheduleAsync(a.AudienceId, ScheduleType.AudienceSchedule, Week.UnPaired, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByAudienceAsync), "get", nameof(AudienceSchedule) + " [paired]", nameof(Audience), a.AudienceId, a.AudienceName));
                    continue;
                }

                var kipSchedule = mapper.Map<List<AudienceSchedule>>(schedule, opts => opts.SetWeekValue(Week.UnPaired));
                if (kipSchedule != null)
                {
                    foreach (var l in kipSchedule.Where(o => o != null))
                    {
                        l.BuildingId = a.BuildingId;
                        l.AudienceId = a.AudienceId;
                        a.ScheduleIsPresent[(int)l.Day] = true;

                        audienceSchedule.Add(l);
                    }
                }
                else
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByAudienceAsync), "map", nameof(AudienceSchedule) + " [paired]", nameof(Audience), a.AudienceId, a.AudienceName));
                }

                // Paired
                schedule = await GetService.GetScheduleAsync(a.AudienceId, ScheduleType.AudienceSchedule, Week.Paired, logger);
                if (schedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByAudienceAsync), "get", nameof(AudienceSchedule) + " [unpaired]", nameof(Audience), a.AudienceId, a.AudienceName));
                    continue;
                }

                kipSchedule = mapper.Map<List<AudienceSchedule>>(schedule, opts => opts.SetWeekValue(Week.Paired));
                if (kipSchedule == null)
                {
                    stringBuilder.AppendLine(string.Format(NullObjectWarningLog, nameof(GetScheduleByAudienceAsync), "map", nameof(AudienceSchedule) + " [unpaired]", nameof(Audience), a.AudienceId, a.AudienceName));
                    continue;
                }

                foreach (var l in kipSchedule.Where(o => o != null))
                {
                    l.BuildingId = a.BuildingId;
                    l.AudienceId = a.AudienceId;
                    a.ScheduleIsPresent[(int)l.Day] = true;

                    audienceSchedule2.Add(l);
                }
            }

            if (stringBuilder.Length != 0)
            {
                // logger.LogWarning(stringBuilder.ToString());
            }

            return (audienceSchedule, audienceSchedule2);
        }
    }
}
