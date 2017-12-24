using System.Collections.Generic;
using Stations;

namespace StationProvider
{
    public interface IStationDataSource
    {
        Dictionary<string, IStation> GetStations();
    }
}