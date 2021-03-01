
namespace KIP_server_GET.Models
{
    public class Faculty
    {
        public uint FacultyID { get; }

        public string FacultyShortName { get; }

        public string FacultyName { get; }

        public Faculty(uint ID, string short_name, string name)
        {
            FacultyID = ID;
            FacultyShortName = short_name;
            FacultyName = name;
        }
    }
}