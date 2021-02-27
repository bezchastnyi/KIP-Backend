using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace json_read
{

    class Program
    {

        private static void read_jason(string url)
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;

                try
                {
                    json_data = w.DownloadString(url);
                    Console.WriteLine(json_data);
                }
                catch (Exception) { }
            }
        }

        private static IEnumerable<T> download_serialized_json_data<T>(string url)
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;

                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }

                return JsonConvert.DeserializeObject<IEnumerable<T>>(json_data);
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            var url1 = "http://schedule.kpi.kharkov.ua/json/FacultyList";
            var url2 = "http://schedule.kpi.kharkov.ua/json/GroupByFacultyList/";
            var url3 = "http://schedule.kpi.kharkov.ua/json/Schedule/";


            read_jason(url3+"11144");


            /*
            var faculty_list = download_serialized_json_data<FacultyList>(url1);

            foreach (var i in faculty_list)
            {
                Console.WriteLine("[" + i.id + "]" + ": \"" + i.title + "\"");
            }
            Console.WriteLine("\n\n");


            var groupByFaculties = new List<IEnumerable<GroupByFaculty>>();

            foreach (var i in faculty_list)
            {
                var temp_url = url2 + i.id;
                var groups = download_serialized_json_data<GroupByFaculty>(temp_url);
                groupByFaculties.Add(groups);
            }


            foreach (var i in groupByFaculties)
            {
                foreach (var j in i)
                {
                    Console.WriteLine("[" + j.id + "]" + ": \"" + j.title + "\" " + j.course);
                }
                Console.WriteLine("\n---------\n");
            }
            Console.WriteLine("\n\n");
            */
        }

    }

}

/*
 * 
 * http://schedule.kpi.kharkov.ua/json/FacultyList
 * 
 * http://schedule.kpi.kharkov.ua/json/GroupByFacultyList/{fid}/
 * 
 * http://schedule.kpi.kharkov.ua/json/Schedule/{gid}/
 * 
 * http://schedule.kpi.kharkov.ua/json/Schedule2/{gid}/
 * 
 * 
 * 
 * 
 * http://schedule.kpi.kharkov.ua/JSON/DeptsByFacultyP/fid/                                 // cathedras
 * 
 * http://schedule.kpi.kharkov.ua/JSON/PrepodListByDeptP/dip/                               // profs
 * 
 * http://schedule.kpi.kharkov.ua/JSON/ScheduleP/pid/
 * 
 * http://schedule.kpi.kharkov.ua/JSON/Schedule2P/pid/
 * 
 * 
 * 
 * 
 * http://schedule.kpi.kharkov.ua/json/SearchGroups/поисковый запрос/
 * 
 * http://schedule.kpi.kharkov.ua/json/SearchPrepod/поисковый запрос/
 * 
 * 
 * 
 * 
 * http://schedule.kpi.kharkov.ua/JSON/BuildingList
 * 
 * http://schedule.kpi.kharkov.ua/JSON/AudListByBuilding/{bid}/
 * 
 * http://schedule.kpi.kharkov.ua/JSON/ScheduleA/{aid}/
 * 
 * http://schedule.kpi.kharkov.ua/JSON/Schedule2A/{aid}/
 * 
 * 
 * 
 * 
 * http://schedule.kpi.kharkov.ua/JSON/AudListByKafedra/{kid}/
 * 
 * http://schedule.kpi.kharkov.ua/JSON/AudListByBuilding/{bid}/
 * 
 * 
 * 
 * 
 * 
 */