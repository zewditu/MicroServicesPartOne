using Microsoft.AspNetCore.Mvc.Rendering;
using WebMvc.Models;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventIndexViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Places { get; set; }
        public IEnumerable<EventCatalogItem> EventCatalogItems { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public int? CategoryFilter { get; set; }
        public int? PlaceFilter { get; set; }
    }
}