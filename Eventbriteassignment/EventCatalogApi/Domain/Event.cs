using Microsoft.VisualBasic;
using System;
using System.ComponentModel;

namespace EventCatalogApi.Domain
{
    public class Event
    {
       
        public int Id { get; set; }
        public string Name { get; set; }    
        public DateTime Date { get; set; }

        public string Desciprion { get; set; }

        public string PictureURL { get; set; }

        public Decimal Ticketprice { get; set; }
        public int AgeLimit { get; set; }
        public int EventPlaceId { get; set; }
        public int EventCategoryId { get; set; }


        // Navigational property 
        public virtual Place Place { get; set; }
        public virtual EventCategory Category { get; set; }
    }
}
