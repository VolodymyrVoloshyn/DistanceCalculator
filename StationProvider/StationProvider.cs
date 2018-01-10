using System;
using System.Collections.Generic;
using Stations;

namespace StationProvider
{
    public abstract class StationProvider : IStationProvider
    {
        public abstract IStation GetStation(string name);
        public abstract IEnumerable<IStation> FindStations(string namePattern);

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
