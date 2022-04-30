namespace WebMvc.Models
{
    public class EventCatalog
    {
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long Count { get; set; }
            public IEnumerable<EventCatalogItem> Data { get; set; }
    }
}
