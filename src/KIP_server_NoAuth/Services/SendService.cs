using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIP_Backend.Models.NoAuth;
using KIP_server_NoAuth.DB;

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
        public static async Task SendMainStuffToDbAsync(
            NoAuthDbContext context,
            (HashSet<Faculty> facultyList,
                HashSet<Group> groupList,
                HashSet<Cathedra> cathedraList,
                HashSet<Building> buildingList,
                HashSet<Audience> audienceList,
                HashSet<Prof> profList) dataList)
        {
            await SendCollectionOfDataToDbAsync(context, dataList.facultyList);
            await SendCollectionOfDataToDbAsync(context, dataList.cathedraList);
            await SendCollectionOfDataToDbAsync(context, dataList.groupList);
            await SendCollectionOfDataToDbAsync(context, dataList.buildingList);
            await SendCollectionOfDataToDbAsync(context, dataList.audienceList);
            await SendCollectionOfDataToDbAsync(context, dataList.profList);
        }

        /// <summary>
        /// Publish data to database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dataList">The list of data. </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendStudentScheduleToDbAsync(
            NoAuthDbContext context, (HashSet<StudentSchedule> studentScheduleList, HashSet<StudentSchedule> studentSchedule2List) dataList)
        {
            var (studentScheduleList, studentSchedule2List) = dataList;
            await SendCollectionOfDataToDbAsync(context, studentScheduleList);
            await SendCollectionOfDataToDbAsync(context, studentSchedule2List);
        }

        /// <summary>
        /// Publish data to database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dataList">The list of data. </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendProfScheduleToDbAsync(
            NoAuthDbContext context, (HashSet<ProfSchedule> profScheduleList, HashSet<ProfSchedule> profSchedule2List) dataList)
        {
            var (profScheduleList, profSchedule2List) = dataList;
            await SendCollectionOfDataToDbAsync(context, profScheduleList);
            await SendCollectionOfDataToDbAsync(context, profSchedule2List);
        }

        /// <summary>
        /// Publish data to database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dataList">The list of data. </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendAudienceScheduleToDbAsync(
            NoAuthDbContext context, (HashSet<AudienceSchedule> AudienceScheduleList, HashSet<AudienceSchedule> AudienceSchedule2List) dataList)
        {
            var (audienceScheduleList, audienceSchedule2List) = dataList;
            await SendCollectionOfDataToDbAsync(context, audienceScheduleList);
            await SendCollectionOfDataToDbAsync(context, audienceSchedule2List);
        }

        /*
        /// <summary>
        /// Publish data to database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dataList">The list of data. </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendDataToDbAsync(
            NoAuthDbContext context,
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
            await SendCollectionOfDataToDbAsync(context, dataList.facultyList);
            await SendCollectionOfDataToDbAsync(context, dataList.cathedraList);
            await SendCollectionOfDataToDbAsync(context, dataList.groupList);
            await SendCollectionOfDataToDbAsync(context, dataList.buildingList);
            await SendCollectionOfDataToDbAsync(context, dataList.audienceList);
            await SendCollectionOfDataToDbAsync(context, dataList.profList);
            await SendCollectionOfDataToDbAsync(context, dataList.studentScheduleList);
            await SendCollectionOfDataToDbAsync(context, dataList.studentSchedule2List);
            await SendCollectionOfDataToDbAsync(context, dataList.profScheduleList);
            await SendCollectionOfDataToDbAsync(context, dataList.profSchedule2List);
            await SendCollectionOfDataToDbAsync(context, dataList.AudienceScheduleList);
            await SendCollectionOfDataToDbAsync(context, dataList.AudienceSchedule2List);
        }*/

        /// <summary>
        /// Sending collection of data to the database.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="objects">The objects.</param>
        /// <returns>A task.<see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendCollectionOfDataToDbAsync<T>(NoAuthDbContext context, IEnumerable<T> objects)
        {
            if (typeof(T) == typeof(Faculty))
            {
                await context.Faculty.AddRangeAsync(objects as IEnumerable<Faculty> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(Group))
            {
                await context.Group.AddRangeAsync(objects as IEnumerable<Group> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(Cathedra))
            {
                await context.Cathedra.AddRangeAsync(objects as IEnumerable<Cathedra> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(Building))
            {
                await context.Building.AddRangeAsync(objects as IEnumerable<Building> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(Audience))
            {
                await context.Audience.AddRangeAsync(objects as IEnumerable<Audience> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(Prof))
            {
                await context.Prof.AddRangeAsync(objects as IEnumerable<Prof> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(StudentSchedule))
            {
                await context.StudentSchedule.AddRangeAsync(objects as IEnumerable<StudentSchedule> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(ProfSchedule))
            {
                await context.ProfSchedule.AddRangeAsync(objects as IEnumerable<ProfSchedule> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            if (typeof(T) == typeof(AudienceSchedule))
            {
                await context.AudienceSchedule.AddRangeAsync(objects as IEnumerable<AudienceSchedule> ?? throw new InvalidOperationException(
                    $"Action: 'Send collection of data to Db' typeparam is not valid ({typeof(T)})"));
            }

            await context.SaveChangesAsync();
        }
    }
}
