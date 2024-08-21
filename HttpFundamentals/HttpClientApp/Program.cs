class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyName/");

        if (response.IsSuccessStatusCode)
        {
            string name = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Name from Listener: {name}");
        }
        else
        {
            Console.WriteLine($"Failed to get response: {response.StatusCode}");
        }

        // receive name by header
        HttpResponseMessage headerResponse = await client.GetAsync("http://localhost:8888/MyNameByHeader/");
        if (headerResponse.Headers.TryGetValues("X-MyName", out var headerValues))
        {
            Console.WriteLine($"Name from Header: {headerValues.First()}");
        }

        // receive name by cookies
        HttpResponseMessage cookieResponse = await client.GetAsync("http://localhost:8888/MyNameByCookies/");
        var cookies = cookieResponse.Headers.GetValues("Set-Cookie");
        foreach (var cookie in cookies)
        {
            if (cookie.StartsWith("MyName"))
            {
                string name = cookie.Split('=')[1];
                Console.WriteLine($"Name from Cookies: {name}");
            }
        }

        // status code cases
        string[] urls = {
            "http://localhost:8888/Success/",
            "http://localhost:8888/Redirection/",
            "http://localhost:8888/ClientError/",
            "http://localhost:8888/ServerError/",
            "http://localhost:8888/Information/"
        };

        foreach (var url in urls)
        {
            try
            {
                HttpResponseMessage responseItem = await client.GetAsync(url);
                Console.WriteLine($"URL: {url}, Status Code: {responseItem.StatusCode}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine($"Request to {url} timed out for http://localhost:8888/Information/");
            }
        }
    }
}
