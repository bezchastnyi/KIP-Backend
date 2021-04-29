// <copyright file="ScheduleByProfKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
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
    /// Building of the KIP schedule by teachers model from the KhPI schedule by teachers.
    /// </summary>
    public class ScheduleByProfKHPIToKIPConverter : ITypeConverter<ScheduleByProfKHPI, List<ProfSchedule>>
    {
        /// <summary>
        /// Convert model of schedule by teachers from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of schedule by teachers of model schedule by teachers KIP.
        /// </returns>
        /// <param name="source">Model of schedule by teachers KHPI.</param>
        /// <param name = "destination">Model of schedule by teachers KIP.</param>
        /// <param name= "context">The context. </param>
        public List<ProfSchedule> Convert(ScheduleByProfKHPI source, List<ProfSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var obj = new List<ProfSchedule>();

            if (MappedDataToKIPDB.GroupList == null)
            {
                return null;
            }

            try
            {
                var week = KIP_POST_APPHostedService.Week;

                // Monday
                var subjectListMonday = new List<string>(6)
                {
                    source.Monday.Para1.Name,
                    source.Monday.Para2.Name,
                    source.Monday.Para3.Name,
                    source.Monday.Para4.Name,
                    source.Monday.Para5.Name,
                    source.Monday.Para6.Name,
                };

                var exists = false;
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
                        source.Monday.Para1.vid,
                        source.Monday.Para2.vid,
                        source.Monday.Para3.vid,
                        source.Monday.Para4.vid,
                        source.Monday.Para5.vid,
                        source.Monday.Para6.vid,
                    };

                    var groupListMonday = new List<string>(6)
                    {
                        source.Monday.Para1.Prepod,
                        source.Monday.Para2.Prepod,
                        source.Monday.Para3.Prepod,
                        source.Monday.Para4.Prepod,
                        source.Monday.Para5.Prepod,
                        source.Monday.Para6.Prepod,
                    };

                    var groupListMondayDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    for (var i = 0; i < groupListMonday.Count; i++)
                    {
                        if (groupListMonday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupListMonday[i].Contains(group.GroupName))
                            {
                                groupListMondayDestination[i].Add(group.GroupID);
                            }
                        }
                    }

                    var buildingListMonday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListMonday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryListMonday.Count; i++)
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

                    for (var i = 0; i < subjectListMonday.Count; i++)
                    {
                        if (subjectListMonday[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Monday,
                                Week = week,
                                GroupID = groupListMondayDestination[i],
                                SubjectName = subjectListMonday[i],
                                AudienceID = audienceListMonday[i],
                                BuildingID = buildingListMonday[i],
                                Type = typeListMonday[i],
                                Number = i,
                            });
                        }
                    }
                }

                // Tuesday
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
                        source.Tuesday.Para1.vid,
                        source.Tuesday.Para2.vid,
                        source.Tuesday.Para3.vid,
                        source.Tuesday.Para4.vid,
                        source.Tuesday.Para5.vid,
                        source.Tuesday.Para6.vid,
                    };

                    var groupListTuesday = new List<string>(6)
                    {
                        source.Tuesday.Para1.Prepod,
                        source.Tuesday.Para2.Prepod,
                        source.Tuesday.Para3.Prepod,
                        source.Tuesday.Para4.Prepod,
                        source.Tuesday.Para5.Prepod,
                        source.Tuesday.Para6.Prepod,
                    };

                    var groupListTuesdayDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    for (var i = 0; i < groupListTuesday.Count; i++)
                    {
                        if (groupListTuesday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupListTuesday[i].Contains(group.GroupName))
                            {
                                groupListTuesdayDestination[i].Add(group.GroupID);
                            }
                        }
                    }

                    var buildingListTuesday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListTuesday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryListTuesday.Count; i++)
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

                    for (var i = 0; i < subjectListTuesday.Count; i++)
                    {
                        if (subjectListTuesday[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Tuesday,
                                Week = week,
                                GroupID = groupListTuesdayDestination[i],
                                SubjectName = subjectListTuesday[i],
                                AudienceID = audienceListTuesday[i],
                                BuildingID = buildingListTuesday[i],
                                Type = typeListTuesday[i],
                                Number = i,
                            });
                        }
                    }
                }

                // Wednesday
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
                foreach (var lesson in subjectListWednesday)
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
                        source.Wednesday.Para1.vid,
                        source.Wednesday.Para2.vid,
                        source.Wednesday.Para3.vid,
                        source.Wednesday.Para4.vid,
                        source.Wednesday.Para5.vid,
                        source.Wednesday.Para6.vid,
                    };

                    var groupListWednesday = new List<string>(6)
                    {
                        source.Wednesday.Para1.Prepod,
                        source.Wednesday.Para2.Prepod,
                        source.Wednesday.Para3.Prepod,
                        source.Wednesday.Para4.Prepod,
                        source.Wednesday.Para5.Prepod,
                        source.Wednesday.Para6.Prepod,
                    };

                    var groupListWednesdayDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    for (var i = 0; i < groupListWednesday.Count; i++)
                    {
                        if (groupListWednesday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupListWednesday[i].Contains(group.GroupName))
                            {
                                groupListWednesdayDestination[i].Add(group.GroupID);
                            }
                        }
                    }

                    var buildingListWednesday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListWednesday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryListWednesday.Count; i++)
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

                    for (var i = 0; i < subjectListWednesday.Count; i++)
                    {
                        if (subjectListWednesday[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Wednesday,
                                Week = week,
                                GroupID = groupListWednesdayDestination[i],
                                SubjectName = subjectListWednesday[i],
                                AudienceID = audienceListWednesday[i],
                                BuildingID = buildingListWednesday[i],
                                Type = typeListWednesday[i],
                                Number = i,
                            });
                        }
                    }
                }

                // Thursday
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
                foreach (var lesson in subjectListThursday)
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
                        source.Thursday.Para1.vid,
                        source.Thursday.Para2.vid,
                        source.Thursday.Para3.vid,
                        source.Thursday.Para4.vid,
                        source.Thursday.Para5.vid,
                        source.Thursday.Para6.vid,
                    };

                    var groupListThursday = new List<string>(6)
                    {
                        source.Thursday.Para1.Prepod,
                        source.Thursday.Para2.Prepod,
                        source.Thursday.Para3.Prepod,
                        source.Thursday.Para4.Prepod,
                        source.Thursday.Para5.Prepod,
                        source.Thursday.Para6.Prepod,
                    };

                    var groupListThursdayDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    for (var i = 0; i < groupListThursday.Count; i++)
                    {
                        if (groupListThursday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupListThursday[i].Contains(group.GroupName))
                            {
                                groupListThursdayDestination[i].Add(group.GroupID);
                            }
                        }
                    }

                    var buildingListThursday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListThursday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryListThursday.Count; i++)
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

                    for (var i = 0; i < subjectListThursday.Count; i++)
                    {
                        if (subjectListThursday[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Thursday,
                                Week = week,
                                GroupID = groupListThursdayDestination[i],
                                SubjectName = subjectListThursday[i],
                                AudienceID = audienceListThursday[i],
                                BuildingID = buildingListThursday[i],
                                Type = typeListThursday[i],
                                Number = i,
                            });
                        }
                    }
                }

                // Friday
                var subjectListFriday = new List<string>(6)
                {
                    source.Friday.Para1.Name,
                    source.Friday.Para2.Name,
                    source.Friday.Para3.Name,
                    source.Friday.Para4.Name,
                    source.Friday.Para5.Name,
                    source.Friday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectListFriday)
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
                        source.Friday.Para1.vid,
                        source.Friday.Para2.vid,
                        source.Friday.Para3.vid,
                        source.Friday.Para4.vid,
                        source.Friday.Para5.vid,
                        source.Friday.Para6.vid,
                    };

                    var groupListFriday = new List<string>(6)
                    {
                        source.Friday.Para1.Prepod,
                        source.Friday.Para2.Prepod,
                        source.Friday.Para3.Prepod,
                        source.Friday.Para4.Prepod,
                        source.Friday.Para5.Prepod,
                        source.Friday.Para6.Prepod,
                    };

                    var groupListFridayDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    for (var i = 0; i < groupListFriday.Count; i++)
                    {
                        if (groupListFriday[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupListFriday[i].Contains(group.GroupName))
                            {
                                groupListFridayDestination[i].Add(group.GroupID);
                            }
                        }
                    }

                    var buildingListFriday = new List<int?>() { null, null, null, null, null, null };
                    var audienceListFriday = new List<int?>() { null, null, null, null, null, null };
                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryListFriday.Count; i++)
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

                    for (var i = 0; i < subjectListFriday.Count; i++)
                    {
                        if (subjectListFriday[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Friday,
                                Week = week,
                                GroupID = groupListFridayDestination[i],
                                SubjectName = subjectListFriday[i],
                                AudienceID = audienceListFriday[i],
                                BuildingID = buildingListFriday[i],
                                Type = typeListFriday[i],
                                Number = i,
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
