using System;

namespace Stations
{
	public class Station : IStation
	{
		public Station(int id, string name, double lat, double lon)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name can't be empty", nameof(name));
			}

			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name can't contain whitespaces only", nameof(name));
			}

			Id = id;
			Name = name;
			Lat = lat;
			Lon = lon;
		}

		public int Id { get; }

		public string Name { get; }

		public double Lat { get; }

		public double Lon { get; }
	}
}
