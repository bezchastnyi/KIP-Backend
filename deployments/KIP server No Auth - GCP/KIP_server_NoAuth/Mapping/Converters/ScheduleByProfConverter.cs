using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.V1.Controllers;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP schedule by teachers model from the KhPI schedule by teachers.
    /// </summary>
    public class ScheduleByProfConverter : ITypeConverter<ScheduleKhPI, List<ProfSchedule>> // TODO check how it works
    {
        /// <summary>
        /// Convert model of schedule by teachers from KHPI to KIP.
        /// </summary>
        /// <returns>Object of schedule by teachers of model schedule by teachers KIP.</returns>
        /// <param name="source">Model of schedule by teachers KHPI.</param>
        /// <param name = "destination">Model of schedule by teachers KIP.</param>
        /// <param name= "context">The context. </param>
        public List<ProfSchedule> Convert(ScheduleKhPI source, List<ProfSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new List<ProfSchedule>();
            var week = DbUpdateController.Week;

            var (subjectListMonday, subjectListTuesday, subjectListWednesday, subjectListThursday, subjectListFriday) = ScheduleStuff.GetSubjectLists(source);
            var (audienceListMonday, audienceListTuesday, audienceListWednesday, audienceListThursday, audienceListFriday) = ScheduleStuff.GetAudienceLists(source);
            var (typeListMonday, typeListTuesday, typeListWednesday, typeListThursday, typeListFriday) = ScheduleStuff.GetTypeLists(source);
            var (groupListMonday, groupListTuesday, groupListWednesday, groupListThursday, groupListFriday) = ScheduleStuff.GetProfLists(source);

            if (subjectListMonday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListMonday, ref groupListDestination, ref groupListNamesDestination);
                ScheduleStuff.AudienceIdentification(audienceListMonday, ref buildingListDestination, ref audienceListDestination);
                RegisterProfScheduleLessons(ref obj, Day.Monday, week, audienceListMonday, typeListMonday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListTuesday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListTuesday, ref groupListDestination, ref groupListNamesDestination);
                ScheduleStuff.AudienceIdentification(audienceListTuesday, ref buildingListDestination, ref audienceListDestination);
                RegisterProfScheduleLessons(ref obj, Day.Tuesday, week, audienceListTuesday, typeListTuesday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListWednesday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListWednesday, ref groupListDestination, ref groupListNamesDestination);
                ScheduleStuff.AudienceIdentification(audienceListWednesday, ref buildingListDestination, ref audienceListDestination);
                RegisterProfScheduleLessons(ref obj, Day.Wednesday, week, audienceListWednesday, typeListWednesday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListThursday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListThursday, ref groupListDestination, ref groupListNamesDestination);
                ScheduleStuff.AudienceIdentification(audienceListThursday, ref buildingListDestination, ref audienceListDestination);
                RegisterProfScheduleLessons(ref obj, Day.Thursday, week, audienceListThursday, typeListThursday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListFriday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListFriday, ref groupListDestination, ref groupListNamesDestination);
                ScheduleStuff.AudienceIdentification(audienceListFriday, ref buildingListDestination, ref audienceListDestination);
                RegisterProfScheduleLessons(ref obj, Day.Friday, week, audienceListFriday, typeListFriday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            return obj.Count == 0 ? null : obj;
        }

        private static void RegisterProfScheduleLessons(
            ref List<ProfSchedule> obj,
            Day day,
            Week week,
            List<string> subjectList,
            List<string> typeList,
            List<List<int?>> groupListDestination,
            List<string> groupListNamesDestination,
            List<int?> buildingListDestination,
            List<(int? id, string name)> audienceListDestination)
        {
            for (var i = 0; i < subjectList.Count; i++)
            {
                if (subjectList[i] != string.Empty)
                {
                    obj.Add(new ProfSchedule
                    {
                        Day = day,
                        Week = week,
                        SubjectName = subjectList[i],
                        Type = typeList[i],
                        Number = i,
                        GroupId = groupListDestination[i],
                        GroupNames = groupListNamesDestination[i],
                        BuildingId = buildingListDestination[i],
                        AudienceId = audienceListDestination[i].id,
                        AudienceName = audienceListDestination[i].name,
                    });
                }
            }
        }
    }
}
