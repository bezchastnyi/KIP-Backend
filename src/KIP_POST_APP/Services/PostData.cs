// <copyright file="PostData.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task PostDataToDBAsync(
            ServerContext context,
            (List<Faculty> facultyList,
            List<Group> groupList,
            List<Cathedra> cathedraList,
            List<Building> buildingList,
            List<Audience> audienceList,
            List<Prof> profList,
            List<StudentSchedule> studentScheduleList,
            List<StudentSchedule> studentSchedule2List,
            List<ProfSchedule> profScheduleList,
            List<ProfSchedule> profSchedule2List)
            dataList)
        {
            await SendFacultyDataToDB(context, dataList.facultyList);
            await SendCathedraDataToDB(context, dataList.cathedraList);
            await SendGroupDataToDBAsync(context, dataList.groupList);
            await SendBuildingDataToDB(context, dataList.buildingList);
            await SendAudienceDataToDB(context, dataList.audienceList);
            await SendProfDataToDB(context, dataList.profList);
            await SendStudentScheduleDataToDB(context, dataList.studentScheduleList);
            await SendStudentScheduleDataToDB(context, dataList.studentSchedule2List);
            await SendProfScheduleDataToDB(context, dataList.profScheduleList);
            await SendProfScheduleDataToDB(context, dataList.profSchedule2List);

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about faculties to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendFacultyDataToDB(ServerContext context, List<Faculty> objects)
        {
            foreach (var obj in objects)
            {
                await context.Faculty.AddAsync(obj);
            }
        }

        /// <summary>
        /// Sending data about groups to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendGroupDataToDBAsync(ServerContext context, List<Group> objects)
        {
            foreach (var obj in objects)
            {
                await context.Group.AddAsync(obj);
            }
        }

        /// <summary>
        /// Sending data about department to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendCathedraDataToDB(ServerContext context, List<Cathedra> objects)
        {
            foreach (var obj in objects)
            {
                await context.Cathedra.AddAsync(obj);
            }
        }

        /// <summary>
        /// Sending data about buildings to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendBuildingDataToDB(ServerContext context, List<Building> objects)
        {
            foreach (var obj in objects)
            {
                await context.Building.AddAsync(obj);
            }
        }

        /// <summary>
        /// Sending data about audiences to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendAudienceDataToDB(ServerContext context, List<Audience> objects)
        {
            foreach (var obj in objects)
            {
                await context.Audience.AddAsync(obj);
            }
        }

        /// <summary>
        /// Sending data about teachers to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendProfDataToDB(ServerContext context, List<Prof> objects)
        {
            foreach (var obj in objects)
            {
                await context.Prof.AddAsync(obj);
            }
        }

        /// <summary>
        /// Sending data about schedule of groups to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendStudentScheduleDataToDB(ServerContext context, List<StudentSchedule> objects)
        {
            foreach (var obj in objects)
            {
                await context.StudentSchedule.AddAsync(obj);
            }
        }

        /// <summary>
        /// Sending data about schedule of teachers to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name = "objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendProfScheduleDataToDB(ServerContext context, List<ProfSchedule> objects)
        {
            foreach (var obj in objects)
            {
                await context.ProfSchedule.AddAsync(obj);
            }
        }
    }
}
