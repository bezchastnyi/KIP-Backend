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

            var week = KIP_POST_APPHostedService.Week;
            var obj = new List<ProfSchedule>();
            var exists = false;

            try
            {
                // Monday
                var subjectList = new List<string>(6)
                {
                    source.Monday.Para1.Name,
                    source.Monday.Para2.Name,
                    source.Monday.Para3.Name,
                    source.Monday.Para4.Name,
                    source.Monday.Para5.Name,
                    source.Monday.Para6.Name,
                };

                foreach (var lesson in subjectList)
                {
                    if (!string.IsNullOrEmpty(lesson) &&
                        !string.IsNullOrWhiteSpace(lesson))
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryList = new List<string>(6)
                    {
                        source.Monday.Para1.Aud,
                        source.Monday.Para2.Aud,
                        source.Monday.Para3.Aud,
                        source.Monday.Para4.Aud,
                        source.Monday.Para5.Aud,
                        source.Monday.Para6.Aud,
                    };

                    var typeList = new List<string>(6)
                    {
                        source.Monday.Para1.vid,
                        source.Monday.Para2.vid,
                        source.Monday.Para3.vid,
                        source.Monday.Para4.vid,
                        source.Monday.Para5.vid,
                        source.Monday.Para6.vid,
                    };

                    var groupList = new List<string>(6)
                    {
                        source.Monday.Para1.Prepod,
                        source.Monday.Para2.Prepod,
                        source.Monday.Para3.Prepod,
                        source.Monday.Para4.Prepod,
                        source.Monday.Para5.Prepod,
                        source.Monday.Para6.Prepod,
                    };

                    var groupListDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    var groupListNamesDestination = new List<string>()
                    {
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                    };

                    for (var i = 0; i < groupList.Count; i++)
                    {
                        if (groupList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupList[i].Contains(group.GroupName))
                            {
                                groupListDestination[i].Add(group.GroupID);
                                groupListNamesDestination[i] += group.GroupName + " ";
                            }
                        }
                    }

                    var buildingList = new List<int?>() { null, null, null, null, null, null };
                    var audienceList = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                    };

                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryList[i]))
                                        {
                                            audienceList[i] = (audience.AudienceID, audience.AudienceName);
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Monday,
                                Week = week,
                                Number = i,
                                Type = typeList[i],
                                GroupID = groupListDestination[i],
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                GroupName = groupListNamesDestination,
                                AudienceName = audienceList[i].name,
                            });
                        }
                    }
                }

                // Tuesday
                subjectList = new List<string>(6)
                {
                    source.Tuesday.Para1.Name,
                    source.Tuesday.Para2.Name,
                    source.Tuesday.Para3.Name,
                    source.Tuesday.Para4.Name,
                    source.Tuesday.Para5.Name,
                    source.Tuesday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectList)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryList = new List<string>(6)
                    {
                        source.Tuesday.Para1.Aud,
                        source.Tuesday.Para2.Aud,
                        source.Tuesday.Para3.Aud,
                        source.Tuesday.Para4.Aud,
                        source.Tuesday.Para5.Aud,
                        source.Tuesday.Para6.Aud,
                    };

                    var typeList = new List<string>(6)
                    {
                        source.Tuesday.Para1.vid,
                        source.Tuesday.Para2.vid,
                        source.Tuesday.Para3.vid,
                        source.Tuesday.Para4.vid,
                        source.Tuesday.Para5.vid,
                        source.Tuesday.Para6.vid,
                    };

                    var groupList = new List<string>(6)
                    {
                        source.Tuesday.Para1.Prepod,
                        source.Tuesday.Para2.Prepod,
                        source.Tuesday.Para3.Prepod,
                        source.Tuesday.Para4.Prepod,
                        source.Tuesday.Para5.Prepod,
                        source.Tuesday.Para6.Prepod,
                    };

                    var groupListDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    var groupListNamesDestination = new List<string>()
                    {
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                    };

                    for (var i = 0; i < groupList.Count; i++)
                    {
                        if (groupList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupList[i].Contains(group.GroupName))
                            {
                                groupListDestination[i].Add(group.GroupID);
                                groupListNamesDestination[i] += group.GroupName + " ";
                            }
                        }
                    }

                    var buildingList = new List<int?>() { null, null, null, null, null, null };
                    var audienceList = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                    };

                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryList[i]))
                                        {
                                            audienceList[i] = (audience.AudienceID, audience.AudienceName);
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Tuesday,
                                Week = week,
                                GroupID = groupListDestination[i],
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupName = groupListNamesDestination,
                                AudienceName = audienceList[i].name,
                            });
                        }
                    }
                }

                // Wednesday
                subjectList = new List<string>(6)
                {
                    source.Wednesday.Para1.Name,
                    source.Wednesday.Para2.Name,
                    source.Wednesday.Para3.Name,
                    source.Wednesday.Para4.Name,
                    source.Wednesday.Para5.Name,
                    source.Wednesday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectList)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryList = new List<string>(6)
                    {
                        source.Wednesday.Para1.Aud,
                        source.Wednesday.Para2.Aud,
                        source.Wednesday.Para3.Aud,
                        source.Wednesday.Para4.Aud,
                        source.Wednesday.Para5.Aud,
                        source.Wednesday.Para6.Aud,
                    };

                    var typeList = new List<string>(6)
                    {
                        source.Wednesday.Para1.vid,
                        source.Wednesday.Para2.vid,
                        source.Wednesday.Para3.vid,
                        source.Wednesday.Para4.vid,
                        source.Wednesday.Para5.vid,
                        source.Wednesday.Para6.vid,
                    };

                    var groupList = new List<string>(6)
                    {
                        source.Wednesday.Para1.Prepod,
                        source.Wednesday.Para2.Prepod,
                        source.Wednesday.Para3.Prepod,
                        source.Wednesday.Para4.Prepod,
                        source.Wednesday.Para5.Prepod,
                        source.Wednesday.Para6.Prepod,
                    };

                    var groupListDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    var groupListNamesDestination = new List<string>()
                    {
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                    };

                    for (var i = 0; i < groupList.Count; i++)
                    {
                        if (groupList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupList[i].Contains(group.GroupName))
                            {
                                groupListDestination[i].Add(group.GroupID);
                                groupListNamesDestination[i] += group.GroupName + " ";
                            }
                        }
                    }

                    var buildingList = new List<int?>() { null, null, null, null, null, null };
                    var audienceList = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                    };

                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryList[i]))
                                        {
                                            audienceList[i] = (audience.AudienceID, audience.AudienceName);
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Wednesday,
                                Week = week,
                                GroupID = groupListDestination[i],
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupName = groupListNamesDestination,
                                AudienceName = audienceList[i].name,
                            });
                        }
                    }
                }

                // Thursday
                subjectList = new List<string>(6)
                {
                    source.Thursday.Para1.Name,
                    source.Thursday.Para2.Name,
                    source.Thursday.Para3.Name,
                    source.Thursday.Para4.Name,
                    source.Thursday.Para5.Name,
                    source.Thursday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectList)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryList = new List<string>(6)
                    {
                        source.Thursday.Para1.Aud,
                        source.Thursday.Para2.Aud,
                        source.Thursday.Para3.Aud,
                        source.Thursday.Para4.Aud,
                        source.Thursday.Para5.Aud,
                        source.Thursday.Para6.Aud,
                    };

                    var typeList = new List<string>(6)
                    {
                        source.Thursday.Para1.vid,
                        source.Thursday.Para2.vid,
                        source.Thursday.Para3.vid,
                        source.Thursday.Para4.vid,
                        source.Thursday.Para5.vid,
                        source.Thursday.Para6.vid,
                    };

                    var groupList = new List<string>(6)
                    {
                        source.Thursday.Para1.Prepod,
                        source.Thursday.Para2.Prepod,
                        source.Thursday.Para3.Prepod,
                        source.Thursday.Para4.Prepod,
                        source.Thursday.Para5.Prepod,
                        source.Thursday.Para6.Prepod,
                    };

                    var groupListDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    var groupListNamesDestination = new List<string>()
                    {
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                    };

                    for (var i = 0; i < groupList.Count; i++)
                    {
                        if (groupList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupList[i].Contains(group.GroupName))
                            {
                                groupListDestination[i].Add(group.GroupID);
                                groupListNamesDestination[i] += group.GroupName + " ";
                            }
                        }
                    }

                    var buildingList = new List<int?>() { null, null, null, null, null, null };
                    var audienceList = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                    };

                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryList[i]))
                                        {
                                            audienceList[i] = (audience.AudienceID, audience.AudienceName);
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Thursday,
                                Week = week,
                                GroupID = groupListDestination[i],
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupName = groupListNamesDestination,
                                AudienceName = audienceList[i].name,
                            });
                        }
                    }
                }

                // Friday
                subjectList = new List<string>(6)
                {
                    source.Friday.Para1.Name,
                    source.Friday.Para2.Name,
                    source.Friday.Para3.Name,
                    source.Friday.Para4.Name,
                    source.Friday.Para5.Name,
                    source.Friday.Para6.Name,
                };

                exists = false;
                foreach (var lesson in subjectList)
                {
                    if (lesson != string.Empty)
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    var auditoryList = new List<string>(6)
                    {
                        source.Friday.Para1.Aud,
                        source.Friday.Para2.Aud,
                        source.Friday.Para3.Aud,
                        source.Friday.Para4.Aud,
                        source.Friday.Para5.Aud,
                        source.Friday.Para6.Aud,
                    };

                    var typeList = new List<string>(6)
                    {
                        source.Friday.Para1.vid,
                        source.Friday.Para2.vid,
                        source.Friday.Para3.vid,
                        source.Friday.Para4.vid,
                        source.Friday.Para5.vid,
                        source.Friday.Para6.vid,
                    };

                    var groupList = new List<string>(6)
                    {
                        source.Friday.Para1.Prepod,
                        source.Friday.Para2.Prepod,
                        source.Friday.Para3.Prepod,
                        source.Friday.Para4.Prepod,
                        source.Friday.Para5.Prepod,
                        source.Friday.Para6.Prepod,
                    };

                    var groupListDestination = new List<List<int?>>()
                    {
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                        new List<int?>(),
                    };

                    var groupListNamesDestination = new List<string>()
                    {
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                    };

                    for (var i = 0; i < groupList.Count; i++)
                    {
                        if (groupList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var group in MappedDataToKIPDB.GroupList)
                        {
                            if (groupList[i].Contains(group.GroupName))
                            {
                                groupListDestination[i].Add(group.GroupID);
                                groupListNamesDestination[i] += group.GroupName + " ";
                            }
                        }
                    }

                    var buildingList = new List<int?>() { null, null, null, null, null, null };
                    var audienceList = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                    };

                    if (MappedDataToKIPDB.BuildingList != null && MappedDataToKIPDB.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MappedDataToKIPDB.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MappedDataToKIPDB.AudienceList)
                                    {
                                        if (audience.AudienceName.Contains(auditoryList[i]))
                                        {
                                            audienceList[i] = (audience.AudienceID, audience.AudienceName);
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new ProfSchedule
                            {
                                Day = Day.Friday,
                                Week = week,
                                GroupID = groupListDestination[i],
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupName = groupListNamesDestination,
                                AudienceName = audienceList[i].name,
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
