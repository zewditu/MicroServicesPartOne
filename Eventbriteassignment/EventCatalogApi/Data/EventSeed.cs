using EventCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventCatalogApi.Data
{
    public static class EventSeed
    {

        public static void Seed(EventCatalogContext context)
        {
            context.Database.Migrate();

            if (!context.EventPlace.Any())
            {
                context.EventPlace.AddRange(GetEventPlaces());
                context.SaveChanges();
            }
            if (!context.EventCategory.Any())
            {
                context.EventCategory.AddRange(GetEventCategory());
                context.SaveChanges();
            }

            if (!context.EventCatalogTable.Any())
            {
                context.EventCatalogTable.AddRange(GetEventCatalog());
                context.SaveChanges();
            }
        }

        private static IEnumerable<EventCategory> GetEventCategory()
        {
            return new List<EventCategory>
            {
                new EventCategory{CategoryName="Music"},
                new EventCategory{CategoryName="TravelAndOutdoor"},
                new EventCategory{CategoryName="Community"}
            };
        }

        private static IEnumerable<Event> GetEventCatalog()
        {
            return new List<Event>
            {
                new Event{Name="15th Annual Washington Brewers Festival",
                Date= new DateTime(2022, 4, 16, 3,0,0),
                Desciprion="Nice to go there",
                PictureURL="http://externalcatalogbaseurltobereplaced/api/pics/1",
                Ticketprice=30,
                AgeLimit=10,
                EventPlaceId=1,
                EventCategoryId=1,
                },
                new Event{Name="Akajeannette Presents: Always, Patsy Cline",
                Date= new DateTime(2022, 4, 22, 3,0,0),
                Desciprion="Mary's River Grange #685 presents an Akajeannette Theatrical production of Always, Patsy Cline.",
                PictureURL="http://externalcatalogbaseurltobereplaced/api/pics/2",
                Ticketprice=30,
                AgeLimit=10,
                EventPlaceId=2,
                EventCategoryId= 2
                },
                new Event{Name="Why Hannelore Klein Did Not Suffer the Same Fate As Her Childhood Friend",
                 Date= new DateTime(2022, 5, 10, 4,0,0),
                Desciprion="Laureen Nussbaum was born Hannelore Klein in Frankfurt, Germany.",
                PictureURL="http://externalcatalogbaseurltobereplaced/api/pics/3",
                Ticketprice=20,
                AgeLimit=18,
                EventPlaceId=1,
                EventCategoryId = 3,
                },
                new Event{Name="Free Range Open Mic",
                 Date= new DateTime(2022, 4, 10, 4,0,0),
                Desciprion="Corvallis' long-running open mic is back after a year of...well, you know. Come out and show the world what you've been working on! Sign ups are at 7:30 and each slot is for two songs or 10 minutes of performance whichever is less.",
                 PictureURL="http://externalcatalogbaseurltobereplaced/api/pics/4",
                Ticketprice=20,
                AgeLimit=18,
                EventPlaceId=3,
                EventCategoryId=1
                },

            };
        }

        private static IEnumerable<Place> GetEventPlaces()
        {
            return new List<Place>
            {
                new Place{Name="Seattle",Address="Magnuson Park Hangar " },
                new Place{Name="Redmond",Address="King County's Marymoor Park " },
                new Place{Name="Kirkland",Address="Lake Union Charters" },
            };
        }
    }
}
