using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Stations;
using System.Linq;

namespace StationProvider
{
    public class TxtStationProvider : StationProvider
    {
        private bool _disposed;
        private readonly IStationDataSource _stationDataSource;

        private Lazy<Dictionary<string, IStation>> _stations;//= new Lazy<Dictionary<string, IStation>>(()=> ReadStationFromSource());

        private readonly object _lockObject= new object();

        public TxtStationProvider(IStationDataSource stationDataSource)
        {
            if (stationDataSource == null)
                throw new ArgumentNullException(nameof(stationDataSource));

            _stationDataSource = stationDataSource;

            _stations = new Lazy<Dictionary<string, IStation>>(ReadStationFromSource);
        }

		public override IEnumerable<IStation> FindStations(string namePattern)
		{
			if (string.IsNullOrEmpty(namePattern)) {
				throw new ArgumentException("Null Or Empty", nameof(namePattern));
			}

			namePattern = namePattern.Trim();

			return _stations.Value.Where(i => i.Key.StartsWith(namePattern, StringComparison.InvariantCultureIgnoreCase)).Select(i => i.Value);
		}

		public override IStation GetStation(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (_stations.Value.ContainsKey(name))
            {
                return _stations.Value[name];
            }

            lock (_lockObject)
            {
                if (_stations.Value.ContainsKey(name))
                {
                    return _stations.Value[name];
                }

                var newStations = ReadStationFromSource();

                _stations= new Lazy<Dictionary<string, IStation>>(() => newStations);

                if (_stations.Value.ContainsKey(name))
                {
                    return _stations.Value[name];
                }
            }

            throw new Exception("Station was not found");
        }

        private Dictionary<string, IStation> ReadStationFromSource()
        {
            var result = new Dictionary<string, IStation>();

            var stations = _stationDataSource.GetStations();

            foreach (var station in stations)
            {
                if (result.ContainsKey(station.Name))
                {
                    continue;
                }

                result.Add(station.Name, station);
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _stationDataSource?.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}
