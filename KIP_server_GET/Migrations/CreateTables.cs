using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KIP_server_GET.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    LocationCode = table.Column<string>(maxLength: 10, nullable: false),
                    RecCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    RecModified = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    RfidUsername = table.Column<string>(maxLength: 256, nullable: true),
                    RfidPassword = table.Column<string>(maxLength: 256, nullable: true),
                    Target = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.LocationCode);
                });










            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    LocationCode = table.Column<string>(maxLength: 10, nullable: false),
                    RecCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    RecModified = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    RfidUsername = table.Column<string>(maxLength: 256, nullable: true),
                    RfidPassword = table.Column<string>(maxLength: 256, nullable: true),
                    Target = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.LocationCode);
                });

            migrationBuilder.CreateTable(
                name: "SyncTicket",
                columns: table => new
                {
                    SyncTicketId = table.Column<Guid>(nullable: false),
                    RecCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    RecModified = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    ReceiptId = table.Column<Guid>(nullable: false),
                    ReceiptAltKey = table.Column<string>(maxLength: 128, nullable: true),
                    Target = table.Column<string>(maxLength: 128, nullable: false),
                    TicketType = table.Column<string>(maxLength: 128, nullable: false),
                    Status = table.Column<string>(maxLength: 128, nullable: false),
                    ExtendedStatus = table.Column<string>(maxLength: 128, nullable: true),
                    LocationCode = table.Column<string>(maxLength: 128, nullable: true),
                    PosNo = table.Column<string>(maxLength: 128, nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    ErrorCode = table.Column<string>(maxLength: 128, nullable: true),
                    ErrorMessage = table.Column<string>(maxLength: 1024, nullable: true),
                    TryCount = table.Column<int>(nullable: false),
                    NextTryAfterDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncTicket", x => x.SyncTicketId);
                });

            migrationBuilder.CreateTable(
                name: "CsRfidRequest",
                columns: table => new
                {
                    SyncTicketId = table.Column<Guid>(nullable: false),
                    RecCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    Type = table.Column<string>(maxLength: 128, nullable: false),
                    JsonData = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsRfidRequest", x => x.SyncTicketId);
                    table.ForeignKey(
                        name: "FK_CsRfidRequest_SyncTicket_SyncTicketId",
                        column: x => x.SyncTicketId,
                        principalTable: "SyncTicket",
                        principalColumn: "SyncTicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Config_LocationCode",
                table: "Config",
                column: "LocationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CsRfidRequest_SyncTicketId",
                table: "CsRfidRequest",
                column: "SyncTicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyncTicket_ReceiptAltKey",
                table: "SyncTicket",
                column: "ReceiptAltKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyncTicket_ReceiptId",
                table: "SyncTicket",
                column: "ReceiptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyncTicket_SyncTicketId",
                table: "SyncTicket",
                column: "SyncTicketId",
                unique: true);
        }        
    }
}
