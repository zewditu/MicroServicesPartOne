using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class TicketOrderService : ITicketOrderService
    {
        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IConfiguration _config;
        //private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly ILogger _logger;
        public TicketOrderService(IConfiguration config,
            //IHttpContextAccessor httpContextAccesor,
            IHttpClient httpClient, ILoggerFactory logger)
        {
            _remoteServiceBaseUrl = $"{config["TicketOrderUrl"]}/api/ticketorder";
            _config = config;
            //_httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
            _logger = logger.CreateLogger<TicketOrderService>();
        }

        public async Task<TicketOrder> GetTicketOrder(string orderId)
        {
            //var token = await GetUserTokenAsync();
            var getOrderUri = ApiPaths.TicketOrder.GetTicketOrder(_remoteServiceBaseUrl, orderId);

            var dataString = await _apiClient.GetAsync(getOrderUri); //token
            _logger.LogInformation("DataString: " + dataString);
            var response = JsonConvert.DeserializeObject<TicketOrder>(dataString);

            return response;
        }

        public async Task<int> CreateTicketOrder(TicketOrder order)
        {
            //var token = await GetUserTokenAsync();

            var addNewOrderUri = ApiPaths.TicketOrder.AddNewTicketOrder(_remoteServiceBaseUrl);
            _logger.LogDebug(" OrderUri " + addNewOrderUri);


            var response = await _apiClient.PostAsync(addNewOrderUri, order); //pass token here 
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error creating order, try later.");
            }

            // response.EnsureSuccessStatusCode();
            var jsonString = response.Content.ReadAsStringAsync();

            jsonString.Wait();
            _logger.LogDebug("response " + jsonString);
            dynamic data = JObject.Parse(jsonString.Result);
            string value = data.orderId;
            return Convert.ToInt32(value);
        }

        //private async Task<string> GetUserTokenAsync()
        //{
        //    var context = _httpContextAccesor.HttpContext;
        //    return await context.GetTokenAsync("access_token");
        //}

    }
}