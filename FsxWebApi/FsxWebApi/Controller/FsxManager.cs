namespace FsxWebApi.Controller
{
    using Infrastructure;
    using Model;

    public class FsxManager
    {
        private readonly FsxCommunicator _fsxCommunicator = new FsxCommunicator();

        public PlaneData GetCurrentPlaneData()
        {
            PlaneData planeData = _fsxCommunicator.GetLocation();

            return planeData;
        }
    }
}
