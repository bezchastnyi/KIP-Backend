using KIP_server_GET.Interfaces;
using KIP_server_GET.Models;
using System.Collections.Generic;

namespace KIP_server_GET.Mocks {
    public class MockFaculty : IFaculty {


        public IEnumerable<Faculty> AllFaculties {
            get {
                return new List<Faculty> {
                    new Faculty(21, "КН", "Комп'ютерних наук та програмної інженерії"),
                    new Faculty(42, "КІТ", "Комп'ютерних та інформаційних технологій")
                };
            }
        }

        public Faculty getFacultyByID(uint FacultyID) {
            throw new System.NotImplementedException();
        }
    }
}