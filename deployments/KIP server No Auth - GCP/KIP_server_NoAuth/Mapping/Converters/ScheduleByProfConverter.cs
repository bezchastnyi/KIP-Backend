using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.DB;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.V1.Controllers;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP schedule by teachers model from the KhPI schedule by teachers.
    /// </summary>
    public class ScheduleByProfConverter : ITypeConverter<ScheduleKhPI, List<ProfSchedule>> // TODO check how it works
    {
        private readonly NoAuthDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleByProfConverter"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ScheduleByProfConverter(NoAuthDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

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
            if (subjectListMonday == null &&
                subjectListTuesday == null &&
                subjectListWednesday == null &&
                subjectListThursday == null &&
                subjectListFriday == null)
            {
                return null;
            }

            var (audienceListMonday, audienceListTuesday, audienceListWednesday, audienceListThursday, audienceListFriday) = ScheduleStuff.GetAudienceLists(source);
            var (typeListMonday, typeListTuesday, typeListWednesday, typeListThursday, typeListFriday) = ScheduleStuff.GetTypeLists(source);
            var (groupListMonday, groupListTuesday, groupListWednesday, groupListThursday, groupListFriday) = ScheduleStuff.GetProfLists(source);

            if (subjectListMonday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListMonday, ref groupListDestination, ref groupListNamesDestination, this._context);
                ScheduleStuff.AudienceIdentification(audienceListMonday, ref buildingListDestination, ref audienceListDestination, this._context);
                RegisterProfScheduleLessons(ref obj, Day.Monday, week, subjectListMonday, typeListMonday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListTuesday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListTuesday, ref groupListDestination, ref groupListNamesDestination, this._context);
                ScheduleStuff.AudienceIdentification(audienceListTuesday, ref buildingListDestination, ref audienceListDestination, this._context);
                RegisterProfScheduleLessons(ref obj, Day.Tuesday, week, subjectListTuesday, typeListTuesday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListWednesday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListWednesday, ref groupListDestination, ref groupListNamesDestination, this._context);
                ScheduleStuff.AudienceIdentification(audienceListWednesday, ref buildingListDestination, ref audienceListDestination, this._context);
                RegisterProfScheduleLessons(ref obj, Day.Wednesday, week, subjectListWednesday, typeListWednesday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListThursday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListThursday, ref groupListDestination, ref groupListNamesDestination, this._context);
                ScheduleStuff.AudienceIdentification(audienceListThursday, ref buildingListDestination, ref audienceListDestination, this._context);
                RegisterProfScheduleLessons(ref obj, Day.Thursday, week, subjectListThursday, typeListThursday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
            }

            if (subjectListFriday != null)
            {
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();
                var audienceListDestination = ScheduleStuff.IdAndNameListDestination();
                var buildingListDestination = new List<int?> { null, null, null, null, null, null };

                ScheduleStuff.GroupsIdentification(groupListFriday, ref groupListDestination, ref groupListNamesDestination, this._context);
                ScheduleStuff.AudienceIdentification(audienceListFriday, ref buildingListDestination, ref audienceListDestination, this._context);
                RegisterProfScheduleLessons(ref obj, Day.Friday, week, subjectListFriday, typeListFriday, groupListDestination, groupListNamesDestination, buildingListDestination, audienceListDestination);
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
