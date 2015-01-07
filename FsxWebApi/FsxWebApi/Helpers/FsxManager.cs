namespace FsxWebApi.Helpers
{
    using Infrastructure;
    using Model;

    public class FsxManager
    {
        private readonly FsxCommunicator _fsxCommunicator = new FsxCommunicator(new Logger());

        public PlaneData GetCurrentPlaneData()
        {
            PlaneData planeData = _fsxCommunicator.GetLocation();

            return planeData;
        }

        public void SetNewPlaneLocation(Location planeLocation)
        {
            
        }
    }
}
