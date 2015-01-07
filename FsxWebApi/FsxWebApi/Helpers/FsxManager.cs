namespace FsxWebApi.Helpers
{
    using Infrastructure;
    using Model;

    public class FsxManager
    {
        private readonly FsxCommunicator _fsxCommunicator = new FsxCommunicator(new Logger());

        public PlaneData GetCurrentPlaneData()
        {
            PlaneData planeData = _fsxCommunicator.GetPlaneData();

            return planeData;
        }

        public bool SetNewPlaneLocation(Location planeLocation)
        {
            return _fsxCommunicator.SetPlaneLocation(planeLocation);
        }
    }
}
