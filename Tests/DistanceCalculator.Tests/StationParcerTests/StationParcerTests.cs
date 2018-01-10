using System;
using DistanceCalculator.Tests.Stubs;
using NUnit.Framework;
using StationProvider.StationParcer;

namespace DistanceCalculator.Tests.StationParcerTests
{
	[TestFixture]
	public class StationParcerTests
	{
		[Test]
		public void Station_Parce_Success()
		{
			var stationId = StationData.StationIdStationDataString;
			var stationName = StationData.StationNameStationDataString;
			var stationLat = StationData.StationLatStationDataString;
			var stationLon = StationData.StationLonStationDataString;

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
			var inputDataString = StationData.StationDataStringNotEnoughData;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_TooMuchData_Failed()
		{
			var inputDataString = StationData.StationDataStringTooMuchData;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_InvalidId_Failed()
		{
			var inputDataString = StationData.StationDataStringInvalidId;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_SingleQuatedStationName_Success()
		{
			var stationName = StationData.StationNameStationDataString;
			var inputDataString = StationData.StationDataStringSingleQuotedStationName;

			var parcer = new StringStationParcer();

			var station = parcer.Parce(inputDataString);

			Assert.AreEqual(stationName, station.Name, "Station Name is not correct");
		}

		[Test]
		public void Station_Parce_DoubleQuatedStationName_Success()
		{
			var stationName = StationData.StationNameStationDataString;
			var inputDataString = StationData.StationDataStringDoubleQuatedStationName;

			var parcer = new StringStationParcer();

			var station = parcer.Parce(inputDataString);

			Assert.AreEqual(stationName, station.Name, "Station Name is not correct");
		}

		[Test]
		public void Station_Parce_EmptyStationName_Failed()
		{
			var inputDataString = StationData.StationDataStringInvalidNameEmpty;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_WhitespaceStationName_Failed()
		{
			var inputDataString = StationData.StationDataStringInvalidNameWhiteSpace;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_InvalidLat_Failed()
		{
			var inputDataString = StationData.StationDataStringInvalidLat;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

		[Test]
		public void Station_Parce_InvalidLon_Failed()
		{
			var inputDataString = StationData.StationDataStringInvalidLon;

			var parcer = new StringStationParcer();

			Assert.Throws<Exception>(() => parcer.Parce(inputDataString));
		}

        [Test]
	    public void Station_Parce_InputIsNull_Failed()
	    {
	        var parcer = new StringStationParcer();

	        Assert.Throws<ArgumentException>(() => parcer.Parce(null));
        }

	    [Test]
	    public void Station_Parce_InputIsEmptyString_Failed()
	    {
	        var parcer = new StringStationParcer();

	        Assert.Throws<ArgumentException>(() => parcer.Parce(string.Empty));
	    }

	    [Test]
	    public void Station_Parce_InputIsWhiteSpaceString_Failed()
	    {
	        var parcer = new StringStationParcer();

	        Assert.Throws<ArgumentException>(() => parcer.Parce("    "));
	    }
    }
}
