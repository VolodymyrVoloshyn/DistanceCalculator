using System;
using System.Collections.Generic;
using System.IO;
using Stations;

namespace StationProvider
{
	public class TxtTextReaderStationDataSource : IStationDataSource
	{
		private readonly IStationParcer<string> _parcer;
		private TextReader _textReader;

		public TxtTextReaderStationDataSource(TextReader textReader, IStationParcer<string> parcer)
		{
			_textReader = textReader ?? throw new ArgumentNullException(nameof(textReader));
			_parcer = parcer ?? throw new ArgumentNullException(nameof(parcer));
		}

		public Dictionary<string, IStation> GetStations()
		{
			Dictionary<string, IStation> stations = new Dictionary<string, IStation>();

			if (_textReader.Peek()!= -1)
			{
				// skip first line
				_textReader.ReadLine();
			}
			else
			{
				throw new Exception("Can't read from reader.");
			}

			while (_textReader.Peek() >= 0)
			{
				var strLine = _textReader.ReadLine();

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

			return stations;
		}
	}
}
