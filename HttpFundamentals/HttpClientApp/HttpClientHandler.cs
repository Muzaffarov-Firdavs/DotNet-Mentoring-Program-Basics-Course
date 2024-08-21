namespace HttpClientApp
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:8888/";

        public HttpClientHandler()
        {
            _httpClient = new HttpClient();
        }

        public async Task ExecuteRequestsAsync()
        {
            await HandleNameRequestAsync("MyName/");
            await HandleNameByHeaderRequestAsync("MyNameByHeader/");
            await HandleNameByCookiesRequestAsync("MyNameByCookies/");
            await HandleStatusCodeRequestsAsync();
        }

        private async Task HandleNameRequestAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}{endpoint}");
            if (response.IsSuccessStatusCode)
            {
                string name = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Name from Listener: {name}");
            }
            else
            {
                Console.WriteLine($"Failed to get response: {response.StatusCode}");
            }
        }

        private async Task HandleNameByHeaderRequestAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}{endpoint}");
            if (response.Headers.TryGetValues("X-MyName", out var headerValues))
            {
                Console.WriteLine($"Name from Header: {headerValues.First()}");
            }
        }

        private async Task HandleNameByCookiesRequestAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}{endpoint}");
            var cookies = response.Headers.GetValues("Set-Cookie");

            foreach (var cookie in cookies)
            {
                if (cookie.StartsWith("MyName"))
                {
                    string name = cookie.Split('=')[1];
                    Console.WriteLine($"Name from Cookies: {name}");
                }
            }
        }

        private async Task HandleStatusCodeRequestsAsync()
        {
            string[] urls = { "Success/", "Redirection/", "ClientError/", "ServerError/", "Information/" };

            foreach (var endpoint in urls)
            {
                await HandleStatusCodeRequestAsync(endpoint);
            }
        }

        private async Task HandleStatusCodeRequestAsync(string endpoint)
        {
            var url = $"{BaseUrl}{endpoint}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                Console.WriteLine($"URL: {url}, Status Code: {response.StatusCode}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine($"Request to {url} timed out for {BaseUrl}Information/");
            }
        }
    }
}
