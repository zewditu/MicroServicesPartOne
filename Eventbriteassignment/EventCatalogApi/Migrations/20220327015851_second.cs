using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogApi.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogTypes_EventPlaceId",
                table: "Catalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog");

            migrationBuilder.RenameTable(
                name: "CatalogTypes",
                newName: "Place");

            migrationBuilder.RenameTable(
                name: "Catalog",
                newName: "EventCatalog");

            migrationBuilder.RenameIndex(
                name: "IX_Catalog_EventPlaceId",
                table: "EventCatalog",
                newName: "IX_EventCatalog_EventPlaceId");

            migrationBuilder.AlterColumn<int>(
                name: "AgeLimit",
                table: "EventCatalog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "CatalogTypes");

            migrationBuilder.RenameTable(
                name: "EventCatalog",
                newName: "Catalog");

            migrationBuilder.RenameIndex(
                name: "IX_EventCatalog_EventPlaceId",
                table: "Catalog",
                newName: "IX_Catalog_EventPlaceId");

            migrationBuilder.AlterColumn<string>(
                name: "AgeLimit",
                table: "Catalog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogTypes_EventPlaceId",
                table: "Catalog",
                column: "EventPlaceId",
                principalTable: "CatalogTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
