using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models
{
    public class TicketOrder
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public DateTime OrderDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal OrderTotal { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }

        [BindNever]
        public string UserName { get; set; }
        [BindNever]
        public string BuyerId { get; set; }
        public string StripeToken { get; set; }

        public OrderStatus OrderStatus { get; set; }

        // See the property initializer syntax below. This
        // initializes the compiler generated field for this
        // auto-implemented property.
        // TODO: this init might be wrong
        public Ticket Ticket { get; } = new Ticket();

        public string PaymentAuthCode { get; set; }
    }
    public enum OrderStatus
    {
        Preparing = 1,
        Shipped = 2,
        Delivered = 3
    }
}

