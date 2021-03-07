using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KIP_server_GET.Migrations
{
    public partial class KIP_DB_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    BuildingID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BuildingShortName = table.Column<string>(type: "varchar(5)", nullable: true),
                    BuildingName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingID);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    FacultyID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FacultyShortName = table.Column<string>(type: "varchar(7)", nullable: true),
                    FacultyName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.FacultyID);
                });

            migrationBuilder.CreateTable(
                name: "Audience",
                columns: table => new
                {
                    AudienceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BuildingID = table.Column<int>(type: "integer", nullable: false),
                    AudienceName = table.Column<string>(type: "varchar(7)", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audience", x => x.AudienceID);
                    table.ForeignKey(
                        name: "FK_Audience_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cathedra",
                columns: table => new
                {
                    CathedraID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FacultyID = table.Column<int>(type: "integer", nullable: false),
                    CathedraShortName = table.Column<string>(type: "varchar(7)", nullable: true),
                    CathedraName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedra", x => x.CathedraID);
                    table.ForeignKey(
                        name: "FK_Cathedra_Faculty_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculty",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FacultyID = table.Column<int>(type: "integer", nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false),
                    GroupName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_Group_Faculty_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculty",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FacultyID = table.Column<int>(type: "integer", nullable: false),
                    NewsText = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsID);
                    table.ForeignKey(
                        name: "FK_News_Faculty_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculty",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prof",
                columns: table => new
                {
                    ProfID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CathedraID = table.Column<int>(type: "integer", nullable: false),
                    ProfName = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProfSurname = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProfPatronymic = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prof", x => x.ProfID);
                    table.ForeignKey(
                        name: "FK_Prof_Cathedra_CathedraID",
                        column: x => x.CathedraID,
                        principalTable: "Cathedra",
                        principalColumn: "CathedraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CathedraID = table.Column<int>(type: "integer", nullable: false),
                    SubjectName = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_Subject_Cathedra_CathedraID",
                        column: x => x.CathedraID,
                        principalTable: "Cathedra",
                        principalColumn: "CathedraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GroupID = table.Column<int>(type: "integer", nullable: false),
                    StudentName = table.Column<string>(type: "varchar(50)", nullable: false),
                    StudentSurname = table.Column<string>(type: "varchar(50)", nullable: false),
                    StudentPatronymic = table.Column<string>(type: "varchar(50)", nullable: false),
                    StudentBDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfSchedule",
                columns: table => new
                {
                    ProfScheduleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProfID = table.Column<int>(type: "integer", nullable: false),
                    SubjectID = table.Column<int>(type: "integer", nullable: false),
                    BuildingID = table.Column<int>(type: "integer", nullable: false),
                    AudienceID = table.Column<int>(type: "integer", nullable: false),
                    Week = table.Column<bool>(type: "boolean", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfSchedule", x => x.ProfScheduleID);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Audience_AudienceID",
                        column: x => x.AudienceID,
                        principalTable: "Audience",
                        principalColumn: "AudienceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Prof_ProfID",
                        column: x => x.ProfID,
                        principalTable: "Prof",
                        principalColumn: "ProfID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSchedule",
                columns: table => new
                {
                    StudentScheduleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GroupID = table.Column<int>(type: "integer", nullable: false),
                    SubjectID = table.Column<int>(type: "integer", nullable: false),
                    BuildingID = table.Column<int>(type: "integer", nullable: false),
                    AudienceID = table.Column<int>(type: "integer", nullable: false),
                    Week = table.Column<bool>(type: "boolean", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSchedule", x => x.StudentScheduleID);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Audience_AudienceID",
                        column: x => x.AudienceID,
                        principalTable: "Audience",
                        principalColumn: "AudienceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth",
                columns: table => new
                {
                    AuthID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StudentID = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth", x => x.AuthID);
                    table.ForeignKey(
                        name: "FK_Auth_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StudentID = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PlanText = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.PlanID);
                    table.ForeignKey(
                        name: "FK_Plan_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "BuildingID", "BuildingName", "BuildingShortName" },
                values: new object[,]
                {
                    { 7, "Адміністративно-господарський корпус", "АК" },
                    { 3, "Учбовий корпус 2", "У2" },
                    { 13, "Учбовий корпус 5", "У5" }
                });

            migrationBuilder.InsertData(
                table: "Faculty",
                columns: new[] { "FacultyID", "FacultyName", "FacultyShortName" },
                values: new object[,]
                {
                    { 21, "Комп`ютерних наук і програмної інженерії", "КН" },
                    { 20, "Соціально-гуманітарних технологій", "СГТ" },
                    { 42, "Комп`ютерних та інформаційних технологій", "КІТ" }
                });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "GroupID", "Course", "FacultyID", "GroupName" },
                values: new object[,]
                {
                    { 11114, 3, 21, "КН-118" },
                    { 12194, 2, 20, "СГТ-319б" },
                    { 12619, 1, 42, "КІТ-120д" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentID", "GroupID", "StudentBDay", "StudentName", "StudentPatronymic", "StudentSurname" },
                values: new object[,]
                {
                    { 18065, 11114, new DateTime(2000, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Безчастний", "Максимович", "Олексій" },
                    { 17985, 11114, new DateTime(2001, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ковальов", "Олексійович", "Дмитро" },
                    { 18112, 11114, new DateTime(2001, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Жадан", "Олександрович", "Артем" }
                });

            migrationBuilder.InsertData(
                table: "Audience",
                columns: new[] { "AudienceID", "AudienceName", "BuildingID", "NumberOfSeats" },
                values: new object[,]
                {
                    { 1534, "307А", 7, 30 },
                    { 1510, "1003", 3, 10 },
                    { 1684, "21", 13, 15 }
                });

            migrationBuilder.InsertData(
                table: "Auth",
                columns: new[] { "AuthID", "Email", "Password", "StudentID" },
                values: new object[,]
                {
                    { 321, "alexeypavlov@gmail.com", "652REF84FGd", 17985 },
                    { 213, "mrnoizy1@gmail.com", "iamNOIZY", 18112 },
                    { 123, "andrey24@gmail.com", "qwerty1245Aw", 18065 }
                });

            migrationBuilder.InsertData(
                table: "Cathedra",
                columns: new[] { "CathedraID", "CathedraName", "CathedraShortName", "FacultyID" },
                values: new object[,]
                {
                    { 194, "Інформатика та інтелектуальна власність", "ІІВ", 21 },
                    { 31, "Системний аналіз та інформаційно-аналітичні технології", "САІТ", 20 },
                    { 76, "Інтелектуальні комп’ютерні системи", "ІКС", 42 }
                });            

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsID", "FacultyID", "NewsText" },
                values: new object[,]
                {
                    { 12022021, 20, "Увага! Студентам Ківа Владислав КН-419а та Шабаш Ігор КН-320г для вирішення питань отримання стипендії необхідно терміново звернуться до центрального відділення Приватбанку м. Харків" },
                    { 3032021, 21, "Студенти Моя Кордова, Савчук, Лобода, Надуэва, Петросов, Поцелуєв можуть отримати  е-тікет у вівторок." },
                    { 28012021, 42, "Шановні студенти контрактники. Нагадуємо вам, що відповідно до пунктів ваших контрактів вам необхідно сплатити за навчання протягом 10 днів після початку семестру." }
                });

            migrationBuilder.InsertData(
                table: "Plan",
                columns: new[] { "PlanID", "PlanText", "StudentID", "Time" },
                values: new object[,]
                {
                    { 123, "Забрать флюрографию", 18065, new DateTime(2020, 4, 12, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 122, "Контрольная по ТВ", 17985, new DateTime(2020, 4, 13, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 121, "Встреча с куратором", 18112, new DateTime(2020, 4, 15, 8, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Prof",
                columns: new[] { "ProfID", "CathedraID", "ProfName", "ProfPatronymic", "ProfSurname" },
                values: new object[,]
                {
                    { 643, 194, "Галкін", "Олександрович", "Сергій" },
                    { 12614, 194, "Тарасенко", "Анатоліївна", "Ірина" },
                    { 1277, 76, "Шабанова-Кушнаренко", "Володимирівна", "Любов" }
                });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "SubjectID", "CathedraID", "SubjectName" },
                values: new object[,]
                {
                    { 358, 194, "Теорія управління" },
                    { 114, 76, "Аналіз даних" },
                    { 77, 76, "Фізичне виховання" }
                });

            migrationBuilder.InsertData(
                table: "ProfSchedule",
                columns: new[] { "ProfScheduleID", "AudienceID", "BuildingID", "ProfID", "SubjectID", "Time", "Week" },
                values: new object[,]
                {
                    { 123, 1534, 7, 643, 358, new DateTime(2020, 2, 24, 8, 30, 0, 0, DateTimeKind.Unspecified), true },
                    { 122, 1510, 3, 12614, 114, new DateTime(2020, 2, 24, 10, 25, 0, 0, DateTimeKind.Unspecified), true },
                    { 121, 1684, 13, 1277, 77, new DateTime(2020, 2, 24, 12, 35, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "StudentSchedule",
                columns: new[] { "StudentScheduleID", "AudienceID", "BuildingID", "GroupID", "SubjectID", "Time", "Week" },
                values: new object[,]
                {
                    { 123, 1534, 13, 12619, 358, new DateTime(2020, 2, 25, 8, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { 122, 1510, 7, 12194, 114, new DateTime(2020, 2, 25, 10, 25, 0, 0, DateTimeKind.Unspecified), true },
                    { 121, 1684, 3, 11114, 77, new DateTime(2020, 2, 25, 12, 35, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audience_BuildingID",
                table: "Audience",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_StudentID",
                table: "Auth",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_FacultyID",
                table: "Cathedra",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_FacultyID",
                table: "Group",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_News_FacultyID",
                table: "News",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_StudentID",
                table: "Plan",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Prof_CathedraID",
                table: "Prof",
                column: "CathedraID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_AudienceID",
                table: "ProfSchedule",
                column: "AudienceID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_BuildingID",
                table: "ProfSchedule",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_ProfID",
                table: "ProfSchedule",
                column: "ProfID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_SubjectID",
                table: "ProfSchedule",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GroupID",
                table: "Student",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_AudienceID",
                table: "StudentSchedule",
                column: "AudienceID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_BuildingID",
                table: "StudentSchedule",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_GroupID",
                table: "StudentSchedule",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_SubjectID",
                table: "StudentSchedule",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_CathedraID",
                table: "Subject",
                column: "CathedraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "ProfSchedule");

            migrationBuilder.DropTable(
                name: "StudentSchedule");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Prof");

            migrationBuilder.DropTable(
                name: "Audience");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Cathedra");

            migrationBuilder.DropTable(
                name: "Faculty");
        }
    }
}
