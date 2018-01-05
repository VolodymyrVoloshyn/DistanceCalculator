using System;
using System.Collections.Generic;
using System.IO;
using Stations;

namespace StationProvider
{
    /// <summary>
    /// All dependencies are provided, but GetStations can be called just once, because of TextReader which supports forward read only.
    /// </summary>
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

		public override IEnumerable<IStation> GetStations()
		{
			List<IStation> stations = new List<IStation>();

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

				stations.Add(station);
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
