using System;
using System.Collections.Generic;
using System.IO;
using Stations;

namespace StationProvider
{
    /// <summary>
    /// Can't be unit tested, because it creates StreamReader inside.
    /// </summary>
	public class TxtStationDataSource : StationDataSource
	{
		private readonly string _stationListFilePath;
		private readonly IStationParcer<string> _parcer;

		public TxtStationDataSource(string stationListFilePath, IStationParcer<string> parcer)
		{
			_stationListFilePath = stationListFilePath ?? throw new ArgumentNullException(nameof(stationListFilePath));
			_parcer = parcer ?? throw new ArgumentNullException(nameof(parcer));
		}

		public override IEnumerable<IStation> GetStations()
		{
			if (!File.Exists(_stationListFilePath))
			{
				throw new Exception("Stations file is not found;");
			}

			List<IStation> stations = new List<IStation>();

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

					stations.Add(station);
				}
			}

			return stations;
		}
	}
}
 