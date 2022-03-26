using Microsoft.VisualBasic;
using System.ComponentModel;

namespace EventCatalogApi.Domain
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public DateAndTime Date { get; set; }

        public string Desciprion { get; set; }

        public string PictureURL { get; set; }

        public decimal TickectPrice { get; set; }
        public string AgeLimit { get; set; }
        public string EventPlaceId { get; set; }

        // Navigational property 
        public virtual Place place { get; set; }    

    }
}
