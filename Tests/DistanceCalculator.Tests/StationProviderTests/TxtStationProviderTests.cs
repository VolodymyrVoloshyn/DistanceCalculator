using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Stations;
using DistanceCalculator.Tests.Stubs;
using Moq;
using StationProvider;

namespace DistanceCalculator.Tests
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
    }
}
