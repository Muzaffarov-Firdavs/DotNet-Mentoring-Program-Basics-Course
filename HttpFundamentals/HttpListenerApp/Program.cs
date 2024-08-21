namespace HttpListenerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IHttpServer server = new HttpServer("http://localhost:8888/");
            server.Start();
        }
    }
}
