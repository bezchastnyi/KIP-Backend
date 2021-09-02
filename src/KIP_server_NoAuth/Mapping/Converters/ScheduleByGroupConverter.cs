using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.Services;
using KIP_server_NoAuth.V1.Controllers;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP schedule by group model from the KhPI schedule by group.
    /// </summary>
    public class ScheduleByGroupConverter : ITypeConverter<ScheduleKhPI, List<StudentSchedule>> // TODO check how it works
    {
        /// <summary>
        /// Convert model of schedule by group from KhPI to KIP.
        /// </summary>
        /// <returns>Object of schedule by group of model schedule by group KIP.</returns>
        /// <param name="source">Model of schedule by group KhPI.</param>
        /// <param name="destination">Model of schedule by group KIP.</param>
        /// <param name="context">The context. </param>
        public List<StudentSchedule> Convert(ScheduleKhPI source, List<StudentSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new List<StudentSchedule>();
            var week = DbUpdateController.Week;

            var (subjectListMonday, subjectListTuesday, subjectListWednesday, subjectListThursday, subjectListFriday) = ScheduleStuff.GetSubjectLists(source);
            var (audienceListMonday, audienceListTuesday, audienceListWednesday, audienceListThursday, audienceListFriday) = ScheduleStuff.GetAudienceLists(source);
            var (typeListMonday, typeListTuesday, typeListWednesday, typeListThursday, typeListFriday) = ScheduleStuff.GetTypeLists(source);
            var (profListMonday, profListTuesday, profListWednesday, profListThursday, profListFriday) = ScheduleStuff.GetProfLists(source);

            if (subjectListMonday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ProfIdentificationAndScheduleImprovement(Day.Monday, week, profListMonday, ref subjectListMonday, ref profListDestination);
                ScheduleStuff.AudienceIdentification(audienceListMonday, ref buildingListDestination, ref audienceListDestination);
                RegisterGroupScheduleLessons(ref obj, Day.Monday, week, subjectListMonday, typeListMonday, profListDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListTuesday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ProfIdentificationAndScheduleImprovement(Day.Tuesday, week, profListTuesday, ref subjectListTuesday, ref profListDestination);
                ScheduleStuff.AudienceIdentification(audienceListTuesday, ref buildingListDestination, ref audienceListDestination);
                RegisterGroupScheduleLessons(ref obj, Day.Tuesday, week, subjectListTuesday, typeListTuesday, profListDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListWednesday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ProfIdentificationAndScheduleImprovement(Day.Wednesday, week, profListWednesday, ref subjectListWednesday, ref profListDestination);
                ScheduleStuff.AudienceIdentification(audienceListWednesday, ref buildingListDestination, ref audienceListDestination);
                RegisterGroupScheduleLessons(ref obj, Day.Wednesday, week, subjectListWednesday, typeListWednesday, profListDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListThursday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ProfIdentificationAndScheduleImprovement(Day.Thursday, week, profListThursday, ref subjectListThursday, ref profListDestination);
                ScheduleStuff.AudienceIdentification(audienceListThursday, ref buildingListDestination, ref audienceListDestination);
                RegisterGroupScheduleLessons(ref obj, Day.Thursday, week, subjectListThursday, typeListThursday, profListDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListFriday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ProfIdentificationAndScheduleImprovement(Day.Friday, week, profListFriday, ref subjectListFriday, ref profListDestination);
                ScheduleStuff.AudienceIdentification(audienceListFriday, ref buildingListDestination, ref audienceListDestination);
                RegisterGroupScheduleLessons(ref obj, Day.Friday, week, subjectListFriday, typeListFriday, profListDestination, buildingListDestination, audienceListDestination);
            }

            return obj.Count == 0 ? null : obj;
        }

        private static void RegisterGroupScheduleLessons(
            ref List<StudentSchedule> obj,
            Day day,
            Week week,
            List<string> subjectList,
            List<string> typeList,
            List<(int? id, string name)> profListDestination,
            List<int?> buildingListDestination,
            List<(int? id, string name)> audienceListDestination)
        {
            for (var i = 0; i < subjectList.Count; i++)
            {
                if (subjectList[i] != string.Empty)
                {
                    obj.Add(new StudentSchedule
                    {
                        Day = day,
                        Week = week,
                        SubjectName = subjectList[i],
                        Type = typeList[i],
                        Number = i,
                        ProfId = profListDestination[i].id,
                        ProfName = profListDestination[i].name,
                        AudienceId = audienceListDestination[i].id,
                        AudienceName = audienceListDestination[i].name,
                        BuildingId = buildingListDestination[i],
                    });
                }
            }
        }

        private static void ProfIdentificationAndScheduleImprovement(Day day, Week week, List<string> profList, ref List<string> subjectList, ref List<(int? id, string name)> profListDestination)
        {
            for (var i = 0; i < profList.Count; i++)
            {
                if (string.IsNullOrEmpty(profList[i]) || string.IsNullOrWhiteSpace(profList[i]))
                {
                    continue;
                }

                if (MapService.ProfList == null)
                {
                    profListDestination[i] = (null, profList[i]);
                    continue;
                }

                var prof = MapService.ProfList.FirstOrDefault(p => profList[i].Contains(p.ProfSurname));
                if (prof == null)
                {
                    continue;
                }

                profListDestination[i] = (prof.ProfId, prof.ProfSurname);

                var list = week == Week.Paired ?
                    MapService.ProfScheduleList.Where(l => l.ProfId == prof.ProfId).ToHashSet() :
                    MapService.ProfSchedule2List.Where(l => l.ProfId == prof.ProfId).ToHashSet();

                var lesson = list.FirstOrDefault(l => l.Number == i && l.Day == day);
                if (lesson != null)
                {
                    subjectList[i] = lesson.SubjectName;
                }
            }
        }
    }
}
