using System;
using System.Collections.Generic;
using Stations;

namespace StationProvider
{
    public interface IStationDataSource : IDisposable
    {
        Dictionary<string, IStation> GetStations();
    }
}