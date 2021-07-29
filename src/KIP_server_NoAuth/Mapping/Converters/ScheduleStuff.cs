using System.Collections.Generic;
using System.Linq;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.Services;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// ScheduleStuff.
    /// </summary>
    public static class ScheduleStuff
    {
        /// <summary>
        /// GetSubjectLists.
        /// </summary>
        /// <param name="source">Model of schedule by group KhPI.</param>
        /// <returns>List.</returns>
        public static (List<string> subjectListMonday, List<string> subjectListTuesday, List<string> subjectListWednesday, List<string> subjectListThursday, List<string> subjectListFriday)
            GetSubjectLists(ScheduleKhPI source)
        {
            if (source == null)
            {
                return default;
            }

            var subjectListMonday = new List<string>
            {
                source.Monday.Para1.Name,
                source.Monday.Para2.Name,
                source.Monday.Para3.Name,
                source.Monday.Para4.Name,
                source.Monday.Para5.Name,
                source.Monday.Para6.Name,
            };

            var subject = subjectListMonday.FirstOrDefault(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s));
            if (subject == null)
            {
                subjectListMonday = null;
            }

            var subjectListTuesday = new List<string>
            {
                source.Tuesday.Para1.Name,
                source.Tuesday.Para2.Name,
                source.Tuesday.Para3.Name,
                source.Tuesday.Para4.Name,
                source.Tuesday.Para5.Name,
                source.Tuesday.Para6.Name,
            };

            subject = subjectListTuesday.FirstOrDefault(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s));
            if (subject == null)
            {
                subjectListTuesday = null;
            }

            var subjectListWednesday = new List<string>
            {
                source.Wednesday.Para1.Name,
                source.Wednesday.Para2.Name,
                source.Wednesday.Para3.Name,
                source.Wednesday.Para4.Name,
                source.Wednesday.Para5.Name,
                source.Wednesday.Para6.Name,
            };

            subject = subjectListWednesday.FirstOrDefault(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s));
            if (subject == null)
            {
                subjectListWednesday = null;
            }

            var subjectListThursday = new List<string>
            {
                source.Thursday.Para1.Name,
                source.Thursday.Para2.Name,
                source.Thursday.Para3.Name,
                source.Thursday.Para4.Name,
                source.Thursday.Para5.Name,
                source.Thursday.Para6.Name,
            };

            subject = subjectListThursday.FirstOrDefault(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s));
            if (subject == null)
            {
                subjectListThursday = null;
            }

            var subjectListFriday = new List<string>
            {
                source.Friday.Para1.Name,
                source.Friday.Para2.Name,
                source.Friday.Para3.Name,
                source.Friday.Para4.Name,
                source.Friday.Para5.Name,
                source.Friday.Para6.Name,
            };

            subject = subjectListFriday.FirstOrDefault(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s));
            if (subject == null)
            {
                subjectListFriday = null;
            }

            return (subjectListMonday, subjectListTuesday, subjectListWednesday, subjectListThursday, subjectListFriday);
        }

        /// <summary>
        /// GetAudienceLists.
        /// </summary>
        /// <param name="source">Model of schedule by group KhPI.</param>
        /// <returns>List.</returns>
        public static (List<string> audienceListMonday, List<string> audienceListTuesday, List<string> audienceListWednesday, List<string> audienceListThursday, List<string> audienceListFriday)
            GetAudienceLists(ScheduleKhPI source)
        {
            if (source == null)
            {
                return default;
            }

            var audienceListMonday = new List<string>
            {
                source.Monday.Para1.Aud,
                source.Monday.Para2.Aud,
                source.Monday.Para3.Aud,
                source.Monday.Para4.Aud,
                source.Monday.Para5.Aud,
                source.Monday.Para6.Aud,
            };

            var audienceListTuesday = new List<string>
            {
                source.Tuesday.Para1.Aud,
                source.Tuesday.Para2.Aud,
                source.Tuesday.Para3.Aud,
                source.Tuesday.Para4.Aud,
                source.Tuesday.Para5.Aud,
                source.Tuesday.Para6.Aud,
            };

            var audienceListWednesday = new List<string>
            {
                source.Wednesday.Para1.Aud,
                source.Wednesday.Para2.Aud,
                source.Wednesday.Para3.Aud,
                source.Wednesday.Para4.Aud,
                source.Wednesday.Para5.Aud,
                source.Wednesday.Para6.Aud,
            };

            var audienceListThursday = new List<string>
            {
                source.Thursday.Para1.Aud,
                source.Thursday.Para2.Aud,
                source.Thursday.Para3.Aud,
                source.Thursday.Para4.Aud,
                source.Thursday.Para5.Aud,
                source.Thursday.Para6.Aud,
            };

            var audienceListFriday = new List<string>
            {
                source.Friday.Para1.Aud,
                source.Friday.Para2.Aud,
                source.Friday.Para3.Aud,
                source.Friday.Para4.Aud,
                source.Friday.Para5.Aud,
                source.Friday.Para6.Aud,
            };

            return (audienceListMonday, audienceListTuesday, audienceListWednesday, audienceListThursday, audienceListFriday);
        }

        /// <summary>
        /// GetTypeLists.
        /// </summary>
        /// <param name="source">Model of schedule by group KhPI.</param>
        /// <returns>List.</returns>
        public static (List<string> typeListMonday, List<string> typeListTuesday, List<string> typeListWednesday, List<string> typeListThursday, List<string> typeListFriday)
            GetTypeLists(ScheduleKhPI source)
        {
            if (source == null)
            {
                return default;
            }

            var typeListMonday = new List<string>
            {
                source.Monday.Para1.vid,
                source.Monday.Para2.vid,
                source.Monday.Para3.vid,
                source.Monday.Para4.vid,
                source.Monday.Para5.vid,
                source.Monday.Para6.vid,
            };

            var typeListTuesday = new List<string>
            {
                source.Tuesday.Para1.vid,
                source.Tuesday.Para2.vid,
                source.Tuesday.Para3.vid,
                source.Tuesday.Para4.vid,
                source.Tuesday.Para5.vid,
                source.Tuesday.Para6.vid,
            };

            var typeListWednesday = new List<string>
            {
                source.Wednesday.Para1.vid,
                source.Wednesday.Para2.vid,
                source.Wednesday.Para3.vid,
                source.Wednesday.Para4.vid,
                source.Wednesday.Para5.vid,
                source.Wednesday.Para6.vid,
            };

            var typeListThursday = new List<string>
            {
                source.Thursday.Para1.vid,
                source.Thursday.Para2.vid,
                source.Thursday.Para3.vid,
                source.Thursday.Para4.vid,
                source.Thursday.Para5.vid,
                source.Thursday.Para6.vid,
            };

            var typeListFriday = new List<string>
            {
                source.Friday.Para1.vid,
                source.Friday.Para2.vid,
                source.Friday.Para3.vid,
                source.Friday.Para4.vid,
                source.Friday.Para5.vid,
                source.Friday.Para6.vid,
            };

            return (typeListMonday, typeListTuesday, typeListWednesday, typeListThursday, typeListFriday);
        }

        /// <summary>
        /// GetProfLists.
        /// </summary>
        /// <param name="source">Model of schedule by group KhPI.</param>
        /// <returns>List.</returns>
        public static (List<string> profListMonday, List<string> profListTuesday, List<string> profListWednesday, List<string> profListThursday, List<string> profListFriday)
            GetProfLists(ScheduleKhPI source)
        {
            if (source == null)
            {
                return default;
            }

            var profListMonday = new List<string>
            {
                source.Monday.Para1.Prepod,
                source.Monday.Para2.Prepod,
                source.Monday.Para3.Prepod,
                source.Monday.Para4.Prepod,
                source.Monday.Para5.Prepod,
                source.Monday.Para6.Prepod,
            };

            var profListTuesday = new List<string>
            {
                source.Tuesday.Para1.Prepod,
                source.Tuesday.Para2.Prepod,
                source.Tuesday.Para3.Prepod,
                source.Tuesday.Para4.Prepod,
                source.Tuesday.Para5.Prepod,
                source.Tuesday.Para6.Prepod,
            };

            var profListWednesday = new List<string>
            {
                source.Wednesday.Para1.Prepod,
                source.Wednesday.Para2.Prepod,
                source.Wednesday.Para3.Prepod,
                source.Wednesday.Para4.Prepod,
                source.Wednesday.Para5.Prepod,
                source.Wednesday.Para6.Prepod,
            };

            var profListThursday = new List<string>
            {
                source.Thursday.Para1.Prepod,
                source.Thursday.Para2.Prepod,
                source.Thursday.Para3.Prepod,
                source.Thursday.Para4.Prepod,
                source.Thursday.Para5.Prepod,
                source.Thursday.Para6.Prepod,
            };

            var profListFriday = new List<string>
            {
                source.Friday.Para1.Prepod,
                source.Friday.Para2.Prepod,
                source.Friday.Para3.Prepod,
                source.Friday.Para4.Prepod,
                source.Friday.Para5.Prepod,
                source.Friday.Para6.Prepod,
            };

            return (profListMonday, profListTuesday, profListWednesday, profListThursday, profListFriday);
        }

        /// <summary>
        /// AudienceIdentification.
        /// </summary>
        /// <param name="audienceList">Model of schedule by group KhPI.</param>
        /// <param name="buildingListDestination">Model of schedule by group KhPI.</param>
        /// <param name="audienceListDestination">Model of schedule by group KhPI.</param>
        public static void AudienceIdentification(List<string> audienceList, ref List<int?> buildingListDestination, ref List<(int? id, string name)> audienceListDestination)
        {
            for (var i = 0; i < audienceList.Count; i++)
            {
                if (string.IsNullOrEmpty(audienceList[i]) || string.IsNullOrWhiteSpace(audienceList[i]))
                {
                    continue;
                }

                if (MapService.BuildingList == null || MapService.AudienceList == null)
                {
                    audienceListDestination[i] = (null, audienceList[i]);
                    continue;
                }

                var building = MapService.BuildingList.FirstOrDefault(b => audienceList[i].Contains(b.BuildingShortName));
                if (building == null)
                {
                    audienceListDestination[i] = (null, audienceList[i]);
                    continue;
                }

                buildingListDestination[i] = building.BuildingId;

                var audience = MapService.AudienceList.FirstOrDefault(a => a.AudienceName.Contains(audienceList[i]));
                if (audience != null)
                {
                    audienceListDestination[i] = (audience.AudienceId, audience.AudienceName);
                }
            }
        }

        /// <summary>
        /// AudienceIdentification.
        /// </summary>
        /// <param name="groupList">Model of schedule by group KhPI.</param>
        /// <param name="groupListDestination">Model of schedule by group KhPI.</param>
        /// <param name="groupListNamesDestination">Model of schedule by group KhPI.</param>
        public static void GroupsIdentification(List<string> groupList, ref List<List<int?>> groupListDestination, ref List<string> groupListNamesDestination)
        {
            for (var i = 0; i < groupList.Count; i++)
            {
                if (string.IsNullOrEmpty(groupList[i]) || string.IsNullOrWhiteSpace(groupList[i]))
                {
                    continue;
                }

                if (MapService.GroupList == null)
                {
                    groupListNamesDestination[i] += groupList[i];
                    continue;
                }

                var groups = MapService.GroupList.Where(g => groupList[i].Contains(g.GroupName));
                foreach (var g in groups)
                {
                    groupListDestination[i].Add(g.GroupId);
                    groupListNamesDestination[i] += g.GroupName + " ";
                }
            }
        }

        /// <summary>
        /// IdAndNameListDestination.
        /// </summary>
        /// <returns>List.</returns>
        public static List<(int? id, string name)> IdAndNameListDestination() =>
            new List<(int? id, string name)>
            {
                (null, string.Empty),
                (null, string.Empty),
                (null, string.Empty),
                (null, string.Empty),
                (null, string.Empty),
                (null, string.Empty),
            };

        /// <summary>
        /// IdAndNameListDestination.
        /// </summary>
        /// <returns>List.</returns>
        public static List<List<int?>> GroupListDestination() =>
            new List<List<int?>>
            {
                new List<int?>(),
                new List<int?>(),
                new List<int?>(),
                new List<int?>(),
                new List<int?>(),
                new List<int?>(),
            };

        /// <summary>
        /// IdAndNameListDestination.
        /// </summary>
        /// <returns>List.</returns>
        public static List<string> GroupListNamesDestination() =>
            new List<string>
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
            };
    }
}
