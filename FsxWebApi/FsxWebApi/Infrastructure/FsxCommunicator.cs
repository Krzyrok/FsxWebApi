﻿namespace FsxWebApi.Infrastructure
{
    using System;
    using FsxConfig;
    using FsxConfig.Enums;
    using Interfaces;
    using Microsoft.FlightSimulator.SimConnect;
    using Model;

    public class FsxCommunicator
    {
        private readonly ILogger _logger;
        // SimConnect object
        private SimConnect _simConnect;
        private bool _receivedMessage = false;
        private PlaneData _planeData;

        public FsxCommunicator(ILogger logger)
        {
            _logger = logger;
        }

        public PlaneData GetPlaneData()
        {
            if (!IsConnectedToFsx()) 
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

        public bool SetPlaneLocation(Location newLocation)
        {
            if (!IsConnectedToFsx())
                return false;

            LocationStruct locationStructForFsx;
            locationStructForFsx.Altitude = newLocation.Altitude;
            locationStructForFsx.Latitude = newLocation.Latitude;
            locationStructForFsx.Longitude = newLocation.Longitude;

            _simConnect.SetDataOnSimObject(Definition.Location, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, locationStructForFsx);

            return true;
        }

        public void Fsx_ReceiveDataEventHandler(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE fsxData)
        {

            switch ((DataRequest)fsxData.dwRequestID)
            {
                case DataRequest.FromBrowser:
                    PlaneDataStruct userPlaneData = (PlaneDataStruct)fsxData.dwData[0];

                    Location location = new Location
                    {
                        Latitude = userPlaneData.Location.Latitude,
                        Longitude = userPlaneData.Location.Longitude,
                        Altitude = userPlaneData.Location.Altitude
                    };

                    _planeData = new PlaneData
                    {
                        FuelLevel = userPlaneData.Fuel,
                        Speed = userPlaneData.Speed,
                        Location = location,
                        EngineElapsedTime = userPlaneData.EngineElapsedTime
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
            _logger.Log("User has closed FSX.");
        }

        public void Fsx_ExceptionEventHandler(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            // handle exceptions (save to file/display message)
            _logger.Log("Error during connection with FSX.");
        }

        private bool IsConnectedToFsx()
        {
            if (_simConnect == null)
                _simConnect = FsxFactory.GetSimConnectObject(this);

            if (_simConnect == null)
            {
                _logger.Log("Couldn't connect to the FSX.");
                return false;
            }
            return true;
        }

        private void CloseFsxConnection()
        {
            if (_simConnect == null) return;

            _simConnect.Dispose();
            _simConnect = null;
        }
    }
}
