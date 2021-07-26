// <copyright file="SendService.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using KIP_Backend.DB;
using KIP_Backend.Models.KIP;

namespace KIP_server_NoAuth.Services
{
    /// <summary>
    /// Publish data.
    /// </summary>
    public static class SendService
    {
        /// <summary>
        /// Publish data to database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dataList">The list of data. </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendDataToDbAsync(
            KIPDbContext context,
            (HashSet<Faculty> facultyList,
            HashSet<Group> groupList,
            HashSet<Cathedra> cathedraList,
            HashSet<Building> buildingList,
            HashSet<Audience> audienceList,
            HashSet<Prof> profList,
            HashSet<StudentSchedule> studentScheduleList,
            HashSet<StudentSchedule> studentSchedule2List,
            HashSet<ProfSchedule> profScheduleList,
            HashSet<ProfSchedule> profSchedule2List,
            HashSet<AudienceSchedule> AudienceScheduleList,
            HashSet<AudienceSchedule> AudienceSchedule2List) dataList)
        {
            await SendFacultyDataToDbAsync(context, dataList.facultyList);
            await SendCathedraDataToDbAsync(context, dataList.cathedraList);
            await SendGroupDataToDBAsync(context, dataList.groupList);
            await SendBuildingDataToDbAsync(context, dataList.buildingList);
            await SendAudienceDataToDbAsync(context, dataList.audienceList);
            await SendProfDataToDbAsync(context, dataList.profList);
            await SendStudentScheduleDataToDbAsync(context, dataList.studentScheduleList);
            await SendStudentScheduleDataToDbAsync(context, dataList.studentSchedule2List);
            await SendProfScheduleDataToDbAsync(context, dataList.profScheduleList);
            await SendProfScheduleDataToDbAsync(context, dataList.profSchedule2List);
            await SendAudienceScheduleDataToDbAsync(context, dataList.AudienceScheduleList);
            await SendAudienceScheduleDataToDbAsync(context, dataList.AudienceSchedule2List);
        }

        /// <summary>
        /// Sending data about faculties to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendFacultyDataToDbAsync(KIPDbContext context, HashSet<Faculty> objects)
        {
            await context.Faculty.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about groups to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendGroupDataToDBAsync(KIPDbContext context, HashSet<Group> objects)
        {
            await context.Group.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about department to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendCathedraDataToDbAsync(KIPDbContext context, HashSet<Cathedra> objects)
        {
            await context.Cathedra.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about buildings to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendBuildingDataToDbAsync(KIPDbContext context, HashSet<Building> objects)
        {
            await context.Building.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about audiences to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendAudienceDataToDbAsync(KIPDbContext context, HashSet<Audience> objects)
        {
            await context.Audience.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about teachers to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendProfDataToDbAsync(KIPDbContext context, HashSet<Prof> objects)
        {
            await context.Prof.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about schedule of groups to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendStudentScheduleDataToDbAsync(KIPDbContext context, HashSet<StudentSchedule> objects)
        {
            await context.StudentSchedule.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about schedule of teachers to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendProfScheduleDataToDbAsync(KIPDbContext context, HashSet<ProfSchedule> objects)
        {
            await context.ProfSchedule.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sending data about schedule of audience to the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendAudienceScheduleDataToDbAsync(KIPDbContext context, HashSet<AudienceSchedule> objects)
        {
            await context.AudienceSchedule.AddRangeAsync(objects);
            await context.SaveChangesAsync();
        }
    }
}
