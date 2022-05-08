using WebMvc.Models;

namespace WebMvc.Services
{
    public interface ITicketOrderService
    {
        Task<TicketOrder> GetTicketOrder(string orderId);
        Task<int> CreateTicketOrder(TicketOrder order);
    }
}
