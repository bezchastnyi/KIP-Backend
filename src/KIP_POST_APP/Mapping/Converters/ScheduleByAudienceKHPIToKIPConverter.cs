// <copyright file="ScheduleByAudienceKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ScheduleByAudienceKHPIToKIPConverter :
        ITypeConverter<ScheduleByAudienceKHPI, List<AudienceSchedule>>
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
        public List<AudienceSchedule> Convert(
            ScheduleByAudienceKHPI source, List<AudienceSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var exists = false;
            var obj = new List<AudienceSchedule>();
            var week = KIP_POST_APPHostedService.Week;

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
                    var typeList = new List<string>(6)
                    {
                        source.Monday.Para1.Aud,
                        source.Monday.Para2.Aud,
                        source.Monday.Para3.Aud,
                        source.Monday.Para4.Aud,
                        source.Monday.Para5.Aud,
                        source.Monday.Para6.Aud,
                    };

                    var profList = new List<string>(6)
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

                    var profListDestination = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
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
                        if (string.IsNullOrEmpty(groupList[i]) ||
                            string.IsNullOrWhiteSpace(groupList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.GroupList != null)
                        {
                            foreach (var group in MappedDataToKIPDB.GroupList)
                            {
                                if (groupList[i].Contains(group.GroupName))
                                {
                                    groupListDestination[i].Add(group.GroupID);
                                    groupListNamesDestination[i] += group.GroupName + " ";
                                }
                            }
                        }
                    }

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(profList[i]) ||
                            string.IsNullOrWhiteSpace(profList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.ProfList != null)
                        {
                            var prof = MappedDataToKIPDB.ProfList.FirstOrDefault(
                            prof => profList[i].Contains(prof.ProfSurname));

                            if (prof != null)
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new AudienceSchedule
                            {
                                Day = Day.Monday,
                                Week = week,
                                SubjectName = subjectList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupID = groupListDestination[i],
                                GroupNames = groupListNamesDestination[i],
                                ProfID = profListDestination[i].id,
                                ProfName = profListDestination[i].name,
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
                    var typeList = new List<string>(6)
                    {
                        source.Tuesday.Para1.Aud,
                        source.Tuesday.Para2.Aud,
                        source.Tuesday.Para3.Aud,
                        source.Tuesday.Para4.Aud,
                        source.Tuesday.Para5.Aud,
                        source.Tuesday.Para6.Aud,
                    };

                    var profList = new List<string>(6)
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

                    var profListDestination = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
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
                        if (string.IsNullOrEmpty(groupList[i]) ||
                            string.IsNullOrWhiteSpace(groupList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.GroupList != null)
                        {
                            foreach (var group in MappedDataToKIPDB.GroupList)
                            {
                                if (groupList[i].Contains(group.GroupName))
                                {
                                    groupListDestination[i].Add(group.GroupID);
                                    groupListNamesDestination[i] += group.GroupName + " ";
                                }
                            }
                        }
                    }

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(profList[i]) ||
                            string.IsNullOrWhiteSpace(profList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.ProfList != null)
                        {
                            var prof = MappedDataToKIPDB.ProfList.FirstOrDefault(
                            prof => profList[i].Contains(prof.ProfSurname));

                            if (prof != null)
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new AudienceSchedule
                            {
                                Day = Day.Tuesday,
                                Week = week,
                                SubjectName = subjectList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupID = groupListDestination[i],
                                GroupNames = groupListNamesDestination[i],
                                ProfID = profListDestination[i].id,
                                ProfName = profListDestination[i].name,
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
                    var typeList = new List<string>(6)
                    {
                        source.Wednesday.Para1.Aud,
                        source.Wednesday.Para2.Aud,
                        source.Wednesday.Para3.Aud,
                        source.Wednesday.Para4.Aud,
                        source.Wednesday.Para5.Aud,
                        source.Wednesday.Para6.Aud,
                    };

                    var profList = new List<string>(6)
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

                    var profListDestination = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
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
                        if (string.IsNullOrEmpty(groupList[i]) ||
                            string.IsNullOrWhiteSpace(groupList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.GroupList != null)
                        {
                            foreach (var group in MappedDataToKIPDB.GroupList)
                            {
                                if (groupList[i].Contains(group.GroupName))
                                {
                                    groupListDestination[i].Add(group.GroupID);
                                    groupListNamesDestination[i] += group.GroupName + " ";
                                }
                            }
                        }
                    }

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(profList[i]) ||
                            string.IsNullOrWhiteSpace(profList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.ProfList != null)
                        {
                            var prof = MappedDataToKIPDB.ProfList.FirstOrDefault(
                            prof => profList[i].Contains(prof.ProfSurname));

                            if (prof != null)
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new AudienceSchedule
                            {
                                Day = Day.Wednesday,
                                Week = week,
                                SubjectName = subjectList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupID = groupListDestination[i],
                                GroupNames = groupListNamesDestination[i],
                                ProfID = profListDestination[i].id,
                                ProfName = profListDestination[i].name,
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
                    var typeList = new List<string>(6)
                    {
                        source.Thursday.Para1.Aud,
                        source.Thursday.Para2.Aud,
                        source.Thursday.Para3.Aud,
                        source.Thursday.Para4.Aud,
                        source.Thursday.Para5.Aud,
                        source.Thursday.Para6.Aud,
                    };

                    var profList = new List<string>(6)
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

                    var profListDestination = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
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
                        if (string.IsNullOrEmpty(groupList[i]) ||
                            string.IsNullOrWhiteSpace(groupList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.GroupList != null)
                        {
                            foreach (var group in MappedDataToKIPDB.GroupList)
                            {
                                if (groupList[i].Contains(group.GroupName))
                                {
                                    groupListDestination[i].Add(group.GroupID);
                                    groupListNamesDestination[i] += group.GroupName + " ";
                                }
                            }
                        }
                    }

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(profList[i]) ||
                            string.IsNullOrWhiteSpace(profList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.ProfList != null)
                        {
                            var prof = MappedDataToKIPDB.ProfList.FirstOrDefault(
                            prof => profList[i].Contains(prof.ProfSurname));

                            if (prof != null)
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new AudienceSchedule
                            {
                                Day = Day.Thursday,
                                Week = week,
                                SubjectName = subjectList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupID = groupListDestination[i],
                                GroupNames = groupListNamesDestination[i],
                                ProfID = profListDestination[i].id,
                                ProfName = profListDestination[i].name,
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
                    var typeList = new List<string>(6)
                    {
                        source.Friday.Para1.Aud,
                        source.Friday.Para2.Aud,
                        source.Friday.Para3.Aud,
                        source.Friday.Para4.Aud,
                        source.Friday.Para5.Aud,
                        source.Friday.Para6.Aud,
                    };

                    var profList = new List<string>(6)
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

                    var profListDestination = new List<(int? id, string name)>()
                    {
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
                        (null, string.Empty),
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
                        if (string.IsNullOrEmpty(groupList[i]) ||
                            string.IsNullOrWhiteSpace(groupList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.GroupList != null)
                        {
                            foreach (var group in MappedDataToKIPDB.GroupList)
                            {
                                if (groupList[i].Contains(group.GroupName))
                                {
                                    groupListDestination[i].Add(group.GroupID);
                                    groupListNamesDestination[i] += group.GroupName + " ";
                                }
                            }
                        }
                    }

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(profList[i]) ||
                            string.IsNullOrWhiteSpace(profList[i]))
                        {
                            continue;
                        }

                        if (MappedDataToKIPDB.ProfList != null)
                        {
                            var prof = MappedDataToKIPDB.ProfList.FirstOrDefault(
                            prof => profList[i].Contains(prof.ProfSurname));

                            if (prof != null)
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);
                            }
                        }
                    }

                    for (var i = 0; i < subjectList.Count; i++)
                    {
                        if (subjectList[i] != string.Empty)
                        {
                            obj.Add(new AudienceSchedule
                            {
                                Day = Day.Friday,
                                Week = week,
                                SubjectName = subjectList[i],
                                Type = typeList[i],
                                Number = i,
                                GroupID = groupListDestination[i],
                                GroupNames = groupListNamesDestination[i],
                                ProfID = profListDestination[i].id,
                                ProfName = profListDestination[i].name,
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
