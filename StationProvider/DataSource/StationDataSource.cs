using System;
using System.Collections.Generic;
using Stations;

namespace StationProvider.DataSource
{
    public abstract class StationDataSource : IStationDataSource//IDisposable
    {
        //public abstract Dictionary<string, IStation> GetStations();
        public abstract IEnumerable<IStation> GetStations();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}