using AutoMapper;
using System;
using KIP_POST_APP.Models.KHPIDB;
using KIP_POST_APP.Models.KIPDB;
using System.Collections.Generic;
using KIP_POST_APP.Models;
using KIP_POST_APP.Services;

namespace KIP_POST_APP.Mapping.Converters
{
    public class ScheduleByGroup_KHPIToListOfSchedule_KIPConverter : ITypeConverter<ScheduleByGroup_KHPI, List<StudentSchedule>>
    {
        public List<StudentSchedule> Convert(ScheduleByGroup_KHPI source, List<StudentSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new List<StudentSchedule>();


            var profListMonday = new List<int>() { 0, 0, 0, 0, 0, 0 };
            var profListTuesday = new List<int>() { 0, 0, 0, 0, 0, 0 };
            var profListWednesday = new List<int>() { 0, 0, 0, 0, 0, 0 };
            var profListThursday = new List<int>() { 0, 0, 0, 0, 0, 0 };
            var profListFriday = new List<int>() { 0, 0, 0, 0, 0, 0 };

            if (MappedDataToKIPDB.ProfList != null)
            {
                foreach (var prof in MappedDataToKIPDB.ProfList)
                {
                    if (source.Monday.Para1.Prepod.Contains(prof.ProfSurname))
                    {
                        profListMonday[0] = prof.ProfID;
                    }

                    if (source.Monday.Para2.Prepod.Contains(prof.ProfSurname))
                    {
                        profListMonday[1] = prof.ProfID;
                    }

                    if (source.Monday.Para3.Prepod.Contains(prof.ProfSurname))
                    {
                        profListMonday[2] = prof.ProfID;
                    }

                    if (source.Monday.Para4.Prepod.Contains(prof.ProfSurname))
                    {
                        profListMonday[3] = prof.ProfID;
                    }

                    if (source.Monday.Para5.Prepod.Contains(prof.ProfSurname))
                    {
                        profListMonday[4] = prof.ProfID;
                    }

                    if (source.Monday.Para6.Prepod.Contains(prof.ProfSurname))
                    {
                        profListMonday[5] = prof.ProfID;
                    }


                    if (source.Tuesday.Para1.Prepod.Contains(prof.ProfSurname))
                    {
                        profListTuesday[0] = prof.ProfID;
                    }

                    if (source.Tuesday.Para2.Prepod.Contains(prof.ProfSurname))
                    {
                        profListTuesday[1] = prof.ProfID;
                    }

                    if (source.Tuesday.Para3.Prepod.Contains(prof.ProfSurname))
                    {
                        profListTuesday[2] = prof.ProfID;
                    }

                    if (source.Tuesday.Para4.Prepod.Contains(prof.ProfSurname))
                    {
                        profListTuesday[3] = prof.ProfID;
                    }

                    if (source.Tuesday.Para5.Prepod.Contains(prof.ProfSurname))
                    {
                        profListTuesday[4] = prof.ProfID;
                    }

                    if (source.Tuesday.Para6.Prepod.Contains(prof.ProfSurname))
                    {
                        profListTuesday[5] = prof.ProfID;
                    }


                    if (source.Wednesday.Para1.Prepod.Contains(prof.ProfSurname))
                    {
                        profListWednesday[0] = prof.ProfID;
                    }

                    if (source.Wednesday.Para2.Prepod.Contains(prof.ProfSurname))
                    {
                        profListWednesday[1] = prof.ProfID;
                    }

                    if (source.Wednesday.Para3.Prepod.Contains(prof.ProfSurname))
                    {
                        profListWednesday[2] = prof.ProfID;
                    }

                    if (source.Wednesday.Para4.Prepod.Contains(prof.ProfSurname))
                    {
                        profListWednesday[3] = prof.ProfID;
                    }

                    if (source.Wednesday.Para5.Prepod.Contains(prof.ProfSurname))
                    {
                        profListWednesday[4] = prof.ProfID;
                    }

                    if (source.Wednesday.Para6.Prepod.Contains(prof.ProfSurname))
                    {
                        profListWednesday[5] = prof.ProfID;
                    }


                    if (source.Thursday.Para1.Prepod.Contains(prof.ProfSurname))
                    {
                        profListThursday[0] = prof.ProfID;
                    }

                    if (source.Thursday.Para2.Prepod.Contains(prof.ProfSurname))
                    {
                        profListThursday[1] = prof.ProfID;
                    }

                    if (source.Thursday.Para3.Prepod.Contains(prof.ProfSurname))
                    {
                        profListThursday[2] = prof.ProfID;
                    }

                    if (source.Thursday.Para4.Prepod.Contains(prof.ProfSurname))
                    {
                        profListThursday[3] = prof.ProfID;
                    }

                    if (source.Thursday.Para5.Prepod.Contains(prof.ProfSurname))
                    {
                        profListThursday[4] = prof.ProfID;
                    }

                    if (source.Thursday.Para6.Prepod.Contains(prof.ProfSurname))
                    {
                        profListThursday[5] = prof.ProfID;
                    }


                    if (source.Friday.Para1.Prepod.Contains(prof.ProfSurname))
                    {
                        profListFriday[0] = prof.ProfID;
                    }

                    if (source.Friday.Para2.Prepod.Contains(prof.ProfSurname))
                    {
                        profListFriday[1] = prof.ProfID;
                    }

                    if (source.Friday.Para3.Prepod.Contains(prof.ProfSurname))
                    {
                        profListFriday[2] = prof.ProfID;
                    }

                    if (source.Friday.Para4.Prepod.Contains(prof.ProfSurname))
                    {
                        profListFriday[3] = prof.ProfID;
                    }

                    if (source.Friday.Para5.Prepod.Contains(prof.ProfSurname))
                    {
                        profListFriday[4] = prof.ProfID;
                    }

                    if (source.Friday.Para6.Prepod.Contains(prof.ProfSurname))
                    {
                        profListFriday[5] = prof.ProfID;
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
                    obj.Add(new StudentSchedule
                    {
                        day = Day.Monday,
                        week = Week.UnPaired,
                        ProfID = profListMonday[i],
                        SubjectName = SubjectListMonday[i],
                        Type = TypeListMonday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListTuesday[i] != string.Empty)
                {
                    obj.Add(new StudentSchedule
                    {
                        day = Day.Tuesday,
                        week = Week.UnPaired,
                        ProfID = profListTuesday[i],
                        SubjectName = SubjectListTuesday[i],
                        Type = TypeListTuesday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListWednesday[i] != string.Empty)
                {
                    obj.Add(new StudentSchedule
                    {
                        day = Day.Wednesday,
                        week = Week.UnPaired,
                        ProfID = profListWednesday[i],
                        SubjectName = SubjectListWednesday[i],
                        Type = TypeListWednesday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListThursday[i] != string.Empty)
                {
                    obj.Add(new StudentSchedule
                    {
                        day = Day.Thursday,
                        week = Week.UnPaired,
                        ProfID = profListThursday[i],
                        SubjectName = SubjectListThursday[i],
                        Type = TypeListThursday[i]
                    });
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (SubjectListFriday[i] != string.Empty)
                {
                    obj.Add(new StudentSchedule
                    {
                        day = Day.Friday,
                        week = Week.UnPaired,
                        ProfID = profListFriday[i],
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
