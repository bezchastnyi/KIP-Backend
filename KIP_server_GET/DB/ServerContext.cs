using KIP_server_GET.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KIP_server_GET.DB
{
    public class ServerContext : DbContext
    {
        public DbSet<Audience> Audience { get; set; }
        public DbSet<Auth> Auth { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Cathedra> Cathedra { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Prof> Prof { get; set; }
        public DbSet<ProfSchedule> ProfSchedule { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentSchedule> StudentSchedule { get; set; }
        public DbSet<Subject> Subject { get; set; }

        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    new Group { FacultyID = 42, GroupID = 12619, Course = 1, GroupName = "КІТ-120д" },
                    new Group { FacultyID = 20, GroupID = 12194, Course = 2, GroupName = "СГТ-319б" },
                    new Group { FacultyID = 21, GroupID = 11114, Course = 3, GroupName = "КН-118" }
                });

            modelBuilder.Entity<Student>().HasData(
                new Student[]
                {
                    new Student { GroupID = 11114, StudentID = 18065, StudentName = "Безчастний", StudentSurname = "Олексій", StudentPatronymic = "Максимович", StudentBDay = new DateTime(2000, 12, 30) },
                    new Student { GroupID = 12619, StudentID = 17985, StudentName = "Ковальов", StudentSurname = "Дмитро", StudentPatronymic = "Олексійович", StudentBDay = new DateTime(2001, 9, 29) },
                    new Student { GroupID = 12194, StudentID = 18112, StudentName = "Жадан", StudentSurname = "Артем", StudentPatronymic = "Олександрович", StudentBDay = new DateTime(2001, 1, 18)}
                });

            modelBuilder.Entity<Auth>().HasData(
                new Auth[]
                {
                    new Auth { StudentID = 18065, Email = "andrey24@gmail.com", Password = "qwerty1245Aw" },
                    new Auth { StudentID = 17985, Email = "alexeypavlov@gmail.com", Password = "652REF84FGd" },
                    new Auth { StudentID = 18112, Email = "mrnoizy1@gmail.com", Password = "iamNOIZY" }
                });

            modelBuilder.Entity<News>().HasData(
                new News[]
                {
                    new News { FacultyID = 20, NewsID = 12022021, NewsText = "Увага! Студентам Ківа Владислав КН-419а та Шабаш Ігор КН-320г для вирішення питань отримання стипендії необхідно терміново звернуться до центрального відділення Приватбанку м. Харків" },
                    new News { FacultyID = 21, NewsID = 03032021, NewsText = "Студенти Моя Кордова, Савчук, Лобода, Надуэва, Петросов, Поцелуєв можуть отримати  е-тікет у вівторок." },
                    new News { FacultyID = 42, NewsID = 28012021, NewsText = "Шановні студенти контрактники. Нагадуємо вам, що відповідно до пунктів ваших контрактів вам необхідно сплатити за навчання протягом 10 днів після початку семестру." }
                });

            modelBuilder.Entity<Plan>().HasData(
                new Plan[]
                {
                    new Plan { StudentID = 18065, PlanID = 123, Time = new DateTime(2020, 4, 12, 12, 0, 0), PlanText = "Забрать флюрографию" },
                    new Plan { StudentID = 17985, PlanID = 122, Time = new DateTime(2020, 4, 13, 14, 30, 0), PlanText = "Контрольная по ТВ" },
                    new Plan { StudentID = 18112, PlanID = 121, Time = new DateTime(2020, 4, 15, 8, 30, 0), PlanText = "Встреча с куратором" }
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
                    new ProfSchedule { ProfID = 643, SubjectID = 358, BuildingID = 7, AudienceID = 1534, Week = true, Time = new DateTime(2020, 2, 24, 8, 30, 0) },
                    new ProfSchedule { ProfID = 12614, SubjectID = 114, BuildingID = 3, AudienceID = 1510, Week = true, Time = new DateTime(2020, 2, 24, 10, 25, 0) },
                    new ProfSchedule { ProfID = 1277, SubjectID = 77, BuildingID = 13, AudienceID = 1684, Week = false, Time = new DateTime(2020, 2, 24, 12, 35, 0) }
                });

            modelBuilder.Entity<StudentSchedule>().HasData(
                new StudentSchedule[]
                {
                    new StudentSchedule { GroupID = 12619, SubjectID = 358, BuildingID = 13, AudienceID = 1534, Week = false, Time = new DateTime(2020, 2, 25, 8, 30, 0) },
                    new StudentSchedule { GroupID = 12194, SubjectID = 114, BuildingID = 7, AudienceID = 1510, Week = true, Time = new DateTime(2020, 2, 25, 10, 25, 0) },
                    new StudentSchedule { GroupID = 11114, SubjectID = 77, BuildingID = 3, AudienceID = 1684, Week = false, Time = new DateTime(2020, 2, 25, 12, 35, 0) }
                });

            modelBuilder.Entity<Subject>().HasData(
                new Subject[]
                {
                    new Subject { CathedraID = 194, SubjectID = 358, SubjectName = "Теорія управління" },
                    new Subject { CathedraID = 76, SubjectID = 114, SubjectName = "Аналіз даних" },
                    new Subject { CathedraID = 76, SubjectID = 77, SubjectName = "Фізичне виховання" }
                });
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