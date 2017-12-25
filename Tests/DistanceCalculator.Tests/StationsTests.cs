using System;
using NUnit.Framework;
using Stations;
using DistanceCalculator.Tests.Stubs;

namespace DistanceCalculator.Tests
{
	[TestFixture]
	public class StationsTests
	{
		private string WhitespaceString = " ";
		private string WhitespacesOnlyString = "           ";

		[Test]
		public void Create_Station_Success()
		{
			// 
			var stationId = StationData.StationId1;
			var stationName = StationData.StationName1;
			var stationLat = StationData.Station1Lat;
			var stationLon = StationData.Station1Lon;

			// Act
			var station = new Station(stationId, stationName, stationLat, stationLon);

			Assert.AreEqual(stationId, station.Id, "Station Id");
			Assert.AreEqual(stationName, station.Name, "Station Name");
			Assert.AreEqual(stationLat, station.Lat, "Station Lat");
			Assert.AreEqual(stationLon, station.Lon, "Station Lon");
		}

		[Test]
		public void Create_Station_EmptyName_Failed()
		{
			var stationId = StationData.StationId1;
			var stationName = string.Empty;
			var stationLat = StationData.Station1Lat;
			var stationLon = StationData.Station1Lon;

			// Act
			Assert.Throws<ArgumentException>(() => new Station(stationId, stationName, stationLat, stationLon));

		}

		[Test]
		public void Create_Station_NameIsNull_Failed()
		{
			var stationId = StationData.StationId1;
			string stationName = null;
			var stationLat = StationData.Station1Lat;
			var stationLon = StationData.Station1Lon;

			// Act
			Assert.Throws<ArgumentNullException>(() => new Station(stationId, stationName, stationLat, stationLon));

		}

		[Test]
		public void Create_Station_WhitespaceName_Failed()
		{
			var stationId = StationData.StationId1;
			var stationName = WhitespaceString;
			var stationLat = StationData.Station1Lat;
			var stationLon = StationData.Station1Lon;

			// Act
			Assert.Throws<ArgumentException>(() => new Station(stationId, stationName, stationLat, stationLon));

		}

		[Test]
		public void Create_Station_WhitespacesOnlyName_Failed()
		{
			var stationId = StationData.StationId1;
			var stationName = WhitespacesOnlyString;
			var stationLat = StationData.Station1Lat;
			var stationLon = StationData.Station1Lon;

			// Act
			Assert.Throws<ArgumentException>(() => new Station(stationId, stationName, stationLat, stationLon));

		}
	}
}
