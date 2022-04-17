using System;

namespace EventCatalogApi.Domain
{
    //public enum PossibleEventCategory
    //{
    //    Music,
    //    FoodAndDrink,
    //    Business,
    //    Health,
    //    Community,
    //    TravelAndOutdoor,
    //    Spirituality,
    //    Other
    //}
    public class EventCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
