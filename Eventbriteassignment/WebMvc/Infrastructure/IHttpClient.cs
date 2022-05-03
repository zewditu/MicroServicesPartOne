namespace WebMvc.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetAsync(string uri,
    string authorizationToken = null,
    string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> PostAsync<T>(string uri,
            T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> PutAsync<T>(string uri,
            T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");
    }
}
