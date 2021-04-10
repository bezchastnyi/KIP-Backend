using AutoMapper;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Models.KIP.Helpers;
using KIP_POST_APP.Services;
using System;
using System.Collections.Generic;

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

            if (MappedDataToKIPDB.ProfList == null)
            {
                return null;
            }

            try
            {
                var week = KIP_POST_APPHostedService.week;

                var SubjectListMonday = new List<string>(6)
                {
                    source.Monday.Para1.Name,
                    source.Monday.Para2.Name,
                    source.Monday.Para3.Name,
                    source.Monday.Para4.Name,
                    source.Monday.Para5.Name,
                    source.Monday.Para6.Name
                };

                bool exists = false;
                foreach (var lesson in SubjectListMonday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var AuditoryListMonday = new List<string>(6)
                    {
                        source.Monday.Para1.Aud,
                        source.Monday.Para2.Aud,
                        source.Monday.Para3.Aud,
                        source.Monday.Para4.Aud,
                        source.Monday.Para5.Aud,
                        source.Monday.Para6.Aud
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

                    var ProfListMonday = new List<string>(6)
                    {
                        source.Monday.Para1.Prepod,
                        source.Monday.Para2.Prepod,
                        source.Monday.Para3.Prepod,
                        source.Monday.Para4.Prepod,
                        source.Monday.Para5.Prepod,
                        source.Monday.Para6.Prepod
                    };

                    var profListMondayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < ProfListMonday.Count; i++)
                    {
                        if (ProfListMonday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (ProfListMonday[i].Contains(prof.ProfSurname))
                            {
                                profListMondayDestination[i] = prof.ProfID;
                                break;
                            }
                        }
                    }

                    var buildingListMonday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListMonday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (int i = 0; i < AuditoryListMonday.Count; i++)
                        {
                            if (AuditoryListMonday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (AuditoryListMonday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListMonday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(AuditoryListMonday[i]))
                                        {
                                            audienceListMonday[i] = audience.AudienceID;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < SubjectListMonday.Count; i++)
                    {
                        if (SubjectListMonday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                day = Day.Monday,
                                week = week,
                                ProfID = profListMondayDestination[i],
                                SubjectName = SubjectListMonday[i],
                                AudienceID = audienceListMonday[i],
                                BuildingID = buildingListMonday[i],
                                Type = TypeListMonday[i]
                            });
                        }
                    }
                }
                

                var SubjectListTuesday = new List<string>(6)
                {
                    source.Tuesday.Para1.Name,
                    source.Tuesday.Para2.Name,
                    source.Tuesday.Para3.Name,
                    source.Tuesday.Para4.Name,
                    source.Tuesday.Para5.Name,
                    source.Tuesday.Para6.Name
                };

                exists = false;
                foreach (var lesson in SubjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var AuditoryListTuesday = new List<string>(6)
                    {
                        source.Tuesday.Para1.Aud,
                        source.Tuesday.Para2.Aud,
                        source.Tuesday.Para3.Aud,
                        source.Tuesday.Para4.Aud,
                        source.Tuesday.Para5.Aud,
                        source.Tuesday.Para6.Aud
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

                    var ProfListTuesday = new List<string>(6)
                    {
                    source.Tuesday.Para1.Prepod,
                    source.Tuesday.Para2.Prepod,
                    source.Tuesday.Para3.Prepod,
                    source.Tuesday.Para4.Prepod,
                    source.Tuesday.Para5.Prepod,
                    source.Tuesday.Para6.Prepod
                    };

                    var profListTuesdayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < ProfListTuesday.Count; i++)
                    {
                        if (ProfListTuesday[i] == string.Empty)
                            continue;

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (ProfListTuesday[i].Contains(prof.ProfSurname))
                            {
                                profListTuesdayDestination[i] = prof.ProfID;
                                break;
                            }
                        }
                    }

                    var buildingListTuesday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListTuesday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (int i = 0; i < AuditoryListTuesday.Count; i++)
                        {
                            if (AuditoryListTuesday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (AuditoryListTuesday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListTuesday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(AuditoryListTuesday[i]))
                                        {
                                            audienceListTuesday[i] = audience.AudienceID;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < SubjectListTuesday.Count; i++)
                    {
                        if (SubjectListTuesday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                day = Day.Tuesday,
                                week = week,
                                ProfID = profListTuesdayDestination[i],
                                SubjectName = SubjectListTuesday[i],
                                AudienceID = audienceListTuesday[i],
                                BuildingID = buildingListTuesday[i],
                                Type = TypeListTuesday[i]
                            });
                        }
                    }
                }


                var SubjectListWednesday = new List<string>(6)
                {
                    source.Wednesday.Para1.Name,
                    source.Wednesday.Para2.Name,
                    source.Wednesday.Para3.Name,
                    source.Wednesday.Para4.Name,
                    source.Wednesday.Para5.Name,
                    source.Wednesday.Para6.Name
                };

                exists = false;
                foreach (var lesson in SubjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var AuditoryListWednesday = new List<string>(6)
                    {
                        source.Wednesday.Para1.Aud,
                        source.Wednesday.Para2.Aud,
                        source.Wednesday.Para3.Aud,
                        source.Wednesday.Para4.Aud,
                        source.Wednesday.Para5.Aud,
                        source.Wednesday.Para6.Aud
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

                    var ProfListWednesday = new List<string>(6)
                    {
                    source.Wednesday.Para1.Prepod,
                    source.Wednesday.Para2.Prepod,
                    source.Wednesday.Para3.Prepod,
                    source.Wednesday.Para4.Prepod,
                    source.Wednesday.Para5.Prepod,
                    source.Wednesday.Para6.Prepod
                    };

                    var profListWednesdayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < ProfListWednesday.Count; i++)
                    {
                        if (ProfListWednesday[i] == string.Empty)
                            continue;

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (ProfListWednesday[i].Contains(prof.ProfSurname))
                            {
                                profListWednesdayDestination[i] = prof.ProfID;
                                break;
                            }
                        }
                    }

                    var buildingListWednesday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListWednesday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (int i = 0; i < AuditoryListWednesday.Count; i++)
                        {
                            if (AuditoryListWednesday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (AuditoryListWednesday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListWednesday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(AuditoryListWednesday[i]))
                                        {
                                            audienceListWednesday[i] = audience.AudienceID;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < SubjectListWednesday.Count; i++)
                    {
                        if (SubjectListWednesday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                day = Day.Wednesday,
                                week = week,
                                ProfID = profListWednesdayDestination[i],
                                SubjectName = SubjectListWednesday[i],
                                AudienceID = audienceListWednesday[i],
                                BuildingID = buildingListWednesday[i],
                                Type = TypeListWednesday[i]
                            });
                        }
                    }
                }


                var SubjectListThursday = new List<string>(6)
                {
                    source.Thursday.Para1.Name,
                    source.Thursday.Para2.Name,
                    source.Thursday.Para3.Name,
                    source.Thursday.Para4.Name,
                    source.Thursday.Para5.Name,
                    source.Thursday.Para6.Name
                };

                exists = false;
                foreach (var lesson in SubjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var AuditoryListThursday = new List<string>(6)
                    {
                        source.Thursday.Para1.Aud,
                        source.Thursday.Para2.Aud,
                        source.Thursday.Para3.Aud,
                        source.Thursday.Para4.Aud,
                        source.Thursday.Para5.Aud,
                        source.Thursday.Para6.Aud
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

                    var ProfListThursday = new List<string>(6)
                    {
                    source.Thursday.Para1.Prepod,
                    source.Thursday.Para2.Prepod,
                    source.Thursday.Para3.Prepod,
                    source.Thursday.Para4.Prepod,
                    source.Thursday.Para5.Prepod,
                    source.Thursday.Para6.Prepod
                    };

                    var profListThursdayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < ProfListThursday.Count; i++)
                    {
                        if (ProfListThursday[i] == string.Empty)
                            continue;

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (ProfListThursday[i].Contains(prof.ProfSurname))
                            {
                                profListThursdayDestination[i] = prof.ProfID;
                                break;
                            }
                        }
                    }

                    var buildingListThursday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListThursday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (int i = 0; i < AuditoryListThursday.Count; i++)
                        {
                            if (AuditoryListThursday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (AuditoryListThursday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListThursday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(AuditoryListThursday[i]))
                                        {
                                            audienceListThursday[i] = audience.AudienceID;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < SubjectListThursday.Count; i++)
                    {
                        if (SubjectListThursday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                day = Day.Thursday,
                                week = week,
                                ProfID = profListThursdayDestination[i],
                                SubjectName = SubjectListThursday[i],
                                AudienceID = audienceListThursday[i],
                                BuildingID = buildingListThursday[i],
                                Type = TypeListThursday[i]
                            });
                        }
                    }
                }


                var SubjectListFriday = new List<string>(6)
                {
                    source.Wednesday.Para1.Name,
                    source.Wednesday.Para2.Name,
                    source.Wednesday.Para3.Name,
                    source.Wednesday.Para4.Name,
                    source.Wednesday.Para5.Name,
                    source.Wednesday.Para6.Name
                };

                exists = false;
                foreach (var lesson in SubjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var AuditoryListFriday = new List<string>(6)
                    {
                        source.Friday.Para1.Aud,
                        source.Friday.Para2.Aud,
                        source.Friday.Para3.Aud,
                        source.Friday.Para4.Aud,
                        source.Friday.Para5.Aud,
                        source.Friday.Para6.Aud
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

                    var ProfListFriday = new List<string>(6)
                    {
                        source.Friday.Para1.Prepod,
                        source.Friday.Para2.Prepod,
                        source.Friday.Para3.Prepod,
                        source.Friday.Para4.Prepod,
                        source.Friday.Para5.Prepod,
                        source.Friday.Para6.Prepod
                    };

                    var profListFridayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < ProfListFriday.Count; i++)
                    {
                        if (ProfListFriday[i] == string.Empty)
                            continue;

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (ProfListFriday[i].Contains(prof.ProfSurname))
                            {
                                profListFridayDestination[i] = prof.ProfID;
                                break;
                            }
                        }
                    }

                    var buildingListFriday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListFriday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (int i = 0; i < AuditoryListFriday.Count; i++)
                        {
                            if (AuditoryListFriday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (AuditoryListFriday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListFriday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(AuditoryListFriday[i]))
                                        {
                                            audienceListFriday[i] = audience.AudienceID;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < SubjectListFriday.Count; i++)
                    {
                        if (SubjectListFriday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                day = Day.Friday,
                                week = week,
                                ProfID = profListFridayDestination[i],
                                SubjectName = SubjectListFriday[i],
                                AudienceID = audienceListFriday[i],
                                BuildingID = buildingListFriday[i],
                                Type = TypeListFriday[i]
                            });
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            if (obj.Count == 0)
                return null;

            return obj;
        }
    }
}
