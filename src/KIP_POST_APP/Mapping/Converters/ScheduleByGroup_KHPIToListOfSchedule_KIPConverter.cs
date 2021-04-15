// <copyright file="ScheduleByGroup_KHPIToListOfSchedule_KIPConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using KIP_POST_APP.Models.KHPI;
using KIP_POST_APP.Models.KIP;
using KIP_POST_APP.Models.KIP.Helpers;
using KIP_POST_APP.Services;

namespace KIP_POST_APP.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP schedule by group model from the KhPI schedule by group.
    /// </summary>
    public class ScheduleByGroup_KHPIToListOfSchedule_KIPConverter : ITypeConverter<ScheduleByGroup_KHPI, List<StudentSchedule>>
    {
        /// <summary>
        /// Convert model of schedule by group from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of schedule by group of model schedule by group KIP.
        /// </returns>
        /// <param name="source">Model of schedule by group KHPI.</param>
        /// <param name = "destination">Model of schedule by group KIP.</param>
        /// <param name= "context">The context. </param>
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
                var week = KIP_POST_APPHostedService.Week;

                var subjectListMonday = new List<string>(6)
                {
                    source.Monday.Para1.Name,
                    source.Monday.Para2.Name,
                    source.Monday.Para3.Name,
                    source.Monday.Para4.Name,
                    source.Monday.Para5.Name,
                    source.Monday.Para6.Name,
                };

                bool exists = false;
                foreach (var lesson in subjectListMonday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryListMonday = new List<string>(6)
                    {
                        source.Monday.Para1.Aud,
                        source.Monday.Para2.Aud,
                        source.Monday.Para3.Aud,
                        source.Monday.Para4.Aud,
                        source.Monday.Para5.Aud,
                        source.Monday.Para6.Aud,
                    };

                    var typeListMonday = new List<string>(6)
                    {
                        source.Monday.Para1.Vid,
                        source.Monday.Para2.Vid,
                        source.Monday.Para3.Vid,
                        source.Monday.Para4.Vid,
                        source.Monday.Para5.Vid,
                        source.Monday.Para6.Vid,
                    };

                    var profListMonday = new List<string>(6)
                    {
                        source.Monday.Para1.Prepod,
                        source.Monday.Para2.Prepod,
                        source.Monday.Para3.Prepod,
                        source.Monday.Para4.Prepod,
                        source.Monday.Para5.Prepod,
                        source.Monday.Para6.Prepod,
                    };

                    var profListMondayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < profListMonday.Count; i++)
                    {
                        if (profListMonday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (profListMonday[i].Contains(prof.ProfSurname))
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
                        for (int i = 0; i < auditoryListMonday.Count; i++)
                        {
                            if (auditoryListMonday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryListMonday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListMonday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryListMonday[i]))
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

                    for (int i = 0; i < subjectListMonday.Count; i++)
                    {
                        if (subjectListMonday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Monday,
                                Week = week,
                                ProfID = profListMondayDestination[i],
                                SubjectName = subjectListMonday[i],
                                AudienceID = audienceListMonday[i],
                                BuildingID = buildingListMonday[i],
                                Type = typeListMonday[i],
                            });
                        }
                    }
                }

                var subjectListTuesday = new List<string>(6)
                {
                    source.Tuesday.Para1.Name,
                    source.Tuesday.Para2.Name,
                    source.Tuesday.Para3.Name,
                    source.Tuesday.Para4.Name,
                    source.Tuesday.Para5.Name,
                    source.Tuesday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryListTuesday = new List<string>(6)
                    {
                        source.Tuesday.Para1.Aud,
                        source.Tuesday.Para2.Aud,
                        source.Tuesday.Para3.Aud,
                        source.Tuesday.Para4.Aud,
                        source.Tuesday.Para5.Aud,
                        source.Tuesday.Para6.Aud,
                    };

                    var typeListTuesday = new List<string>(6)
                    {
                        source.Tuesday.Para1.Vid,
                        source.Tuesday.Para2.Vid,
                        source.Tuesday.Para3.Vid,
                        source.Tuesday.Para4.Vid,
                        source.Tuesday.Para5.Vid,
                        source.Tuesday.Para6.Vid,
                    };

                    var profListTuesday = new List<string>(6)
                    {
                    source.Tuesday.Para1.Prepod,
                    source.Tuesday.Para2.Prepod,
                    source.Tuesday.Para3.Prepod,
                    source.Tuesday.Para4.Prepod,
                    source.Tuesday.Para5.Prepod,
                    source.Tuesday.Para6.Prepod,
                    };

                    var profListTuesdayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < profListTuesday.Count; i++)
                    {
                        if (profListTuesday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (profListTuesday[i].Contains(prof.ProfSurname))
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
                        for (int i = 0; i < auditoryListTuesday.Count; i++)
                        {
                            if (auditoryListTuesday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryListTuesday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListTuesday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryListTuesday[i]))
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

                    for (int i = 0; i < subjectListTuesday.Count; i++)
                    {
                        if (subjectListTuesday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Tuesday,
                                Week = week,
                                ProfID = profListTuesdayDestination[i],
                                SubjectName = subjectListTuesday[i],
                                AudienceID = audienceListTuesday[i],
                                BuildingID = buildingListTuesday[i],
                                Type = typeListTuesday[i],
                            });
                        }
                    }
                }

                var subjectListWednesday = new List<string>(6)
                {
                    source.Wednesday.Para1.Name,
                    source.Wednesday.Para2.Name,
                    source.Wednesday.Para3.Name,
                    source.Wednesday.Para4.Name,
                    source.Wednesday.Para5.Name,
                    source.Wednesday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryListWednesday = new List<string>(6)
                    {
                        source.Wednesday.Para1.Aud,
                        source.Wednesday.Para2.Aud,
                        source.Wednesday.Para3.Aud,
                        source.Wednesday.Para4.Aud,
                        source.Wednesday.Para5.Aud,
                        source.Wednesday.Para6.Aud,
                    };

                    var typeListWednesday = new List<string>(6)
                    {
                    source.Wednesday.Para1.Vid,
                    source.Wednesday.Para2.Vid,
                    source.Wednesday.Para3.Vid,
                    source.Wednesday.Para4.Vid,
                    source.Wednesday.Para5.Vid,
                    source.Wednesday.Para6.Vid,
                    };

                    var profListWednesday = new List<string>(6)
                    {
                    source.Wednesday.Para1.Prepod,
                    source.Wednesday.Para2.Prepod,
                    source.Wednesday.Para3.Prepod,
                    source.Wednesday.Para4.Prepod,
                    source.Wednesday.Para5.Prepod,
                    source.Wednesday.Para6.Prepod,
                    };

                    var profListWednesdayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < profListWednesday.Count; i++)
                    {
                        if (profListWednesday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (profListWednesday[i].Contains(prof.ProfSurname))
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
                        for (int i = 0; i < auditoryListWednesday.Count; i++)
                        {
                            if (auditoryListWednesday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryListWednesday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListWednesday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryListWednesday[i]))
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

                    for (int i = 0; i < subjectListWednesday.Count; i++)
                    {
                        if (subjectListWednesday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Wednesday,
                                Week = week,
                                ProfID = profListWednesdayDestination[i],
                                SubjectName = subjectListWednesday[i],
                                AudienceID = audienceListWednesday[i],
                                BuildingID = buildingListWednesday[i],
                                Type = typeListWednesday[i],
                            });
                        }
                    }
                }

                var subjectListThursday = new List<string>(6)
                {
                    source.Thursday.Para1.Name,
                    source.Thursday.Para2.Name,
                    source.Thursday.Para3.Name,
                    source.Thursday.Para4.Name,
                    source.Thursday.Para5.Name,
                    source.Thursday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryListThursday = new List<string>(6)
                    {
                        source.Thursday.Para1.Aud,
                        source.Thursday.Para2.Aud,
                        source.Thursday.Para3.Aud,
                        source.Thursday.Para4.Aud,
                        source.Thursday.Para5.Aud,
                        source.Thursday.Para6.Aud,
                    };

                    var typeListThursday = new List<string>(6)
                    {
                    source.Thursday.Para1.Vid,
                    source.Thursday.Para2.Vid,
                    source.Thursday.Para3.Vid,
                    source.Thursday.Para4.Vid,
                    source.Thursday.Para5.Vid,
                    source.Thursday.Para6.Vid,
                    };

                    var profListThursday = new List<string>(6)
                    {
                    source.Thursday.Para1.Prepod,
                    source.Thursday.Para2.Prepod,
                    source.Thursday.Para3.Prepod,
                    source.Thursday.Para4.Prepod,
                    source.Thursday.Para5.Prepod,
                    source.Thursday.Para6.Prepod,
                    };

                    var profListThursdayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < profListThursday.Count; i++)
                    {
                        if (profListThursday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (profListThursday[i].Contains(prof.ProfSurname))
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
                        for (int i = 0; i < auditoryListThursday.Count; i++)
                        {
                            if (auditoryListThursday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryListThursday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListThursday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryListThursday[i]))
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

                    for (int i = 0; i < subjectListThursday.Count; i++)
                    {
                        if (subjectListThursday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Thursday,
                                Week = week,
                                ProfID = profListThursdayDestination[i],
                                SubjectName = subjectListThursday[i],
                                AudienceID = audienceListThursday[i],
                                BuildingID = buildingListThursday[i],
                                Type = typeListThursday[i],
                            });
                        }
                    }
                }

                var subjectListFriday = new List<string>(6)
                {
                    source.Wednesday.Para1.Name,
                    source.Wednesday.Para2.Name,
                    source.Wednesday.Para3.Name,
                    source.Wednesday.Para4.Name,
                    source.Wednesday.Para5.Name,
                    source.Wednesday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectListTuesday)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryListFriday = new List<string>(6)
                    {
                        source.Friday.Para1.Aud,
                        source.Friday.Para2.Aud,
                        source.Friday.Para3.Aud,
                        source.Friday.Para4.Aud,
                        source.Friday.Para5.Aud,
                        source.Friday.Para6.Aud,
                    };

                    var typeListFriday = new List<string>(6)
                    {
                        source.Friday.Para1.Vid,
                        source.Friday.Para2.Vid,
                        source.Friday.Para3.Vid,
                        source.Friday.Para4.Vid,
                        source.Friday.Para5.Vid,
                        source.Friday.Para6.Vid,
                    };

                    var profListFriday = new List<string>(6)
                    {
                        source.Friday.Para1.Prepod,
                        source.Friday.Para2.Prepod,
                        source.Friday.Para3.Prepod,
                        source.Friday.Para4.Prepod,
                        source.Friday.Para5.Prepod,
                        source.Friday.Para6.Prepod,
                    };

                    var profListFridayDestination = new List<int?>() { null, null, null, null, null, null };

                    for (int i = 0; i < profListFriday.Count; i++)
                    {
                        if (profListFriday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MappedDataToKIPDB.ProfList)
                        {
                            if (profListFriday[i].Contains(prof.ProfSurname))
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
                        for (int i = 0; i < auditoryListFriday.Count; i++)
                        {
                            if (auditoryListFriday[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryListFriday[i].Contains(building.BuildingShortName))
                                {
                                    buildingListFriday[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryListFriday[i]))
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

                    for (int i = 0; i < subjectListFriday.Count; i++)
                    {
                        if (subjectListFriday[i] != string.Empty)
                        {
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Friday,
                                Week = week,
                                ProfID = profListFridayDestination[i],
                                SubjectName = subjectListFriday[i],
                                AudienceID = audienceListFriday[i],
                                BuildingID = buildingListFriday[i],
                                Type = typeListFriday[i],
                            });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            if (obj.Count == 0)
            {
                return null;
            }

            return obj;
        }
    }
}
