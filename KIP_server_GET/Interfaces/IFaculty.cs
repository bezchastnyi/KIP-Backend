using KIP_server_GET.Models;
using System.Collections.Generic;

namespace KIP_server_GET.Interfaces {
    public interface IFaculty {
        IEnumerable<Faculty> AllFaculties { get; }

        Faculty getFacultyByID(uint FacultyID);
    }
}