using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KIP_POST_APP.Migrations
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuildingName = table.Column<string>(type: "varchar(100)", nullable: false),
                    BuildingShortName = table.Column<string>(type: "varchar(5)", nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacultyName = table.Column<string>(type: "varchar(100)", nullable: false),
                    FacultyShortName = table.Column<string>(type: "varchar(7)", nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AudienceName = table.Column<string>(type: "varchar(100)", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "integer", nullable: true),
                    BuildingID = table.Column<int>(type: "integer", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CathedraName = table.Column<string>(type: "varchar(100)", nullable: false),
                    CathedraShortName = table.Column<string>(type: "varchar(7)", nullable: true),
                    FacultyID = table.Column<int>(type: "integer", nullable: false)
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
                name: "Prof",
                columns: table => new
                {
                    ProfID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfSurname = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProfName = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProfPatronymic = table.Column<string>(type: "varchar(100)", nullable: true),
                    CathedraID = table.Column<int>(type: "integer", nullable: false)
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
                name: "ProfSchedule",
                columns: table => new
                {
                    ProfScheduleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "varchar(200)", nullable: false),
                    week = table.Column<int>(type: "integer", nullable: false),
                    day = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProfID = table.Column<int>(type: "integer", nullable: false),
                    BuildingID = table.Column<int>(type: "integer", nullable: true),
                    AudienceID = table.Column<int>(type: "integer", nullable: true),
                    GroupID = table.Column<List<Nullable<int>>>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfSchedule", x => x.ProfScheduleID);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Audience_AudienceID",
                        column: x => x.AudienceID,
                        principalTable: "Audience",
                        principalColumn: "AudienceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Prof_ProfID",
                        column: x => x.ProfID,
                        principalTable: "Prof",
                        principalColumn: "ProfID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false),
                    FacultyID = table.Column<int>(type: "integer", nullable: false),
                    ProfScheduleID = table.Column<int>(type: "integer", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Group_ProfSchedule_ProfScheduleID",
                        column: x => x.ProfScheduleID,
                        principalTable: "ProfSchedule",
                        principalColumn: "ProfScheduleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentSchedule",
                columns: table => new
                {
                    StudentScheduleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "varchar(200)", nullable: false),
                    week = table.Column<int>(type: "integer", nullable: false),
                    day = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    GroupID = table.Column<int>(type: "integer", nullable: false),
                    BuildingID = table.Column<int>(type: "integer", nullable: true),
                    AudienceID = table.Column<int>(type: "integer", nullable: true),
                    ProfID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSchedule", x => x.StudentScheduleID);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Audience_AudienceID",
                        column: x => x.AudienceID,
                        principalTable: "Audience",
                        principalColumn: "AudienceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Prof_ProfID",
                        column: x => x.ProfID,
                        principalTable: "Prof",
                        principalColumn: "ProfID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audience_BuildingID",
                table: "Audience",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_FacultyID",
                table: "Cathedra",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_FacultyID",
                table: "Group",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ProfScheduleID",
                table: "Group",
                column: "ProfScheduleID");

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
                name: "IX_StudentSchedule_ProfID",
                table: "StudentSchedule",
                column: "ProfID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSchedule");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "ProfSchedule");

            migrationBuilder.DropTable(
                name: "Audience");

            migrationBuilder.DropTable(
                name: "Prof");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Cathedra");

            migrationBuilder.DropTable(
                name: "Faculty");
        }
    }
}
