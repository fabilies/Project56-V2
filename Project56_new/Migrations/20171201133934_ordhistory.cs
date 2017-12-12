using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project56_new.Migrations
{
    public partial class ordhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "OrdHistory");

            migrationBuilder.DropColumn(
                name: "dt_modified",
                table: "OrdHistory");

            migrationBuilder.DropColumn(
                name: "l_show",
                table: "OrdHistory");

            migrationBuilder.AddColumn<string>(
                name: "itm_description",
                table: "OrdHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ord_id",
                table: "OrdHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ordline_id",
                table: "OrdHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "priced_payed",
                table: "OrdHistory",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "qty_bought",
                table: "OrdHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "user_ad",
                table: "OrdHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "itm_description",
                table: "OrdHistory");

            migrationBuilder.DropColumn(
                name: "ord_id",
                table: "OrdHistory");

            migrationBuilder.DropColumn(
                name: "ordline_id",
                table: "OrdHistory");

            migrationBuilder.DropColumn(
                name: "priced_payed",
                table: "OrdHistory");

            migrationBuilder.DropColumn(
                name: "qty_bought",
                table: "OrdHistory");

            migrationBuilder.DropColumn(
                name: "user_ad",
                table: "OrdHistory");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "OrdHistory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dt_modified",
                table: "OrdHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "l_show",
                table: "OrdHistory",
                nullable: false,
                defaultValue: 0);
        }
    }
}
