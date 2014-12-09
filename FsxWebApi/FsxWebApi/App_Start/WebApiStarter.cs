namespace FsxWebApi
{
    using System;
    using System.Web.Http.SelfHost;

    public static class WebApiStarter
    {
        public static void StartServer(HttpSelfHostConfiguration configuration)
        {
            using (var server = new HttpSelfHostServer(configuration))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Address: " + configuration.BaseAddress);
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
