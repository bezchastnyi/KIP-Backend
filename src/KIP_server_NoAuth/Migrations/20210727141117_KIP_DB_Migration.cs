using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KIP_server_NoAuth.Migrations
{
    /// <inheritdoc />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1125:Use shorthand for nullable types", Justification = "Migrations need it")]
    public partial class KIP_DB_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    BuildingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BuildingName = table.Column<string>(type: "varchar(100)", nullable: false),
                    BuildingShortName = table.Column<string>(type: "varchar(5)", nullable: true),
                    NumberOfAudiences = table.Column<int>(type: "integer", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingId);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FacultyName = table.Column<string>(type: "varchar(100)", nullable: false),
                    FacultyShortName = table.Column<string>(type: "varchar(7)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Faculty = table.Column<string>(type: "text", nullable: true),
                    Course = table.Column<int>(type: "integer", nullable: true),
                    Group = table.Column<string>(type: "text", nullable: true),
                    TempProfValue = table.Column<int>(type: "integer", nullable: true),
                    TempBuildingValue = table.Column<int>(type: "integer", nullable: true),
                    TempAudienceValue = table.Column<int>(type: "integer", nullable: true),
                    TempDayValue = table.Column<int>(type: "integer", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Audience",
                columns: table => new
                {
                    AudienceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AudienceName = table.Column<string>(type: "varchar(100)", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "integer", nullable: true),
                    ScheduleIsPresent = table.Column<List<bool>>(type: "boolean[]", nullable: true),
                    BuildingId = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audience", x => x.AudienceId);
                    table.ForeignKey(
                        name: "FK_Audience_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cathedra",
                columns: table => new
                {
                    CathedraId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CathedraName = table.Column<string>(type: "varchar(100)", nullable: false),
                    CathedraShortName = table.Column<string>(type: "varchar(7)", nullable: true),
                    FacultyId = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedra", x => x.CathedraId);
                    table.ForeignKey(
                        name: "FK_Cathedra_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GroupName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false),
                    ScheduleIsPresent = table.Column<List<bool>>(type: "boolean[]", nullable: true),
                    FacultyId = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prof",
                columns: table => new
                {
                    ProfId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProfSurname = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProfName = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProfPatronymic = table.Column<string>(type: "varchar(100)", nullable: true),
                    ScheduleIsPresent = table.Column<List<bool>>(type: "boolean[]", nullable: true),
                    CathedraId = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prof", x => x.ProfId);
                    table.ForeignKey(
                        name: "FK_Prof_Cathedra_CathedraId",
                        column: x => x.CathedraId,
                        principalTable: "Cathedra",
                        principalColumn: "CathedraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudienceSchedule",
                columns: table => new
                {
                    AudienceScheduleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SubjectName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Week = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    BuildingId = table.Column<int>(type: "integer", nullable: false),
                    AudienceId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<List<Nullable<int>>>(type: "integer[]", nullable: true),
                    GroupNames = table.Column<string>(type: "text", nullable: true),
                    ProfId = table.Column<int>(type: "integer", nullable: true),
                    ProfName = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceSchedule", x => x.AudienceScheduleId);
                    table.ForeignKey(
                        name: "FK_AudienceSchedule_Audience_AudienceId",
                        column: x => x.AudienceId,
                        principalTable: "Audience",
                        principalColumn: "AudienceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudienceSchedule_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudienceSchedule_Prof_ProfId",
                        column: x => x.ProfId,
                        principalTable: "Prof",
                        principalColumn: "ProfId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfSchedule",
                columns: table => new
                {
                    ProfScheduleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SubjectName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Week = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    ProfId = table.Column<int>(type: "integer", nullable: false),
                    BuildingId = table.Column<int>(type: "integer", nullable: true),
                    AudienceId = table.Column<int>(type: "integer", nullable: true),
                    AudienceName = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<List<Nullable<int>>>(type: "integer[]", nullable: true),
                    GroupNames = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfSchedule", x => x.ProfScheduleId);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Audience_AudienceId",
                        column: x => x.AudienceId,
                        principalTable: "Audience",
                        principalColumn: "AudienceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfSchedule_Prof_ProfId",
                        column: x => x.ProfId,
                        principalTable: "Prof",
                        principalColumn: "ProfId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSchedule",
                columns: table => new
                {
                    StudentScheduleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SubjectName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Week = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    BuildingId = table.Column<int>(type: "integer", nullable: true),
                    AudienceId = table.Column<int>(type: "integer", nullable: true),
                    AudienceName = table.Column<string>(type: "text", nullable: true),
                    ProfId = table.Column<int>(type: "integer", nullable: true),
                    ProfName = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSchedule", x => x.StudentScheduleId);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Audience_AudienceId",
                        column: x => x.AudienceId,
                        principalTable: "Audience",
                        principalColumn: "AudienceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSchedule_Prof_ProfId",
                        column: x => x.ProfId,
                        principalTable: "Prof",
                        principalColumn: "ProfId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audience_AudienceId",
                table: "Audience",
                column: "AudienceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audience_BuildingId",
                table: "Audience",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceSchedule_AudienceId",
                table: "AudienceSchedule",
                column: "AudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceSchedule_AudienceId_Day",
                table: "AudienceSchedule",
                columns: new[] { "AudienceId", "Day" });

            migrationBuilder.CreateIndex(
                name: "IX_AudienceSchedule_BuildingId",
                table: "AudienceSchedule",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceSchedule_ProfId",
                table: "AudienceSchedule",
                column: "ProfId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_BuildingId",
                table: "Building",
                column: "BuildingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_CathedraId",
                table: "Cathedra",
                column: "CathedraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_FacultyId",
                table: "Cathedra",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_FacultyId",
                table: "Faculty",
                column: "FacultyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_FacultyId",
                table: "Group",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_GroupId",
                table: "Group",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prof_CathedraId",
                table: "Prof",
                column: "CathedraId");

            migrationBuilder.CreateIndex(
                name: "IX_Prof_ProfId",
                table: "Prof",
                column: "ProfId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_AudienceId",
                table: "ProfSchedule",
                column: "AudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_BuildingId",
                table: "ProfSchedule",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_ProfId",
                table: "ProfSchedule",
                column: "ProfId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfSchedule_ProfId_Day",
                table: "ProfSchedule",
                columns: new[] { "ProfId", "Day" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_AudienceId",
                table: "StudentSchedule",
                column: "AudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_BuildingId",
                table: "StudentSchedule",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_GroupId",
                table: "StudentSchedule",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_GroupId_Day",
                table: "StudentSchedule",
                columns: new[] { "GroupId", "Day" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedule_ProfId",
                table: "StudentSchedule",
                column: "ProfId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudienceSchedule");

            migrationBuilder.DropTable(
                name: "ProfSchedule");

            migrationBuilder.DropTable(
                name: "StudentSchedule");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Audience");

            migrationBuilder.DropTable(
                name: "Group");

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
