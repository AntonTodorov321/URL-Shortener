using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIpTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ips");

            migrationBuilder.DropTable(
                name: "UrlIps");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "ShortCode",
                table: "Urls",
                newName: "ShortUrl");

            migrationBuilder.AddColumn<string>(
                name: "SecretCode",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UrlVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlVisits_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlVisits_UrlId",
                table: "UrlVisits",
                column: "UrlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlVisits");

            migrationBuilder.DropColumn(
                name: "SecretCode",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "ShortUrl",
                table: "Urls",
                newName: "ShortCode");

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Urls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlIps",
                columns: table => new
                {
                    IpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimesOpened = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlIps", x => new { x.IpId, x.UrlId });
                });
        }
    }
}
