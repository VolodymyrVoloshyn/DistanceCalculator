using System;
using System.Collections.Generic;
using System.IO;
using Stations;

namespace StationProvider
{
	public class TxtTextReaderStationFactoryDataSource : IStationDataSource
	{
		private readonly IStationParcer<string> _parcer;
		private Func<TextReader> _textReaderFactory;

		public TxtTextReaderStationFactoryDataSource(Func<TextReader> textReaderFactory, IStationParcer<string> parcer)
		{
			_textReaderFactory = textReaderFactory ?? throw new ArgumentNullException(nameof(textReaderFactory));
			_parcer = parcer ?? throw new ArgumentNullException(nameof(parcer));
		}

		public Dictionary<string, IStation> GetStations()
		{
			var textReader = _textReaderFactory();

			if (textReader == null) {
				throw new Exception("Reader returned from factory is null.");
			}

			Dictionary<string, IStation> stations = new Dictionary<string, IStation>();

			if (textReader.Peek()!= -1)
			{
				// skip first line
				textReader.ReadLine();
			}
			else
			{
				throw new Exception("Can't read from reader.");
			}

			while (textReader.Peek() >= 0)
			{
				var strLine = textReader.ReadLine();

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
