using EventCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EventCatalogApi.Data
{
    public static class EventSeed
    {

        public static void Seed(EventCatalogContext context)
        {
            context.Database.Migrate();
            if(!context.EventPlace.AnyAsync().Result)
            {
                context.EventPlace.AddRange(GetEventPlaces());
                context.SaveChanges();
            }
            if (!context.EventCatalogTable.AnyAsync().Result)
            {
                context.EventCatalogTable.AddRange(GetEventCatalog());
                context.SaveChanges();
            }


        }

        private static IEnumerable<Event> GetEventCatalog()
        {
            return new List<Event>
            {
                new Event{Name="15th Annual Washington Brewers Festival",
                 Date= new DateTime(2022, 4, 16, 3,0,0),
                Desciprion="Nice to go there", PictureURL="http://externalcatalogbaseurltobereplaced/api/pic/1",
                Ticketprice=30,AgeLimit=10,EventPlaceId=1,
                },
                new Event{Name="Second event",
                 Date= new DateTime(2022, 4, 22, 3,0,0),
                Desciprion="Nice to go there", PictureURL="http://externalcatalogbaseurltobereplaced/api/pic/2",
                Ticketprice=30,AgeLimit=10,EventPlaceId=2,
                },
                new Event{Name="third event ",
                 Date= new DateTime(2022, 5, 10, 4,0,0),
                Desciprion="Nice to go there", PictureURL="http://externalcatalogbaseurltobereplaced/api/pic/3",
                Ticketprice=20,AgeLimit=18,EventPlaceId=1,
                },
                new Event{Name="third event ",
                 Date= new DateTime(2022, 4, 10, 4,0,0),
                Desciprion="Nice to go there", PictureURL="http://externalcatalogbaseurltobereplaced/api/pic/4",
                Ticketprice=20,AgeLimit=18,EventPlaceId=3,
                },

            };
        }

        private static IEnumerable<Place> GetEventPlaces()
        {
            return new List<Place>
            {
                new Place{Name="Seattle",Aderess="Magnuson Park Hangar " },
                new Place{Name="Redmond",Aderess="King County's Marymoor Park " },
                new Place{Name="Kirkland",Aderess="Lake Union Charters" },
            };
        }
    }
}
