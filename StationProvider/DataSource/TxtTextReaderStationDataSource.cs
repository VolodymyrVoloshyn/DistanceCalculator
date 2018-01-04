using System;
using System.Collections.Generic;
using System.IO;
using Stations;

namespace StationProvider
{
	public class TxtTextReaderStationDataSource : StationDataSource
	{
		private readonly IStationParcer<string> _parcer;
		private readonly TextReader _textReader;
	    private bool _disposed;

		public TxtTextReaderStationDataSource(TextReader textReader, IStationParcer<string> parcer)
		{
			_textReader = textReader ?? throw new ArgumentNullException(nameof(textReader));
			_parcer = parcer ?? throw new ArgumentNullException(nameof(parcer));
		}

		public override Dictionary<string, IStation> GetStations()
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

	    protected override void Dispose(bool disposing)
	    {
	        if (_disposed)
	        {
                return;
	        }

	        if (disposing)
	        {
	            _textReader?.Dispose();
	        }

	        _disposed = true;

	        base.Dispose(disposing);
	    }
	}
}
