using System.Net;

class Program
{
    static void Main(string[] args)
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8888/");
        listener.Start();
        Console.WriteLine("Listening...");

        while (true)
        {
            var context = listener.GetContext();
            var request = context.Request;
            var response = context.Response;

            string resource = request.Url.AbsolutePath.Trim('/');
            Console.WriteLine($"Received request for {resource}");

            switch (resource)
            {
                case "MyName":
                    GetMyName(response);
                    break;
                case "MyNameByHeader":
                    GetMyNameByHeader(response);
                    break;
                case "MyNameByCookies":
                    MyNameByCookies(response);
                    break;
                case "Information":
                    response.StatusCode = (int)HttpStatusCode.Continue;
                    break;
                case "Success":
                    response.StatusCode = (int)HttpStatusCode.OK;
                    break;
                case "Redirection":
                    response.StatusCode = (int)HttpStatusCode.Redirect;
                    break;
                case "ClientError":
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case "ServerError":
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    response.StatusCode = 404;
                    {
                        using var writer = new StreamWriter(response.OutputStream);
                        writer.WriteLine("Not Found");
                    }
                    break;
            }

            Console.WriteLine($"Responded with status code {response.StatusCode}");
            response.Close();
        }
    }

    static void GetMyName(HttpListenerResponse response)
    {
        string name = "Firdavs";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(name);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }

    static void GetMyNameByHeader(HttpListenerResponse response)
    {
        response.Headers.Add("X-MyName", "Firdavs");
        response.StatusCode = (int)HttpStatusCode.OK;
        response.Close();
    }

    static void MyNameByCookies(HttpListenerResponse response)
    {
        response.Cookies.Add(new Cookie("MyName", "Firdavs"));
        response.StatusCode = (int)HttpStatusCode.OK;
        response.Close();
    }
}
