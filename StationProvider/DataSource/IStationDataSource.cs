using System;
using System.Collections.Generic;
using Stations;

namespace StationProvider.DataSource
{
     public interface IStationDataSource : IDisposable
    {
        //Dictionary<string, IStation> GetStations();
        IEnumerable<IStation> GetStations();
    }
}