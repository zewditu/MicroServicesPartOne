using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogApi.Migrations
{
    public partial class forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventCatalog_Place_EventPlaceId",
                table: "EventCatalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCatalog",
                table: "EventCatalog");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "EventPlace");

            migrationBuilder.RenameTable(
                name: "EventCatalog",
                newName: "EventCatalogTable");

            migrationBuilder.RenameIndex(
                name: "IX_EventCatalog_EventPlaceId",
                table: "EventCatalogTable",
                newName: "IX_EventCatalogTable_EventPlaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventPlace",
                table: "EventPlace",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCatalogTable",
                table: "EventCatalogTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventCatalogTable_EventPlace_EventPlaceId",
                table: "EventCatalogTable",
                column: "EventPlaceId",
                principalTable: "EventPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventCatalogTable_EventPlace_EventPlaceId",
                table: "EventCatalogTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventPlace",
                table: "EventPlace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCatalogTable",
                table: "EventCatalogTable");

            migrationBuilder.RenameTable(
                name: "EventPlace",
                newName: "Place");

            migrationBuilder.RenameTable(
                name: "EventCatalogTable",
                newName: "EventCatalog");

            migrationBuilder.RenameIndex(
                name: "IX_EventCatalogTable_EventPlaceId",
                table: "EventCatalog",
                newName: "IX_EventCatalog_EventPlaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCatalog",
                table: "EventCatalog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventCatalog_Place_EventPlaceId",
                table: "EventCatalog",
                column: "EventPlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
