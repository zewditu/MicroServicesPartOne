using System.Xml.Serialization;

namespace EventCatalogApi.Domain
{
    public class Ticket
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }

    }
}
