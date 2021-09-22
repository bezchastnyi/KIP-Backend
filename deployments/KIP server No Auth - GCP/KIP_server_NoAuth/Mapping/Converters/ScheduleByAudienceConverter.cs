using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.DB;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.V1.Controllers;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP schedule by group model from the KhPI schedule by group.
    /// </summary>
    public class ScheduleByAudienceConverter : ITypeConverter<ScheduleKhPI, List<AudienceSchedule>> // TODO check how it works
    {
        private readonly NoAuthDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleByAudienceConverter"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ScheduleByAudienceConverter(NoAuthDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Convert model of schedule by group from KhPI to KIP.
        /// </summary>
        /// <returns>Object of schedule by group of model schedule by group KIP.</returns>
        /// <param name="source">Model of schedule by group KhPI.</param>
        /// <param name="destination">Model of schedule by group KIP.</param>
        /// <param name="context">The context. </param>
        public List<AudienceSchedule> Convert(ScheduleKhPI source, List<AudienceSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new List<AudienceSchedule>();
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

            var (typeListMonday, typeListTuesday, typeListWednesday, typeListThursday, typeListFriday) = ScheduleStuff.GetAudienceLists(source);
            var (profListMonday, profListTuesday, profListWednesday, profListThursday, profListFriday) = ScheduleStuff.GetTypeLists(source);
            var (groupListMonday, groupListTuesday, groupListWednesday, groupListThursday, groupListFriday) = ScheduleStuff.GetProfLists(source);

            if (subjectListMonday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();

                ScheduleStuff.GroupsIdentification(groupListMonday, ref groupListDestination, ref groupListNamesDestination, this._context);
                this.ProfIdentification(profListMonday, ref profListDestination);
                RegisterAudienceScheduleLessons(ref obj, Day.Monday, week, subjectListMonday, typeListMonday, groupListDestination, groupListNamesDestination, profListDestination);
            }

            if (subjectListTuesday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();

                ScheduleStuff.GroupsIdentification(groupListTuesday, ref groupListDestination, ref groupListNamesDestination, this._context);
                this.ProfIdentification(profListTuesday, ref profListDestination);
                RegisterAudienceScheduleLessons(ref obj, Day.Tuesday, week, subjectListTuesday, typeListTuesday, groupListDestination, groupListNamesDestination, profListDestination);
            }

            if (subjectListWednesday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();

                ScheduleStuff.GroupsIdentification(groupListWednesday, ref groupListDestination, ref groupListNamesDestination, this._context);
                this.ProfIdentification(profListWednesday, ref profListDestination);
                RegisterAudienceScheduleLessons(ref obj, Day.Wednesday, week, subjectListWednesday, typeListWednesday, groupListDestination, groupListNamesDestination, profListDestination);
            }

            if (subjectListThursday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();

                ScheduleStuff.GroupsIdentification(groupListThursday, ref groupListDestination, ref groupListNamesDestination, this._context);
                this.ProfIdentification(profListThursday, ref profListDestination);
                RegisterAudienceScheduleLessons(ref obj, Day.Thursday, week, subjectListThursday, typeListThursday, groupListDestination, groupListNamesDestination, profListDestination);
            }

            if (subjectListFriday != null)
            {
                var profListDestination = ScheduleStuff.IdAndNameListDestination();
                var groupListDestination = ScheduleStuff.GroupListDestination();
                var groupListNamesDestination = ScheduleStuff.GroupListNamesDestination();

                ScheduleStuff.GroupsIdentification(groupListFriday, ref groupListDestination, ref groupListNamesDestination, this._context);
                this.ProfIdentification(profListFriday, ref profListDestination);
                RegisterAudienceScheduleLessons(ref obj, Day.Friday, week, subjectListFriday, typeListFriday, groupListDestination, groupListNamesDestination, profListDestination);
            }

            return obj.Count == 0 ? null : obj;
        }

        private static void RegisterAudienceScheduleLessons(
            ref List<AudienceSchedule> obj,
            Day day,
            Week week,
            List<string> subjectList,
            List<string> typeList,
            List<List<int?>> groupListDestination,
            List<string> groupListNamesDestination,
            List<(int? id, string name)> profListDestination)
        {
            for (var i = 0; i < subjectList.Count; i++)
            {
                if (subjectList[i] != string.Empty)
                {
                    obj.Add(new AudienceSchedule
                    {
                        Day = day,
                        Week = week,
                        SubjectName = subjectList[i],
                        Type = typeList[i],
                        Number = i,
                        GroupId = groupListDestination[i],
                        GroupNames = groupListNamesDestination[i],
                        ProfId = profListDestination[i].id,
                        ProfName = profListDestination[i].name,
                    });
                }
            }
        }

        private void ProfIdentification(List<string> profList, ref List<(int? id, string name)> profListDestination)
        {
            for (var i = 0; i < profList.Count; i++)
            {
                if (string.IsNullOrEmpty(profList[i]) || string.IsNullOrWhiteSpace(profList[i]))
                {
                    continue;
                }

                if (this._context.Prof == null)
                {
                    profListDestination[i] = (null, profList[i]);
                    continue;
                }

                var prof = this._context.Prof.FirstOrDefault(p => profList[i].Contains(p.ProfSurname));
                if (prof != null)
                {
                    profListDestination[i] = (prof.ProfId, prof.ProfSurname);
                }
            }
        }
    }
}
