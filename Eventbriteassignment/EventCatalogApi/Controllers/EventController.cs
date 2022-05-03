using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogApi.Data;
using EventCatalogApi.Domain;
using EventCatalogApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventCatalogContext _context;
        private readonly IConfiguration _config;
        public EventController(EventCatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> EventPlaces()
        {
            var places = await _context.EventPlace.ToListAsync();
            return Ok(places);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> EventCategories()
        {
            var categories = await _context.EventCategory.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Events(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {
            var itemsCount = _context.EventCatalogTable.LongCountAsync();
            var items = await _context.EventCatalogTable
                                .OrderBy(c => c.Name)
                                .Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            items = ChangePictureUrl(items);

            var model = new PaginatedItemsViewModel
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count =  itemsCount.Result,
                Data = items
            };
            return Ok(model);
        }


        [HttpGet("[action]/filter")]
        public async Task<IActionResult> Events(
            [FromQuery] int? eventTypeId,
            [FromQuery] int? eventPlaceId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<Event>)_context.EventCatalogTable;
            if (eventTypeId.HasValue)
            {
                query = query.Where(c => c.EventCategoryId == eventTypeId);
            }
            if (eventPlaceId.HasValue)
            {
                query = query.Where(c => c.EventPlaceId== eventPlaceId);
            }
            var itemsCount = _context.EventCatalogTable.LongCountAsync();
            var items = await query
                                .OrderBy(c => c.Name)
                                .Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            items = ChangePictureUrl(items);
            var model = new PaginatedItemsViewModel
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount.Result,
                Data = items
            };
            return Ok(model);
        }

        private List<Event> ChangePictureUrl(List<Event> events)
        {
            events.ForEach(ev =>
                ev.PictureURL = ev.PictureURL.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["Externalbaseurl"]));

            return events;
        }
    }
}
