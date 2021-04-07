using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPIDB;
using KIP_POST_APP.Models.KIPDB;
using System.Collections.Generic;
using KIP_POST_APP.Models;
using KIP_POST_APP.Services;

namespace KIP_POST_APP.Mapping.Converters
{
    public class ScheduleByProf_KHPIToListOfProfSchedule_KIPConverter : ITypeConverter<ScheduleByProf_KHPI, List<ProfSchedule>>
    {
        public List<ProfSchedule> Convert(ScheduleByProf_KHPI source, List<ProfSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new List<ProfSchedule>();


            var groupListMonday = new List<List<int>>() { new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int> () { 0 }};

            var groupListTuesday = new List<List<int>>() { new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int> () { 0 }};

            var groupListWednesday = new List<List<int>>() { new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int> () { 0 }};

            var groupListThursday = new List<List<int>>() { new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int> () { 0 }};

            var groupListFriday = new List<List<int>>() { new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int>() { 0 },
                                                          new List<int> () { 0 }};

            if (MappedDataToKIPDB.GroupList != null)
            {
                foreach (var group in MappedDataToKIPDB.GroupList)
                {
                    if (source.Monday.Para1.Prepod.Contains(group.GroupName))
                    {
                        groupListMonday[0].Add(group.GroupID);
                    }

                    if (source.Monday.Para2.Prepod.Contains(group.GroupName))
                    {
                        groupListMonday[1].Add(group.GroupID);
                    }

                    if (source.Monday.Para3.Prepod.Contains(group.GroupName))
                    {
                        groupListMonday[2].Add(group.GroupID);
                    }

                    if (source.Monday.Para4.Prepod.Contains(group.GroupName))
                    {
                        groupListMonday[3].Add(group.GroupID);
                    }

                    if (source.Monday.Para5.Prepod.Contains(group.GroupName))
                    {
                        groupListMonday[4].Add(group.GroupID);
                    }

                    if (source.Monday.Para6.Prepod.Contains(group.GroupName))
                    {
                        groupListMonday[5].Add(group.GroupID);
                    }


                    if (source.Tuesday.Para1.Prepod.Contains(group.GroupName))
                    {
                        groupListTuesday[0].Add(group.GroupID);
                    }

                    if (source.Tuesday.Para2.Prepod.Contains(group.GroupName))
                    {
                        groupListTuesday[1].Add(group.GroupID);
                    }

                    if (source.Tuesday.Para3.Prepod.Contains(group.GroupName))
                    {
                        groupListTuesday[2].Add(group.GroupID);
                    }

                    if (source.Tuesday.Para4.Prepod.Contains(group.GroupName))
                    {
                        groupListTuesday[3].Add(group.GroupID);
                    }

                    if (source.Tuesday.Para5.Prepod.Contains(group.GroupName))
                    {
                        groupListTuesday[4].Add(group.GroupID);
                    }

                    if (source.Tuesday.Para6.Prepod.Contains(group.GroupName))
                    {
                        groupListTuesday[5].Add(group.GroupID);
                    }


                    if (source.Wednesday.Para1.Prepod.Contains(group.GroupName))
                    {
                        groupListWednesday[0].Add(group.GroupID);
                    }

                    if (source.Wednesday.Para2.Prepod.Contains(group.GroupName))
                    {
                        groupListWednesday[1].Add(group.GroupID);
                    }

                    if (source.Wednesday.Para3.Prepod.Contains(group.GroupName))
                    {
                        groupListWednesday[2].Add(group.GroupID);
                    }

                    if (source.Wednesday.Para4.Prepod.Contains(group.GroupName))
                    {
                        groupListWednesday[3].Add(group.GroupID);
                    }

                    if (source.Wednesday.Para5.Prepod.Contains(group.GroupName))
                    {
                        groupListWednesday[4].Add(group.GroupID);
                    }

                    if (source.Wednesday.Para6.Prepod.Contains(group.GroupName))
                    {
                        groupListWednesday[5].Add(group.GroupID);
                    }


                    if (source.Thursday.Para1.Prepod.Contains(group.GroupName))
                    {
                        groupListThursday[0].Add(group.GroupID);
                    }

                    if (source.Thursday.Para2.Prepod.Contains(group.GroupName))
                    {
                        groupListThursday[1].Add(group.GroupID);
                    }

                    if (source.Thursday.Para3.Prepod.Contains(group.GroupName))
                    {
                        groupListThursday[2].Add(group.GroupID);
                    }

                    if (source.Thursday.Para4.Prepod.Contains(group.GroupName))
                    {
                        groupListThursday[3].Add(group.GroupID);
                    }

                    if (source.Thursday.Para5.Prepod.Contains(group.GroupName))
                    {
                        groupListThursday[4].Add(group.GroupID);
                    }

                    if (source.Thursday.Para6.Prepod.Contains(group.GroupName))
                    {
                        groupListThursday[5].Add(group.GroupID);
                    }


                    if (source.Friday.Para1.Prepod.Contains(group.GroupName))
                    {
                        groupListFriday[0].Add(group.GroupID);
                    }

                    if (source.Friday.Para2.Prepod.Contains(group.GroupName))
                    {
                        groupListFriday[1].Add(group.GroupID);
                    }

                    if (source.Friday.Para3.Prepod.Contains(group.GroupName))
                    {
                        groupListFriday[2].Add(group.GroupID);
                    }

                    if (source.Friday.Para4.Prepod.Contains(group.GroupName))
                    {
                        groupListFriday[3].Add(group.GroupID);
                    }

                    if (source.Friday.Para5.Prepod.Contains(group.GroupName))
                    {
                        groupListFriday[4].Add(group.GroupID);
                    }

                    if (source.Friday.Para6.Prepod.Contains(group.GroupName))
                    {
                        groupListFriday[5].Add(group.GroupID);
                    }
                }
            }


            var SubjectListMonday = new List<string>(6)
            {
                source.Monday.Para1.Name,
                source.Monday.Para2.Name,
                source.Monday.Para3.Name,
                source.Monday.Para4.Name,
                source.Monday.Para5.Name,
                source.Monday.Para6.Name
            };

            var SubjectListTuesday = new List<string>(6)
            {
                source.Tuesday.Para1.Name,
                source.Tuesday.Para2.Name,
                source.Tuesday.Para3.Name,
                source.Tuesday.Para4.Name,
                source.Tuesday.Para5.Name,
                source.Tuesday.Para6.Name
            };

            var SubjectListWednesday = new List<string>(6)
            {
                source.Wednesday.Para1.Name,
                source.Wednesday.Para2.Name,
                source.Wednesday.Para3.Name,
                source.Wednesday.Para4.Name,
                source.Wednesday.Para5.Name,
                source.Wednesday.Para6.Name
            };

            var SubjectListThursday = new List<string>(6)
            {
                source.Thursday.Para1.Name,
                source.Thursday.Para2.Name,
                source.Thursday.Para3.Name,
                source.Thursday.Para4.Name,
                source.Thursday.Para5.Name,
                source.Thursday.Para6.Name
            };

            var SubjectListFriday = new List<string>(6)
            {
                source.Wednesday.Para1.Name,
                source.Wednesday.Para2.Name,
                source.Wednesday.Para3.Name,
                source.Wednesday.Para4.Name,
                source.Wednesday.Para5.Name,
                source.Wednesday.Para6.Name
            };

            var TypeListMonday = new List<string>(6)
            {
                source.Monday.Para1.vid,
                source.Monday.Para2.vid,
                source.Monday.Para3.vid,
                source.Monday.Para4.vid,
                source.Monday.Para5.vid,
                source.Monday.Para6.vid
            };

            var TypeListTuesday = new List<string>(6)
            {
                source.Tuesday.Para1.vid,
                source.Tuesday.Para2.vid,
                source.Tuesday.Para3.vid,
                source.Tuesday.Para4.vid,
                source.Tuesday.Para5.vid,
                source.Tuesday.Para6.vid
            };

            var TypeListWednesday = new List<string>(6)
            {
                source.Wednesday.Para1.vid,
                source.Wednesday.Para2.vid,
                source.Wednesday.Para3.vid,
                source.Wednesday.Para4.vid,
                source.Wednesday.Para5.vid,
                source.Wednesday.Para6.vid
            };

            var TypeListThursday = new List<string>(6)
            {
                source.Thursday.Para1.vid,
                source.Thursday.Para2.vid,
                source.Thursday.Para3.vid,
                source.Thursday.Para4.vid,
                source.Thursday.Para5.vid,
                source.Thursday.Para6.vid
            };

            var TypeListFriday = new List<string>(6)
            {
                source.Friday.Para1.vid,
                source.Friday.Para2.vid,
                source.Friday.Para3.vid,
                source.Friday.Para4.vid,
                source.Friday.Para5.vid,
                source.Friday.Para6.vid
            };


            for (int i = 0; i < 6; i++)
            {
                if (SubjectListMonday[i] != string.Empty)
                {
                    obj.Add(new ProfSchedule
                    {
                        day = Day.Monday,
                        week = Week.UnPaired,
                        GroupId = new List<int>(groupListMonday[i]),
                        SubjectName = SubjectListMonday[i],
                        Type = TypeListMonday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListTuesday[i] != string.Empty)
                {
                    obj.Add(new ProfSchedule
                    {
                        day = Day.Tuesday,
                        week = Week.UnPaired,
                        GroupId = new List<int>(groupListTuesday[i]),
                        SubjectName = SubjectListTuesday[i],
                        Type = TypeListTuesday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListWednesday[i] != string.Empty)
                {
                    obj.Add(new ProfSchedule
                    {
                        day = Day.Wednesday,
                        week = Week.UnPaired,
                        GroupId = new List<int>(groupListWednesday[i]),
                        SubjectName = SubjectListWednesday[i],
                        Type = TypeListWednesday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListThursday[i] != string.Empty)
                {
                    obj.Add(new ProfSchedule
                    {
                        day = Day.Thursday,
                        week = Week.UnPaired,
                        GroupId = new List<int>(groupListThursday[i]),
                        SubjectName = SubjectListThursday[i],
                        Type = TypeListThursday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListFriday[i] != string.Empty)
                {
                    obj.Add(new ProfSchedule
                    {
                        day = Day.Friday,
                        week = Week.UnPaired,
                        GroupId = new List<int>(groupListFriday[i]),
                        SubjectName = SubjectListFriday[i],
                        Type = TypeListFriday[i]
                    });
                }
            }

            if (obj.Count == 0)
                return null;

            return obj;
        }
    }
}
