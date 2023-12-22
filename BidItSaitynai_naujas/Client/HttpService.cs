using BidItSaitynai_naujas.Client.Shared;

namespace BidItSaitynai_naujas.Client
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
        }
        public HttpClient GetClient() { 

            return _httpClient;
        }
        public void SetHeader(string mainLayout)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + mainLayout);
        }
    }
}
