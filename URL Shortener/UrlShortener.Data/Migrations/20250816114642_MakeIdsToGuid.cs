#nullable disable

namespace UrlShortener.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class MakeIdsToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
            name: "NewId",
            table: "Urls",
            type: "uniqueidentifier",
            nullable: false,
            defaultValueSql: "NEWID()");

            migrationBuilder.DropPrimaryKey(
            name: "PK_Urls",
            table: "Urls");

            migrationBuilder.DropColumn(
            name: "Id",
            table: "Urls");

            migrationBuilder.RenameColumn(
            name: "NewId",
            table: "Urls",
            newName: "Id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Urls",
            table: "Urls",
            column: "Id");

            migrationBuilder.AddColumn<Guid>(
            name: "NewId",
            table: "Ips",
            type: "uniqueidentifier",
            nullable: false,
            defaultValueSql: "NEWID()");

            migrationBuilder.DropPrimaryKey(
            name: "PK_Ips",
            table: "Ips");

            migrationBuilder.DropColumn(
            name: "Id",
            table: "Ips");

            migrationBuilder.RenameColumn(
            name: "NewId",
            table: "Ips",
            newName: "Id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Ips",
            table: "Ips",
            column: "Id");

            migrationBuilder.AddColumn<Guid>(
            name: "NewUrlId",
            table: "UrlIps",
            type: "uniqueidentifier",
            nullable: false,
            defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
            name: "NewIpId",
            table: "UrlIps",
            type: "uniqueidentifier",
            nullable: false,
            defaultValueSql: "NEWID()");

            migrationBuilder.DropPrimaryKey(
            name: "PK_UrlIps",
            table: "UrlIps");

            migrationBuilder.DropColumn(
            name: "UrlId",
            table: "UrlIps");
            migrationBuilder.DropColumn(
            name: "IpId",
            table: "UrlIps");

            migrationBuilder.RenameColumn(
            name: "NewUrlId",
            table: "UrlIps",
            newName: "UrlId");
            migrationBuilder.RenameColumn(
            name: "NewIpId",
            table: "UrlIps",
            newName: "IpId");

            migrationBuilder.AddPrimaryKey(
            name: "PK_UrlIps",
            table: "UrlIps",
            columns: new[] { "UrlId", "IpId" });
        }
    }
}
