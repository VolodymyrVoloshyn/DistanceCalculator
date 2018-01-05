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
	public class TxtTextReaderStationDataSourceTests
    {
        [Test]
        public void GetStations_Success()
        {
            string stationData = "dummyStr";
            string stationName1 = StationData.StationName1;
            string stationName2 = StationData.StationName2;
            string stationName3 = StationData.StationName3;

            var textReaderMock = new Mock<TextReader>();

            textReaderMock.SetupSequence(tr => tr.Peek())
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(-1);

            textReaderMock.SetupSequence(tr => tr.ReadLine())
                .Returns(string.Empty)
                .Returns(stationData)
                .Returns(stationData)
                .Returns(stationData);

            
            var stationParserMock = new Mock<IStationParcer<string>>();

            stationParserMock.SetupSequence(sp => sp.Parce(It.IsAny<string>()))
                .Returns(new Station(1, stationName1, 0, 0))
                .Returns(new Station(2, stationName2, 0, 0))
                .Returns(new Station(3, stationName3, 0, 0));
            
            var ds = new TxtTextReaderStationDataSource(textReaderMock.Object, stationParserMock.Object);

            // Act
            var result= ds.GetStations();

            Assert.IsNotNull(result);

            textReaderMock.Verify(tr => tr.Peek(), Times.Exactly(5));
            textReaderMock.Verify(tr => tr.ReadLine(), Times.Exactly(4));
            stationParserMock.Verify(tr => tr.Parce(It.IsAny<string>()), Times.Exactly(3));

            var resArr = result.ToArray();

            Assert.AreEqual(3, resArr.Length, "Count of returned stations is not valid.");

            Assert.AreEqual(stationName1, resArr[0].Name, "Station has invalid name");
            Assert.AreEqual(stationName2, resArr[1].Name, "Station has invalid name");
            Assert.AreEqual(stationName3, resArr[2].Name, "Station has invalid name");
        }

        [Test]
        public void GetStations_ManyReadsTheSameStationData_Success()
        {
            string stationData = "dummyStr";
            string stationName1 = StationData.StationName1;

            var textReaderMock = new Mock<TextReader>();

            textReaderMock.SetupSequence(tr => tr.Peek())
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(-1);

            textReaderMock.Setup(tr => tr.ReadLine())
                .Returns(stationData);
            
            var stationParserMock = new Mock<IStationParcer<string>>();

            // returns the same station name 
            stationParserMock.SetupSequence(sp => sp.Parce(It.IsAny<string>()))
                .Returns(new Station(1, stationName1, 0, 0))
                .Returns(new Station(2, stationName1, 0, 0))
                .Returns(new Station(3, stationName1, 0, 0));
            
            var ds = new TxtTextReaderStationDataSource(textReaderMock.Object, stationParserMock.Object);

            // Act
            var result= ds.GetStations();

            Assert.IsNotNull(result);

            textReaderMock.Verify(tr => tr.Peek(), Times.Exactly(5));
            textReaderMock.Verify(tr => tr.ReadLine(), Times.Exactly(4));
            stationParserMock.Verify(tr => tr.Parce(It.IsAny<string>()), Times.Exactly(3));

            var resArr = result.ToArray();

            Assert.AreEqual(3, resArr.Length, "Count of returned stations is not valid.");

            foreach (var station in resArr)
            {
                Assert.AreEqual(stationName1, station.Name, "Station has invalid name");
            }
        }

        [Test]
        public void GetStations_ReadFromEmptyTextReader_Failed()
        {
            var textReaderMock = new Mock<TextReader>();

            textReaderMock.Setup(tr => tr.Peek())
                .Returns(-1);

            var stationParser = Mock.Of<IStationParcer<string>>();

            var ds = new TxtTextReaderStationDataSource(textReaderMock.Object, stationParser);

            // Act
            Assert.Throws<Exception>(() => ds.GetStations());
        }

        [Test]
        public void GetStations_ParserReturnsNull_Failed()
        {
            string stationData = "dummyStr";
            
            var textReaderMock = new Mock<TextReader>();

            textReaderMock.Setup(tr => tr.Peek())
                .Returns(0);

            textReaderMock.Setup(tr => tr.ReadLine())
                .Returns(stationData);

            var stationParserMock = new Mock<IStationParcer<string>>();

            stationParserMock.Setup(sp => sp.Parce(It.IsAny<string>()))
                .Returns((IStation)null);

            var ds = new TxtTextReaderStationDataSource(textReaderMock.Object, stationParserMock.Object);

            // Act
            Assert.Throws<Exception>(() => ds.GetStations());
        }
    }
}
