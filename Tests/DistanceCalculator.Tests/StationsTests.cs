using System;
using NUnit.Framework;
using Stations;
using DistanceCalculator.Tests.Stubs;

namespace DistanceCalculator.Tests
{
	[TestFixture]
	public class StationsTests
	{
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
	}
}
