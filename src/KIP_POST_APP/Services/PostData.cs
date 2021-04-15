// <copyright file="PostData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;

namespace KIP_POST_APP.Services
{
    /// <summary>
    /// Publish data.
    /// </summary>
    public static class PostData
    {
        /// <summary>
        /// Publish data to database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "dataList">The list of data. </param>
        public static void PostDataToDB(
            ServerContext context,
            (List<Faculty> facultyList, List<Group> groupList, List<Cathedra> cathedraList, List<Building> buildingList,
             List<Audience> audienceList, List<Prof> profList, List<StudentSchedule> studentScheduleList,
             List<StudentSchedule> studentSchedule2List, List<ProfSchedule> profScheduleList,
             List<ProfSchedule> profSchedule2List) dataList)
        {
            SendFacultyDataToDB(context, dataList.facultyList);
            SendCathedraDataToDB(context, dataList.cathedraList);
            SendGroupDataToDB(context, dataList.groupList);
            SendBuildingDataToDB(context, dataList.buildingList);
            SendAudienceDataToDB(context, dataList.audienceList);
            SendProfDataToDB(context, dataList.profList);
            SendStudentScheduleDataToDB(context, dataList.studentScheduleList);
            SendStudentScheduleDataToDB(context, dataList.studentSchedule2List);
            SendProfScheduleDataToDB(context, dataList.profScheduleList);
            SendProfScheduleDataToDB(context, dataList.profSchedule2List);

            context.SaveChanges();
        }

        /// <summary>
        /// Sending data about faculties to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendFacultyDataToDB(ServerContext context, List<Faculty> objects)
        {
            foreach (var obj in objects)
            {
                context.Faculty.Add(obj);
            }
        }

        /// <summary>
        /// Sending data about groups to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendGroupDataToDB(ServerContext context, List<Group> objects)
        {
            foreach (var obj in objects)
            {
                context.Group.Add(obj);
            }
        }

        /// <summary>
        /// Sending data about department to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendCathedraDataToDB(ServerContext context, List<Cathedra> objects)
        {
            foreach (var obj in objects)
            {
                context.Cathedra.Add(obj);
            }
        }

        /// <summary>
        /// Sending data about buildings to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendBuildingDataToDB(ServerContext context, List<Building> objects)
        {
            foreach (var obj in objects)
            {
                context.Building.Add(obj);
            }
        }

        /// <summary>
        /// Sending data about audiences to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendAudienceDataToDB(ServerContext context, List<Audience> objects)
        {
            foreach (var obj in objects)
            {
                context.Audience.Add(obj);
            }
        }

        /// <summary>
        /// Sending data about teachers to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendProfDataToDB(ServerContext context, List<Prof> objects)
        {
            foreach (var obj in objects)
            {
                context.Prof.Add(obj);
            }
        }

        /// <summary>
        /// Sending data about schedule of groups to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendStudentScheduleDataToDB(ServerContext context, List<StudentSchedule> objects)
        {
            foreach (var obj in objects)
            {
                context.StudentSchedule.Add(obj);
            }
        }

        /// <summary>
        /// Sending data about schedule of teachers to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        public static void SendProfScheduleDataToDB(ServerContext context, List<ProfSchedule> objects)
        {
            foreach (var obj in objects)
            {
                context.ProfSchedule.Add(obj);
            }
        }
    }
}
