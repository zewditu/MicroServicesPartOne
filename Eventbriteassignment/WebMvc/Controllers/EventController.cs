using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventCatalogService _service;
        public EventController(IEventCatalogService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? pageNumber, int? catalogFilter, int? placeFilter)
        {
            var itemsOnPage = 10;
            var events = await _service.GetEventCatalogItemsAsync(pageNumber ?? 0, itemsOnPage,
                catalogFilter, placeFilter);


            var vm = new EventIndexViewModel
            {
                Categories = await _service.GetCategoriesAsync(),
                Places = await _service.GetPlacesAsync(),
                EventCatalogItems = events.Data,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = events.PageIndex,
                    TotalItems = events.Count,
                    ItemsPerPage = events.PageSize,
                    TotalPages = (int)Math.Ceiling((decimal)events.Count / itemsOnPage)
                },
                CategoryFilter = catalogFilter,
                PlaceFilter = placeFilter
            };

            return View(vm);
        }
    }
}
