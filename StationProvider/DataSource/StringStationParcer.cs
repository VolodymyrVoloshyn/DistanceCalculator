using System;
using Stations;
using System.Globalization;

namespace StationProvider
{
	public class StringStationParcer : IStationParcer<string>
	{
		public IStation Parce(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentException("Value cannot be null or empty.", nameof(input));
			}

			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(input));
			}

			var stationData = input.Split(',');

			if (stationData.Length != 9)
			{
				throw new Exception("Input station data is in invalid format");
			}

			int id;

			if (!int.TryParse(stationData[0], NumberStyles.Any, CultureInfo.InvariantCulture, out id))
			{
				throw new Exception("Input station data is in invalid format");
			}

			string name = stationData[1].Trim(' ', '"', '\'');

			if (string.IsNullOrEmpty(name)
				 || string.IsNullOrWhiteSpace(name))
			{
				throw new Exception("Input station data is in invalid format");
			}

			string description = stationData[2].Trim();

			double lat, lon;

			if (!double.TryParse(stationData[3], NumberStyles.Any, CultureInfo.InvariantCulture, out lat))
			{
				throw new Exception("Input station data is in invalid format");
			}

			if (!double.TryParse(stationData[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out lon))
			{
				throw new Exception("Input station data is in invalid format");
			}

			int? zoneId = null;
			int i;

			if (int.TryParse(stationData[5].Trim(), out i))
			{
				zoneId = i;
			}

			string stopUrl = stationData[6].Trim();

			int locationType;

			if (!int.TryParse(stationData[7], NumberStyles.Any, CultureInfo.InvariantCulture, out locationType))
			{
				throw new Exception("Input station data is in invalid format");
			}

			string parentId = stationData[8].Trim();
			
			return new Station(id, name, lat, lon);
		}
	}
}
