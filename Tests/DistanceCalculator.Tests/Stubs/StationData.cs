using System;
using System.Globalization;

namespace DistanceCalculator.Tests.Stubs
{
	public static class StationData
	{
		public static int StationId1 { get { return 4321; } }
		public static string StationName1 { get { return "Station1"; } }
		public static double Station1Lat { get { return 34.6788; } }
		public static double Station1Lon { get { return 22.2323; } }

		public static int StationId_StationDataString = 55555;
		public static string StationName_StationDataString = "Station Number One";
		public static double StationLat_StationDataString = 40.7;
		public static double StationLon_StationDataString = 73.0133;

		public static string StationDataString = string.Format(CultureInfo.InvariantCulture, "{0}, {1} , Station Description ,  {2}, {3},,,0,",
			StationId_StationDataString,
			StationName_StationDataString,
			StationLat_StationDataString,
			StationLon_StationDataString);

		public static string StationDataString_NotEnoughData = "2323, Station name,,";
		public static string StationDataString_TooMuchData = "2333, Station name,,,,  33.3, -843.00,,,0,";

		public static string StationDataString_InvalidId = "Aa, Station name , Station Description ,  11.11, 22.22,,,0,";

		public static string StationDataString_SingleQuotedStationName = string.Format(CultureInfo.InvariantCulture, "1234, '{0}' , Station Description ,  11.11, 22.22,,,0,",
			StationName_StationDataString);

		public static string StationDataString_DoubleQuatedStationName = string.Format(CultureInfo.InvariantCulture, "1234, \"{0}\" , Station Description ,  11.11, 22.22,,,0,",
			StationName_StationDataString);

		public static string StationDataString_InvalidName_Empty = "1123,, Station Description ,  11.11, 22.22,,,0,";
		public static string StationDataString_InvalidName_WhiteSpace = "1123,   , Station Description ,  11.11, 22.22,,,0,";

		public static string StationDataString_InvalidLat = "1123, Station name  , Station Description ,  11 11, 22.22,,,0,";
		public static string StationDataString_InvalidLon = "1123, Station name  , Station Description ,  11.11, 22q22,,,0,";
	}
}
