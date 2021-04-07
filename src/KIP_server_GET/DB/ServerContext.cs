using KIP_server_GET.Models.KIP;
using Microsoft.EntityFrameworkCore;

namespace KIP_server_GET.DB
{
    public class ServerContext : DbContext
    {
        public DbSet<Audience> Audience { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Cathedra> Cathedra { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Prof> Prof { get; set; }
        public DbSet<ProfSchedule> ProfSchedule { get; set; }
        public DbSet<StudentSchedule> StudentSchedule { get; set; }

        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Building>().HasData(
                 new Building[]
                 {
                    new Building { BuildingID = 7, BuildingShortName = "АК", BuildingName = "Адміністративно-господарський корпус" },
                    new Building { BuildingID = 3, BuildingShortName = "У2", BuildingName = "Учбовий корпус 2" },
                    new Building { BuildingID = 13, BuildingShortName = "У5", BuildingName = "Учбовий корпус 5" }
                 });

            modelBuilder.Entity<Audience>().HasData(
                new Audience[]
                {
                    new Audience { BuildingID = 7, AudienceID = 1534, AudienceName = "307А", NumberOfSeats = 30 },
                    new Audience { BuildingID = 3, AudienceID = 1510, AudienceName = "1003", NumberOfSeats = 10 },
                    new Audience { BuildingID = 13, AudienceID = 1684, AudienceName = "21", NumberOfSeats = 15 }
                });

            modelBuilder.Entity<Faculty>().HasData(
                new Faculty[]
                {
                    new Faculty { FacultyID = 21, FacultyShortName = "КН", FacultyName = "Комп`ютерних наук і програмної інженерії"},
                    new Faculty { FacultyID = 20, FacultyShortName = "СГТ", FacultyName = "Соціально-гуманітарних технологій"},
                    new Faculty { FacultyID = 42, FacultyShortName = "КІТ", FacultyName = "Комп`ютерних та інформаційних технологій"}
                });

            modelBuilder.Entity<Cathedra>().HasData(
                new Cathedra[]
                {
                    new Cathedra { FacultyID = 21, CathedraID = 194, CathedraShortName = "ІІВ", CathedraName = "Інформатика та інтелектуальна власність" },
                    new Cathedra { FacultyID = 20, CathedraID = 31, CathedraShortName = "САІТ", CathedraName = "Системний аналіз та інформаційно-аналітичні технології" },
                    new Cathedra { FacultyID = 42, CathedraID = 76, CathedraShortName = "ІКС", CathedraName = "Інтелектуальні комп’ютерні системи" }
                });

            modelBuilder.Entity<Group>().HasData(
                new Group[]
                {
                    new Group { CathedraID = 194, FacultyID = 42, GroupID = 12619, Course = 1, GroupName = "КІТ-120д" },
                    new Group { CathedraID = 31, FacultyID = 20, GroupID = 12194, Course = 2, GroupName = "СГТ-319б" },
                    new Group { CathedraID = 76, FacultyID = 21, GroupID = 11114, Course = 3, GroupName = "КН-118" },
                    new Group { CathedraID = 76, FacultyID = 21, GroupID = 11117, Course = 4, GroupName = "КН-117" }
                });

            modelBuilder.Entity<Prof>().HasData(
                new Prof[]
                {
                    new Prof { CathedraID = 194, ProfID = 643, ProfName = "Галкін", ProfSurname = "Сергій", ProfPatronymic = "Олександрович" },
                    new Prof { CathedraID = 194, ProfID = 12614, ProfName = "Тарасенко", ProfSurname = "Ірина", ProfPatronymic = "Анатоліївна" },
                    new Prof { CathedraID = 76, ProfID = 1277, ProfName = "Шабанова-Кушнаренко", ProfSurname = "Любов", ProfPatronymic = "Володимирівна" }
                });
            modelBuilder.Entity<ProfSchedule>().HasData(
                new ProfSchedule[]
                {
                    new ProfSchedule { GroupID = { 12619, 12194 }, BuildingID = 13, AudienceID = 1534, ProfID = 643, SubjectName = "Теорія керування", Week = Week.UnPaired, day = Day.Monday, Type = "Лекція" },
                    new ProfSchedule { GroupID = { 12194 }, BuildingID = 7, AudienceID = 1510, ProfID = 12614, SubjectName = "П6", Week = Week.UnPaired, day = Day.Friday, Type = "Практика" },
                    new ProfSchedule { GroupID = { 11114 }, BuildingID = 3, AudienceID = 1684, ProfID = 1277, SubjectName = "Нейромережеві технології", Week = Week.Paired, day = Day.Thursday, Type = "Екзамен"  }
                });
            
            modelBuilder.Entity<StudentSchedule>().HasData(
                new StudentSchedule[]
                {
                    new StudentSchedule { GroupID = 12619, BuildingID = 13, AudienceID = 1534, ProfID = 643, SubjectName = "Теорія керування", Week = Week.UnPaired, day = Day.Monday, Type = "Лекція" },
                    new StudentSchedule { GroupID = 12194, BuildingID = 7, AudienceID = 1510, ProfID = 12614, SubjectName = "П6", Week = Week.UnPaired, day = Day.Friday, Type = "Практика" },
                    new StudentSchedule { GroupID = 11114, BuildingID = 3, AudienceID = 1684, ProfID = 1277, SubjectName = "Нейромережеві технології", Week = Week.Paired, day = Day.Thursday, Type = "Екзамен"  }
                });
            */
        }
    }
}

/*
 * DB Entity Framework Migration script
 * 
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add KIP_DB_Migration
 * dotnet ef database update
 */