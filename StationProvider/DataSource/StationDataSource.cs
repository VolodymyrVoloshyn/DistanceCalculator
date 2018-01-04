using System;
using System.Collections.Generic;
using Stations;

namespace StationProvider
{
    public abstract class StationDataSource : IStationDataSource//IDisposable
    {
        //Dictionary<string, IStation> GetStations();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract Dictionary<string, IStation> GetStations();

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}