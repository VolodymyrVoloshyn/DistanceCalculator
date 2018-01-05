using System;
using System.Collections.Generic;
using System.IO;
using Stations;

namespace StationProvider
{
    /// <summary>
    /// All dependencies are provided, GetStations can be called many times
    /// </summary>
	public class TxtTextReaderStationFactoryDataSource : StationDataSource
	{
		private readonly IStationParcer<string> _parcer;
		private readonly Func<TextReader> _textReaderFactory;

		public TxtTextReaderStationFactoryDataSource(Func<TextReader> textReaderFactory, IStationParcer<string> parcer)
		{
			_textReaderFactory = textReaderFactory ?? throw new ArgumentNullException(nameof(textReaderFactory));
			_parcer = parcer ?? throw new ArgumentNullException(nameof(parcer));
		}

		public override IEnumerable<IStation> GetStations()
		{
		    List<IStation> stations;

		    using (var textReader = _textReaderFactory())
		    {
                if (textReader == null)
                {
                    throw new Exception("Reader returned from factory is null.");
                }

                stations = new List<IStation>();

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

		            stations.Add(station);
		        }
		    }

		    return stations;
		}
	}
}
