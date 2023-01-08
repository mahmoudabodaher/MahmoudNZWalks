using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MahmoudNZWalks.API.Migrations
{
    public partial class UpdateTheTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Region_RegionID",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionID",
                table: "Walks",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionID",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Region_RegionID",
                table: "Walks",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
