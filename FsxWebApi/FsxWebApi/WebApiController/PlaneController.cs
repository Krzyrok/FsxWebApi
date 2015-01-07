namespace FsxWebApi.WebApiController
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Helpers;
    using Model;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlaneController : ApiController
    {
        private readonly FsxManager _fsxManager = new FsxManager();

        // GET: api/Plane
        public IHttpActionResult Get()
        {
            //return ActionRes;
            PlaneData planeData = _fsxManager.GetCurrentPlaneData();

            if (planeData == null)
            {
                return NotFound();
            }
            return Ok(planeData);
        }

        // POST: api/Plane
        public IHttpActionResult Post(Location newLocation)
        {
            // Pass the values to the FSX

            return Ok();
        }
    }
}
