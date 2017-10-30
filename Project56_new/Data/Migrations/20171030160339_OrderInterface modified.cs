using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project56_new.Data.Migrations
{
    public partial class OrderInterfacemodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdHistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dt_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    l_show = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdHistory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OrdLines",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dt_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    itm_id = table.Column<int>(type: "int", nullable: false),
                    l_show = table.Column<int>(type: "int", nullable: false),
                    ord_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdLines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OrdMains",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dt_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_delivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_order = table.Column<DateTime>(type: "datetime2", nullable: false),
                    l_show = table.Column<int>(type: "int", nullable: false),
                    ordstatus_id = table.Column<int>(type: "int", nullable: false),
                    user_ad = table.Column<string>(type: "string", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdMains", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OrdStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dt_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    l_show = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdStatus", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdHistory");

            migrationBuilder.DropTable(
                name: "OrdLines");

            migrationBuilder.DropTable(
                name: "OrdMains");

            migrationBuilder.DropTable(
                name: "OrdStatus");
        }
    }
}
