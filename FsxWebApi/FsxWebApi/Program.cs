namespace FsxWebApi
{
    using System.Web.Http.SelfHost;

    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new HttpSelfHostConfiguration("http://localhost:8080");
            WebApiConfig.Register(configuration);
            WebApiStarter.StartServer(configuration);
			// git tests
        }
    }
}
