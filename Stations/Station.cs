using System;

namespace Stations
{
    public class Station : IStation
    {
        public Station(int id, string name, double lat, double lon)
        {
            Id = id;
            Name = name;
            Lat = lat;
            Lon = lon;
        }

        public int Id { get; private set; }
        
        public string Name { get; private set; }

        public double Lat { get; private set; }

        public double Lon { get; private set; }
    }
}
