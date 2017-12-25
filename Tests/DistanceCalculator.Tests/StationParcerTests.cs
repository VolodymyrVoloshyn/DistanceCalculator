using DistanceCalculator.Tests.Stubs;
using NUnit.Framework;
using StationProvider;
using System;

namespace DistanceCalculator.Tests
{
	[TestFixture]
	public class StationParcerTests
	{
		[Test]
		public void Station_Parce_Success()
		{
			var stationId = StationData.StationId_StationDataString;
			var stationName = StationData.StationName_StationDataString;
			var stationLat = StationData.StationLat_StationDataString;
			var stationLon = StationData.StationLon_StationDataString;

			var inputDataString = StationData.StationDataString;

			var parcer = new StringStationParcer();

			var station = parcer.Parce(inputDataString);

			Assert.AreEqual(stationId, station.Id, "Station Id is not correct");
			Assert.AreEqual(stationName, station.Name, "Station Name is not correct");
			Assert.AreEqual(stationLat, station.Lat, "Station Lat is not correct");
			Assert.AreEqual(stationLon, station.Lon, "Station Lon is not correct");
		}

		[Test]
		public void Station_Parce_NotEnoughData_Failed()
		{
			var inputDataString = StationData.StationDataString_NotEnoughData;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_TooMuchData_Failed()
		{
			var inputDataString = StationData.StationDataString_TooMuchData;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_InvalidId_Failed()
		{
			var inputDataString = StationData.StationDataString_InvalidId;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_SingleQuatedStationName_Success()
		{
			var stationName = StationData.StationName_StationDataString;
			var inputDataString = StationData.StationDataString_SingleQuotedStationName;

			var parcer = new StringStationParcer();

			var station = parcer.Parce(inputDataString);

			Assert.AreEqual(stationName, station.Name, "Station Name is not correct");
		}

		[Test]
		public void Station_Parce_DoubleQuatedStationName_Success()
		{
			var stationName = StationData.StationName_StationDataString;
			var inputDataString = StationData.StationDataString_DoubleQuatedStationName;

			var parcer = new StringStationParcer();

			var station = parcer.Parce(inputDataString);

			Assert.AreEqual(stationName, station.Name, "Station Name is not correct");
		}

		[Test]
		public void Station_Parce_EmptyStationName_Failed()
		{
			var inputDataString = StationData.StationDataString_InvalidName_Empty;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_WhitespaceStationName_Failed()
		{
			var inputDataString = StationData.StationDataString_InvalidName_WhiteSpace;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_InvalidLat_Failed()
		{
			var inputDataString = StationData.StationDataString_InvalidLat;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_InvalidLon_Failed()
		{
			var inputDataString = StationData.StationDataString_InvalidLon;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}
	}
}
