using System.Net;

namespace HttpListenerApp
{
    public class HttpServer : IHttpServer
    {
        private readonly HttpListener _listener;

        public HttpServer(string prefix)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                var context = _listener.GetContext();
                ProcessRequest(context);
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            string resource = request.Url.AbsolutePath.Trim('/');
            Console.WriteLine($"Received request for {resource}");

            if (!HandleResource(resource, response))
            {
                HandleNotFound(response);
            }

            Console.WriteLine($"Responded with status code {response.StatusCode}");
            response.Close();
        }

        private bool HandleResource(string resource, HttpListenerResponse response)
        {
            switch (resource)
            {
                case "MyName":
                    RespondWithText(response, "Firdavs");
                    return true;
                case "MyNameByHeader":
                    response.Headers.Add("X-MyName", "Firdavs");
                    response.StatusCode = (int)HttpStatusCode.OK;
                    return true;
                case "MyNameByCookies":
                    response.Cookies.Add(new Cookie("MyName", "Firdavs"));
                    response.StatusCode = (int)HttpStatusCode.OK;
                    return true;
                case "Information":
                    response.StatusCode = (int)HttpStatusCode.Continue;
                    return true;
                case "Success":
                    response.StatusCode = (int)HttpStatusCode.OK;
                    return true;
                case "Redirection":
                    response.StatusCode = (int)HttpStatusCode.Redirect;
                    return true;
                case "ClientError":
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return true;
                case "ServerError":
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return true;
                default:
                    return false;
            }
        }

        private void RespondWithText(HttpListenerResponse response, string text)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void HandleNotFound(HttpListenerResponse response)
        {
            response.StatusCode = 404;
            using var writer = new StreamWriter(response.OutputStream);
            writer.WriteLine("Not Found");
        }
    }
}
