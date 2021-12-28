using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiGenericRepository.Migrations
{
    public partial class TokenUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DtToken",
                table: "Tb_User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "Tb_User",
                type: "nvarchar(500)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtToken",
                table: "Tb_User");

            migrationBuilder.DropColumn(
                name: "token",
                table: "Tb_User");
        }
    }
}
