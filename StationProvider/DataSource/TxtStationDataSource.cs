using System;
using System.Collections.Generic;
using System.IO;
using Stations;

namespace StationProvider
{
	public class TxtStationDataSource : IStationDataSource
	{
		private readonly string _stationListFilePath;
		private readonly IStationParcer<string> _parcer;

		public TxtStationDataSource(string stationListFilePath, IStationParcer<string> parcer)
		{
			_stationListFilePath = stationListFilePath ?? throw new ArgumentNullException(nameof(stationListFilePath));
			_parcer = parcer ?? throw new ArgumentNullException(nameof(parcer));
		}

		public Dictionary<string, IStation> GetStations()
		{
			if (!File.Exists(_stationListFilePath))
			{
				throw new Exception("Stations file is not found;");
			}

			Dictionary<string, IStation> stations = new Dictionary<string, IStation>();

			using (StreamReader sr = new StreamReader(_stationListFilePath))
			{
				if (!sr.EndOfStream)
				{
					// skip first line
					sr.ReadLine();
				}
				else
				{
					throw new Exception("Stations file is empty;");
				}

				while (sr.Peek() >= 0)
				{
					var strLine = sr.ReadLine();

					var station = _parcer.Parce(strLine);
					if (station == null)
					{
						throw new Exception("Can't parce station. Station can't be null.");
					}

					if (stations.ContainsKey(station.Name))
					{
						continue;
						// or throw ?
					}

					stations.Add(station.Name, station);
				}
			}

			return stations;
		}
	}
}
 