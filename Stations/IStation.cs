using System;

namespace Stations
{
    public interface IStation
    {
        int Id { get; }

        string Name { get; }
        double Lat { get; }
        double Lon { get; }
    }
}
