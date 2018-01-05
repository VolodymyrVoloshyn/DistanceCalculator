using System;
using System.Collections.Generic;
using Stations;

namespace StationProvider
{
    public interface IStationProvider : IDisposable
    {
        IStation GetStation(string name);
        IEnumerable<IStation> FindStations(string namePattern);
    }
}
