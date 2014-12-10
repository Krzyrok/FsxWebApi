namespace FsxWebApi.Infrastructure
{
    using System;
    using Enums;
    using Microsoft.FlightSimulator.SimConnect;
    using Model;

    public class FsxCommunicator
    {
        // SimConnect object
        private SimConnect _simConnect;
        private bool _receivedMessage = false;
        private PlaneData _planeData;

        public PlaneData GetLocation()
        {
            if (_simConnect == null)
                _simConnect = FsxFactory.GetSimConnectObject(this);

            if (_simConnect == null)
                return null;

            _simConnect.RequestDataOnSimObjectType(DataRequest.FromBrowser, Definition.Plane, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            
            // ReceiveMessage must be called to trigger the events. 
            do
            {
                try
                {
                    _simConnect.ReceiveMessage();
                }
                catch (Exception)
                {
                    return null;
                }
            } while (!_receivedMessage);

            _receivedMessage = false;

            return _planeData;
        }

        public void CloseConnection()
        {
            CloseFsxConnection();
        }

        public void Fsx_ReceiveDataEventHandler(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE fsxData)
        {

            switch ((DataRequest)fsxData.dwRequestID)
            {
                case DataRequest.FromBrowser:
                    PlaneDataStruct userPlaneData = (PlaneDataStruct)fsxData.dwData[0];

                    Location location = new Location
                    {
                        Latitude = userPlaneData.Latitude,
                        Longitude = userPlaneData.Longitude,
                        Altitude = userPlaneData.Altitude
                    };

                    _planeData = new PlaneData
                    {
                        FuelLevel = userPlaneData.Fuel,
                        Speed = userPlaneData.Speed,
                        Location = location
                    };

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _receivedMessage = true;
        }


        // The case where the user closes FSX
        public void Fsx_UserClosedFsxEventHandler(SimConnect sender, SIMCONNECT_RECV data)
        {
            CloseFsxConnection();
        }

        public void Fsx_ExceptionEventHandler(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            // handle exceptions (save to file/display message)
        }


        private void CloseFsxConnection()
        {
            if (_simConnect == null) return;

            _simConnect.Dispose();
            _simConnect = null;
        }
    }
}
