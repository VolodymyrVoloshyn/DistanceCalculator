using System;
using System.IO;
using System.Linq;
using DistanceCalculator.Tests.StationDataSourceTests;
using DistanceCalculator.Tests.Stubs;
using Moq;
using NUnit.Framework;
using StationProvider;
using StationProvider.DataSource;
using StationProvider.StationParcer;

namespace DistanceCalculator.Tests.StationProviderTests
{
    [TestFixture]
    public class TxtStationProviderTests
    {
        [Test]
        public void GetStation_Success()
        {
            string stationName = StationData.StationName1;

            var stationDataSourceMock = new Mock<IStationDataSource>();

            stationDataSourceMock.Setup(ds => ds.GetStations())
                .Returns(StationData.Stations);

            var stationProvider = new TxtStationProvider(stationDataSourceMock.Object);

            // Act
            var result = stationProvider.GetStation(stationName);

            stationDataSourceMock.Verify(tr => tr.GetStations(), Times.Once());

            Assert.AreEqual(stationName, result.Name, "Station has invalid name");
        }

        [Test]
        public void GetStation_ByNull_Failed()
        {
            var stationDataSource = Mock.Of<IStationDataSource>();

            var stationProvider = new TxtStationProvider(stationDataSource);

            // Act

            Assert.Throws<ArgumentNullException>(() => stationProvider.GetStation(null));
        }

        [Test]
        public void GetStation_ByEmptyString_Success()
        {
            var stationDataSourceMock = new Mock<IStationDataSource>();

            stationDataSourceMock.Setup(ds => ds.GetStations())
                .Returns(StationData.Stations);

            var stationProvider = new TxtStationProvider(stationDataSourceMock.Object);

            // Act

            Assert.Throws<Exception>(() => stationProvider.GetStation(string.Empty));
        }

        [Test]
        public void GetStation_ByUnknownName_Success()
        {
            string stationName = StationData.StationName4;

            var stationDataSourceMock = new Mock<IStationDataSource>();

            stationDataSourceMock.SetupSequence(ds => ds.GetStations())
                .Returns(StationData.Stations)
                .Returns(StationData.Stations.Concat(new[] { StationData.Station4 }));

            var stationProvider = new TxtStationProvider(stationDataSourceMock.Object);

            // Act
            var result = stationProvider.GetStation(stationName);

            stationDataSourceMock.Verify(tr => tr.GetStations(), Times.Exactly(2));

            Assert.AreEqual(stationName, result.Name, "Station has invalid name");
        }

        [Test]
        public void GetStation_ByUnknownName_Failed()
        {
            string stationName = "dummy";

            var stationDataSourceMock = new Mock<IStationDataSource>();

            stationDataSourceMock.SetupSequence(ds => ds.GetStations())
                .Returns(StationData.Stations)
                .Returns(StationData.Stations.Concat(new[] { StationData.Station4 }));

            var stationProvider = new TxtStationProvider(stationDataSourceMock.Object);

            // Act
            Assert.Throws<Exception>(() => stationProvider.GetStation(stationName));
        }

        [Test]
        public void GetStation_ReadsDuplicatedDataFromDataSource_Success()
        {
            string stationName = StationData.StationName1;

            var stationDataSourceMock = new Mock<IStationDataSource>();

            stationDataSourceMock.Setup(ds => ds.GetStations())
                .Returns(StationData.StationsWithDuplicates);

            var stationProvider = new TxtStationProvider(stationDataSourceMock.Object);

            // Act
            var result = stationProvider.GetStation(stationName);

            stationDataSourceMock.Verify(tr => tr.GetStations(), Times.Once);

            Assert.AreEqual(stationName, result.Name, "Station has invalid name");
        }

        [Test]
        public void Constructor_PassDependencyNull_Failed()
        {
            Assert.Throws<ArgumentNullException>(() => new TxtStationProvider(null));
        }

        [Test]
        public void FindStation_ByNull_Failed()
        {
            var stationProvider = new TxtStationProvider(Mock.Of<IStationDataSource>());

            // Act
            Assert.Throws<ArgumentException>(() => stationProvider.FindStations(null));
        }

        [Test]
        public void FindStation_ByEmptyString_Failed()
        {
            var stationProvider = new TxtStationProvider(Mock.Of<IStationDataSource>());

            // Act
            Assert.Throws<ArgumentException>(() => stationProvider.FindStations(string.Empty));
        }

        [Test]
        public void FindStation_Success()
        {
            string stationNamePattern = StationData.StationNamePattern1;

            var stationDataSourceMock = new Mock<IStationDataSource>();

            stationDataSourceMock.Setup(ds => ds.GetStations())
                .Returns(StationData.Stations);

            var stationProvider = new TxtStationProvider(stationDataSourceMock.Object);

            // Act
            var result = stationProvider.FindStations(stationNamePattern);

            var resultArray = result.ToArray();

            stationDataSourceMock.Verify(tr => tr.GetStations(), Times.Once);

            Assert.AreEqual(3, resultArray.Length, "Count of found stations is not correct.");

            for (var i = 0; i < resultArray.Length; i++)
            {
                var station = resultArray[i];

                Assert.IsNotNull(station, "Station is null. Index in array is " + i);

                var stationName = station.Name;

                Assert.IsNotNull(stationName, "Station name is null. Index in array is " + i);

                Assert.IsTrue(stationName.StartsWith(stationNamePattern, StringComparison.InvariantCultureIgnoreCase),
                    string.Format("Station has invalid name. Station name is {0}. Expected start with {1}",
                        stationName, stationNamePattern));
            }
        }

        [Test]
        public void FindStation_InDuplicatedData_Success()
        {
            string stationNamePattern = StationData.StationNamePattern1;

            var stationDataSourceMock = new Mock<IStationDataSource>();

            stationDataSourceMock.Setup(ds => ds.GetStations())
                .Returns(StationData.StationsWithDuplicates);

            var stationProvider = new TxtStationProvider(stationDataSourceMock.Object);

            // Act
            var result = stationProvider.FindStations(stationNamePattern);

            var resultArray = result.ToArray();

            stationDataSourceMock.Verify(tr => tr.GetStations(), Times.Once);

            Assert.AreEqual(3, resultArray.Length, "Count of found stations is not correct.");

            for (var i = 0; i < resultArray.Length; i++)
            {
                var station = resultArray[i];

                Assert.IsNotNull(station, "Station is null. Index in array is " + i);

                var stationName = station.Name;

                Assert.IsNotNull(stationName, "Station name is null. Index in array is " + i);

                Assert.IsTrue(stationName.StartsWith(stationNamePattern, StringComparison.InvariantCultureIgnoreCase),
                    string.Format("Station has invalid name. Station name is {0}. Expected start with {1}",
                        stationName, stationNamePattern));
            }
        }
    }
}
