using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Stations;
using System.Linq;

namespace StationProvider
{
    public class TxtStationProvider : IStationProvider
    {
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

		public IEnumerable<IStation> FindStations(string namePattern)
		{
			if (string.IsNullOrEmpty(namePattern)) {
				throw new ArgumentException("Null Or Empty", nameof(namePattern));
			}

			namePattern = namePattern.Trim();

			return _stations.Value.Where(i => i.Key.StartsWith(namePattern, StringComparison.InvariantCultureIgnoreCase)).Select(i => i.Value);
		}

		public IStation GetStation(string name)
        {
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
            return _stationDataSource.GetStations();
        }
    }
}
