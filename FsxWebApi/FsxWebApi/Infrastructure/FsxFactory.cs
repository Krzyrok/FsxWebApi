namespace FsxWebApi.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Enums;
    using Microsoft.FlightSimulator.SimConnect;

    public static class FsxFactory
    {
        public static SimConnect GetSimConnectObject(FsxCommunicator communicator)
        {
            try
            {
                IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;

                // the constructor
                SimConnect simconnect = new SimConnect("User Requests", mainWindowHandle, Constants.WmUserSimconnect, null, 0);

                InitializeSimConnect(simconnect, communicator);

                return simconnect;

            }
            catch (COMException)
            {
                // exception handling
            }

            return null;
        }

        // Set up all the SimConnect data definitions and event handlers
        private static void InitializeSimConnect(SimConnect simconnect, FsxCommunicator fsxCommunicator)
        {
            try
            {
                // listen to quit (from FSX) events
                simconnect.OnRecvQuit += fsxCommunicator.Fsx_UserClosedFsxEventHandler;

                // listen to exceptions
                simconnect.OnRecvException += fsxCommunicator.Fsx_ExceptionEventHandler;

                // define a data structure
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(Definition.Plane, "Fuel Tank Center Quantity", "gallons", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(Definition.Plane, "Ground Velocity", "meters per second", SIMCONNECT_DATATYPE.FLOAT32, 0.0f, SimConnect.SIMCONNECT_UNUSED); ;

                // registering struct in simconnect
                simconnect.RegisterDataDefineStruct<PlaneDataStruct>(Definition.Plane);

                // catch a simobject data request
                simconnect.OnRecvSimobjectDataBytype += fsxCommunicator.Fsx_ReceiveDataEventHandler;
            }
            catch (COMException)
            {
            }
        } 
    }
}
