using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP;
using System;
using System.Collections.Generic;

namespace KIP_POST_APP.Services
{
    public static class PostData
    {
        /*
        public static void PostDataToDB(ServerContext context, 
            (List<Faculty> facultyList, List<Group> groupList, List<Cathedra> cathedraList, List<Building> buildingList,
             List<Audience> audienceList, List<Prof> profList, List<StudentSchedule> studentScheduleList, 
             List<ProfSchedule> profScheduleList) DataList)*/
        public static void PostDataToDB(ServerContext context,
            (List<Faculty> facultyList, List<Group> groupList, List<Cathedra> cathedraList, List<Building> buildingList,
             List<Audience> audienceList, List<Prof> profList) DataList)
        {
            SendFacultyDataToDB(context, DataList.facultyList);
            SendCathedraDataToDB(context, DataList.cathedraList);
            SendGroupDataToDB(context, DataList.groupList);
            SendBuildingDataToDB(context, DataList.buildingList);
            SendAudienceDataToDB(context, DataList.audienceList);
            SendProfDataToDB(context, DataList.profList);
            //SendStudentScheduleDataToDB(context, DataList.studentScheduleList);
            //SendProfScheduleDataToDB(context, DataList.profScheduleList);

            context.SaveChanges();
        }

        public static void SendFacultyDataToDB(ServerContext context, List<Faculty> objects)
        {
            foreach (var obj in objects)
            {
                context.Faculty.Add(obj);
            }
        }

        public static void SendGroupDataToDB(ServerContext context, List<Group> objects)
        {
            foreach (var obj in objects)
            {
                context.Group.Add(obj);
            }
        }

        public static void SendCathedraDataToDB(ServerContext context, List<Cathedra> objects)
        {
            foreach (var obj in objects)
            {
                context.Cathedra.Add(obj);
            }
        }

        public static void SendBuildingDataToDB(ServerContext context, List<Building> objects)
        {
            foreach (var obj in objects)
            {
                context.Building.Add(obj);
            }
        }

        public static void SendAudienceDataToDB(ServerContext context, List<Audience> objects)
        {
            foreach (var obj in objects)
            {
                context.Audience.Add(obj);
            }
        }

        public static void SendProfDataToDB(ServerContext context, List<Prof> objects)
        {
            foreach (var obj in objects)
            {
                context.Prof.Add(obj);
            }
        }

        /*
        public static void SendStudentScheduleDataToDB(ServerContext context, List<StudentSchedule> objects)
        {
            foreach (var obj in objects)
            {
                context.StudentSchedule.Add(obj);
            }
        }

        public static void SendProfScheduleDataToDB(ServerContext context, List<ProfSchedule> objects)
        {
            foreach (var obj in objects)
            {
                context.ProfSchedule.Add(obj);
            }
        }
        */
    }
}
