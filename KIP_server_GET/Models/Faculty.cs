using System;
using Microsoft.EntityFrameworkCore;

namespace KIP_server_GET.Models
{
    [Keyless]
    public class Faculty
    {
        public int FacultyID { get; }

        public string FacultyShortName { get; }

        public string FacultyName { get; }

        public Faculty() { }

        public Faculty(int ID, string short_name, string name)
        {
            FacultyID = ID;
            FacultyShortName = short_name;
            FacultyName = name;
        }
    }
}