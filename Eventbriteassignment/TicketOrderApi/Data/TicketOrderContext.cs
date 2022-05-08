using Microsoft.EntityFrameworkCore;
using TicketOrderApi.Models;

namespace TicketOrderApi.Data
{
    public class TicketOrderContext : DbContext
    {
        public TicketOrderContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TicketOrder> TicketOrders { get; set; }

    }
}