using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project56_new.Migrations
{
    public partial class wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishLines",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dt_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    itm_id = table.Column<int>(type: "int", nullable: false),
                    l_show = table.Column<int>(type: "int", nullable: false),
                    ord_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WishMains",
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
                    user_ad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishMains", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishLines");

            migrationBuilder.DropTable(
                name: "WishMains");
        }
    }
}
