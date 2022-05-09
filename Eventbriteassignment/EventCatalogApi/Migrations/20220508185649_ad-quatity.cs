using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogApi.Migrations
{
    public partial class adquatity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketQuantity",
                table: "EventCatalogTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketQuantity",
                table: "EventCatalogTable");
        }
    }
}
