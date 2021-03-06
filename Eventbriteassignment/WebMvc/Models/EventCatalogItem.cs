namespace WebMvc.Models
{
    public class EventCatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public Decimal TicketPrice { get; set; }
        public int TicketQuantity { get; set; }
        public int AgeLimit { get; set; }
        public int EventPlaceId { get; set; }
        public int EventCategoryId { get; set; }
        public string Place { get; set; }
        public string Category { get; set; }
    }
}
