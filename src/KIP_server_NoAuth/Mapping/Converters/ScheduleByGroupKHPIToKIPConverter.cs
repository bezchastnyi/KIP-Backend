// <copyright file="ScheduleByGroupKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KIP_Backend.Models.KIP;
using KIP_Backend.Models.KIP.Helpers;
using KIP_server_NoAuth.Models.KhPI;
using KIP_server_NoAuth.Services;
using KIP_server_NoAuth.V1.Controllers;

namespace KIP_server_NoAuth.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP schedule by group model from the KhPI schedule by group.
    /// </summary>
    public class ScheduleByGroupKHPIToKIPConverter : ITypeConverter<ScheduleByGroupKHPI, List<StudentSchedule>>
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
        public List<StudentSchedule> Convert(ScheduleByGroupKHPI source, List<StudentSchedule> destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var exists = false;
            var obj = new List<StudentSchedule>();
            var week = DbUpdateController.Week;

            if (MapService.ProfList == null)
            {
                return null;
            }

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

                    var profList = new List<string>(6)
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

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(profList[i]) ||
                            string.IsNullOrWhiteSpace(profList[i]))
                        {
                            continue;
                        }

                        if (MapService.ProfList != null)
                        {
                            var prof = MapService.ProfList.FirstOrDefault(
                            prof => profList[i].Contains(prof.ProfSurname));

                            if (prof != null)
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);

                                HashSet<ProfSchedule> list = null;
                                if (week == Week.UnPaired)
                                {
                                    list = MapService.ProfScheduleList.
                                        Where(para => para.ProfID == prof.ProfID).ToHashSet();
                                }
                                else
                                {
                                    list = MapService.ProfSchedule2List.
                                        Where(para => para.ProfID == prof.ProfID).ToHashSet();
                                }

                                foreach (var lesson in list)
                                {
                                    if (lesson.Number == i &&
                                        lesson.Day == Day.Monday)
                                    {
                                        subjectList[i] = lesson.SubjectName;
                                        break;
                                    }
                                }
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

                    if (MapService.BuildingList != null &&
                        MapService.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MapService.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MapService.AudienceList)
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
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Monday,
                                Week = week,
                                ProfID = profListDestination[i].id,
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                ProfName = profListDestination[i].name,
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

                    var profList = new List<string>(6)
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

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(profList[i]) ||
                            string.IsNullOrWhiteSpace(profList[i]))
                        {
                            continue;
                        }

                        var prof = MapService.ProfList.FirstOrDefault(
                            prof => profList[i].Contains(prof.ProfSurname));

                        if (prof != default)
                        {
                            profListDestination[i] = (prof.ProfID, prof.ProfSurname);

                            HashSet<ProfSchedule> list = null;
                            if (week == Week.UnPaired)
                            {
                                list = MapService.ProfScheduleList.
                                    Where(para => para.ProfID == prof.ProfID).ToHashSet();
                            }
                            else
                            {
                                list = MapService.ProfSchedule2List.
                                    Where(para => para.ProfID == prof.ProfID).ToHashSet();
                            }

                            foreach (var lesson in list)
                            {
                                if (lesson.Number == i &&
                                    lesson.Day == Day.Tuesday)
                                {
                                    subjectList[i] = lesson.SubjectName;
                                    break;
                                }
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

                    if (MapService.BuildingList != null && MapService.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MapService.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MapService.AudienceList)
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
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Tuesday,
                                Week = week,
                                ProfID = profListDestination[i].id,
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                ProfName = profListDestination[i].name,
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

                    var profList = new List<string>(6)
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

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (profList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MapService.ProfList)
                        {
                            if (profList[i].Contains(prof.ProfSurname))
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);

                                foreach (
                                    var lesson in MapService.ProfScheduleList.
                                        Where(para => para.ProfID == prof.ProfID))
                                {
                                    if (
                                        lesson.Number == i &&
                                        lesson.Day == Day.Wednesday)
                                    {
                                        subjectList[i] = lesson.SubjectName;
                                        break;
                                    }
                                }

                                break;
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

                    if (MapService.BuildingList != null && MapService.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MapService.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MapService.AudienceList)
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
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Wednesday,
                                Week = week,
                                ProfID = profListDestination[i].id,
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                ProfName = profListDestination[i].name,
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

                    var profList = new List<string>(6)
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

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (profList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MapService.ProfList)
                        {
                            if (profList[i].Contains(prof.ProfSurname))
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);

                                foreach (
                                    var lesson in MapService.ProfScheduleList.
                                        Where(para => para.ProfID == prof.ProfID))
                                {
                                    if (
                                        lesson.Number == i &&
                                        lesson.Day == Day.Thursday)
                                    {
                                        subjectList[i] = lesson.SubjectName;
                                        break;
                                    }
                                }

                                break;
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

                    if (MapService.BuildingList != null && MapService.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MapService.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MapService.AudienceList)
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
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Thursday,
                                Week = week,
                                ProfID = profListDestination[i].id,
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                ProfName = profListDestination[i].name,
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

                    var profList = new List<string>(6)
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

                    for (var i = 0; i < profList.Count; i++)
                    {
                        if (profList[i] == string.Empty)
                        {
                            continue;
                        }

                        foreach (var prof in MapService.ProfList)
                        {
                            if (profList[i].Contains(prof.ProfSurname))
                            {
                                profListDestination[i] = (prof.ProfID, prof.ProfSurname);

                                foreach (
                                    var lesson in MapService.ProfScheduleList.
                                        Where(para => para.ProfID == prof.ProfID))
                                {
                                    if (
                                        lesson.Number == i &&
                                        lesson.Day == Day.Friday)
                                    {
                                        subjectList[i] = lesson.SubjectName;
                                        break;
                                    }
                                }

                                break;
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

                    if (MapService.BuildingList != null && MapService.AudienceList != null)
                    {
                        for (var i = 0; i < auditoryList.Count; i++)
                        {
                            if (auditoryList[i] == string.Empty)
                            {
                                continue;
                            }

                            foreach (var building in MapService.BuildingList)
                            {
                                if (auditoryList[i].Contains(building.BuildingShortName))
                                {
                                    buildingList[i] = building.BuildingID;
                                    foreach (var audience in MapService.AudienceList)
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
                            obj.Add(new StudentSchedule
                            {
                                Day = Day.Friday,
                                Week = week,
                                ProfID = profListDestination[i].id,
                                SubjectName = subjectList[i],
                                AudienceID = audienceList[i].id,
                                BuildingID = buildingList[i],
                                Type = typeList[i],
                                Number = i,
                                ProfName = profListDestination[i].name,
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
