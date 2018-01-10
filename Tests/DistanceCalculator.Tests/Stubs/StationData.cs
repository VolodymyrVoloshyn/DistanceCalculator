using System.Globalization;
using Stations;

namespace DistanceCalculator.Tests.Stubs
{
    public static class StationData
    {
        public static int StationId1 => 4321;
        public static string StationName1 => "Station1";
        public static string StationName2 => "Station2";
        public static string StationName3 => "Station3";
        public static string StationName4 => "Station4";

        public static string StationNamePattern1 => "Stati";

        public static double Station1Lat => 34.6788;
        public static double Station1Lon => 22.2323;

        public static int StationIdStationDataString = 55555;
        public static string StationNameStationDataString = "Station Number One";
        public static double StationLatStationDataString = 40.7;
        public static double StationLonStationDataString = 73.0133;

        public static string StationDataString = string.Format(CultureInfo.InvariantCulture, "{0}, {1} , Station Description ,  {2}, {3},,,0,",
            StationIdStationDataString,
            StationNameStationDataString,
            StationLatStationDataString,
            StationLonStationDataString);

        public static string StationDataStringNotEnoughData = "2323, Station name,,";
        public static string StationDataStringTooMuchData = "2333, Station name,,,,  33.3, -843.00,,,0,";

        public static string StationDataStringInvalidId = "Aa, Station name , Station Description ,  11.11, 22.22,,,0,";

        public static string StationDataStringSingleQuotedStationName = string.Format(CultureInfo.InvariantCulture, "1234, '{0}' , Station Description ,  11.11, 22.22,,,0,",
            StationNameStationDataString);

        public static string StationDataStringDoubleQuatedStationName = string.Format(CultureInfo.InvariantCulture, "1234, \"{0}\" , Station Description ,  11.11, 22.22,,,0,",
            StationNameStationDataString);

        public static string StationDataStringInvalidNameEmpty = "1123,, Station Description ,  11.11, 22.22,,,0,";
        public static string StationDataStringInvalidNameWhiteSpace = "1123,   , Station Description ,  11.11, 22.22,,,0,";

        public static string StationDataStringInvalidLat = "1123, Station name  , Station Description ,  11 11, 22.22,,,0,";
        public static string StationDataStringInvalidLon = "1123, Station name  , Station Description ,  11.11, 22q22,,,0,";


        public static string StationsDataMultilines =
            @"400038,""MADISON AV/E 91 ST"",,  40.784359, -73.956139,,,0,
	    400039,""MADISON AV/E 93 ST"",,  40.785637, -73.955208,,,0,
	    400041,""MADISON AV/E 98 ST"",,  40.788414, -73.953178,,,0,
	    400042,""MADISON AV/E 101 ST"",,  40.790302, -73.951797,,,0,
	    400043,""MADISON AV/E 102 ST"",,  40.791370, -73.951019,,,0,
	    400044,""MADISON AV/E 104 ST"",,  40.792160, -73.950447,,,0,
	    400045,""MADISON AV/E 106 ST"",,  40.793434, -73.949509,,,0,
	    400046,""MADISON AV/E 107 ST"",,  40.794552, -73.948692,,,0,
	    400047,""MADISON AV/E 109 ST"",,  40.795815, -73.947777,,,0,
	    400048,""MADISON AV/E 111 ST"",,  40.797024, -73.946892,,,0,
	    400050,""MADISON AV/E 115 ST"",,  40.799541, -73.945053,,,0,
	    400051,""MADISON AV/E 118 ST"",,  40.801506, -73.943619,,,0,
	    400053,""MADISON AV/E 123 ST"",,  40.804649, -73.941376,,,0,
	    400054,""MADISON AV/E 125 ST"",,  40.806015, -73.940346,,,0,
	    400055,""MADISON AV/E 127 ST"",,  40.807247, -73.939461,,,0,
	    400056,""MADISON AV/E 129 ST"",,  40.808510, -73.938538,,,0,
	    ";

        public static string[] StationsDataStringArray = {
            "400050,\"MADISON AV/E 115 ST\",,  40.799541, -73.945053,,,0",
            "400051,\"MADISON AV/E 118 ST\",,  40.801506, -73.943619,,,0",
            "400053,\"MADISON AV/E 123 ST\",,  40.804649, -73.941376,,,0"
        };

        public static  IStation[] Stations =
        {
            new Station(1, StationName1, 0.1, 0.1),
            new Station(2, StationName2, 0.2, 0.2),
            new Station(3, StationName3, 0.3, 0.3)
        };

        public static IStation[] StationsWithDuplicates =
        {
            new Station(1, StationName1, 0.1, 0.1),
            new Station(2, StationName2, 0.2, 0.2),
            new Station(3, StationName3, 0.3, 0.3),
            new Station(1, StationName1, 0.1, 0.1),
            new Station(2, StationName2, 0.2, 0.2)
        };

        public static IStation Station4 = new Station(4, StationName4, 0.4, 0.4);
    }
}
