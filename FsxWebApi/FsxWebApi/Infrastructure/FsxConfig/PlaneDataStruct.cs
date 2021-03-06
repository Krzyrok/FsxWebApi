﻿namespace FsxWebApi.Infrastructure.FsxConfig
{
    using System.Runtime.InteropServices;

    // declaration of data structure - simconnect needs to know how to fill it/read it.
    // Sequential - position of field (in the struct) is connected to the position in memory
    // Pack - minimum size of the field (in bytes)
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PlaneDataStruct
    {
        public LocationStruct Location;
        public double Fuel;
        public double EngineElapsedTime; 
        public float Speed;
    };
}
