using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketOrderApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentAuthCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrders", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketOrders");
        }
    }
}
