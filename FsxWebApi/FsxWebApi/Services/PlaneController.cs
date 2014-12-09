namespace FsxWebApi.Services
{
    using Infrastructure;
    using Model;
    using System.Web.Http;

    public class PlaneController : ApiController
    {
        private readonly FsxManager _fsxManager = new FsxManager();

        // GET: api/Plane
        public PlaneData Get()
        {
            return _fsxManager.GetCurrentPlaneData();
        }

        // POST: api/Plane
        public void Post(Location newLocation)
        {
            // Pass the values to the FSX
        }
    }
}
