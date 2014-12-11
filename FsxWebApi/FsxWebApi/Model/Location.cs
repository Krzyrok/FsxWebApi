namespace FsxWebApi.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Location
    {
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "altitude")]
        public double Altitude { get; set; }
    }
}