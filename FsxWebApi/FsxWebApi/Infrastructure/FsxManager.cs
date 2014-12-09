namespace FsxWebApi.Infrastructure
{
    using Model;

    public class FsxManager
    {
        FsxCommunicator _fsxCommunicator;
        public PlaneData GetCurrentPlaneData()
        {
            if (_fsxCommunicator == null)
                _fsxCommunicator = new FsxCommunicator();


            PlaneData planeData = _fsxCommunicator.GetLocation();

            return planeData;
        }
    }
}
