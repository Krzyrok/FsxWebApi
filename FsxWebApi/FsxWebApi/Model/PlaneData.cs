namespace FsxWebApi.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PlaneData
    {
        [DataMember(Name = "fuel_level")]
        public double FuelLevel { get; set; }

        [DataMember(Name = "speed")]
        public double Speed { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "engine_elapsed_time")]
        public double EngineElapsedTime { get; set; }
    }
}