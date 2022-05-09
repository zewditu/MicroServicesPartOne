

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketOrderApi.Models
{
    public enum OrderStatus
    {
        Preparing = 1,
        Shipped = 2,
        Delivered = 3,
    }
    public class TicketOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string BuyerId { get; set; }
        public string PaymentAuthCode { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventName { get; set; }
        public int Quantity { get; set; }

    }
}
