using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _client;
        public EventCatalogService(IConfiguration config, IHttpClient client)
        {
            _baseUrl = $"{config["EventCatalogUrl"]}/api/event";
            _client = client;
        }
        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var categoryUri = ApiPaths.Events.GetAllCategories(_baseUrl);
            var dataString = await _client.GetAsync(categoryUri);

            var items = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = "All",
                    Selected = true
                }
            };

            var categories = JArray.Parse(dataString);
            foreach (var category in categories)
            {
                items.Add(new SelectListItem
                {
                    Value = category.Value<string>("id"),
                    Text = category.Value<string>("name"),
                });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetPlacesAsync()
        {
            var categoryUri = ApiPaths.Events.GetAllPlaces(_baseUrl);
            var dataString = await _client.GetAsync(categoryUri);

            var items = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = "All",
                    Selected = true
                }
            };

            var places = JArray.Parse(dataString);
            foreach (var place in places)
            {
                items.Add(new SelectListItem
                {
                    Value = place.Value<string>("id"),
                    Text = place.Value<string>("name"),
                });
            }
            return items;
        }

        public async Task<EventCatalog> GetEventCatalogItemsAsync(int pageNumber, int pageSize, int? category, int? place)
        {
            var catalogItemsUri = ApiPaths.Events.GetAllEvents(_baseUrl, pageNumber, pageSize, category, place);
            //  http://eventcatalogapi/api/event/items?pageindex=0&pagesize=10

            var dataString = await _client.GetAsync(catalogItemsUri);
            return JsonConvert.DeserializeObject<EventCatalog>(dataString);
        }
    }
}
