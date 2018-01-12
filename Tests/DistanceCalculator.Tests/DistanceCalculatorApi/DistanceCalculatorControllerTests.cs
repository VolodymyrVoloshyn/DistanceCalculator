using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DistanceCalculator.Tests.Stubs;
using DistanceCalculatorApi;
using Moq;
using NUnit.Framework;
using StationProvider;
using Stations;

namespace DistanceCalculator.Tests.DistanceCalculatorApi
{
    [TestFixture]
    public class DistanceCalculatorControllerTests
    {
        [Test]
        public void GetDistance_Success()
        {
            var station1 = StationData.Station1;
            var station2 = StationData.Station2;

            var stationProviderMock= new Mock<IStationProvider>(MockBehavior.Strict);
            stationProviderMock.SetupSequence(p => p.GetStation(It.IsAny<string>()))
                .Returns(station1)
                .Returns(station2);

            double distance = 10.9;

            var distCalcMock = new Mock<IStationDistanceCalculator>(MockBehavior.Strict);
            distCalcMock.Setup(calc =>
                    calc.GetDistance(It.IsAny<IStation>(), It.IsAny<IStation>()))
                .Returns(distance);

            var distCalcController = new DistanceCalculatorController(distCalcMock.Object, stationProviderMock.Object);

            distCalcController.Request= new HttpRequestMessage();
            distCalcController.Configuration= new HttpConfiguration();

            // Act

            var response = distCalcController.GetDistance(station1.Name, station2.Name);

            stationProviderMock.Verify(p=> p.GetStation(It.IsAny<string>()), Times.Exactly(2));

            distCalcMock.Verify(calc =>
                calc.GetDistance(It.Is<IStation>(st=> st.Equals(station1)), It.Is<IStation>(st => st.Equals(station2))), Times.Once);

            double resultDist;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.TryGetContentValue(out resultDist));

            Assert.AreEqual(distance, resultDist, "Result distance is incorrect");
        }

        [Test]
        public void GetDistance_GetStation1_Failed()
        {
            var station1 = StationData.Station1;

            var stationProviderMock = new Mock<IStationProvider>(MockBehavior.Strict);
            stationProviderMock.Setup(p => p.GetStation(It.Is<string>(s=> s == null)))
                .Throws<Exception>();

            stationProviderMock.Setup(p => p.GetStation(It.IsNotNull<string>()))
                .Returns(station1);

            double distance = 10.9;

            var distCalcMock = new Mock<IStationDistanceCalculator>(MockBehavior.Strict);
            distCalcMock.Setup(calc =>
                    calc.GetDistance(It.IsAny<IStation>(), It.IsAny<IStation>()))
                .Returns(distance);

            var distCalcController = new DistanceCalculatorController(distCalcMock.Object, stationProviderMock.Object);

            distCalcController.Request = new HttpRequestMessage();
            distCalcController.Configuration = new HttpConfiguration();

            // Act

            var response = distCalcController.GetDistance(null, station1.Name);

            stationProviderMock.Verify(p => p.GetStation(It.IsAny<string>()), Times.Once);

            distCalcMock.Verify(calc =>
                calc.GetDistance(It.IsAny<IStation>(), It.IsAny<IStation>()), Times.Never);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void GetDistance_GetStation2_Failed()
        {
            var station1 = StationData.Station1;

            var stationProviderMock = new Mock<IStationProvider>(MockBehavior.Strict);
            stationProviderMock.Setup(p => p.GetStation(It.Is<string>(s => s == null)))
                .Throws<Exception>();

            stationProviderMock.Setup(p => p.GetStation(It.IsNotNull<string>()))
                .Returns(station1);

            double distance = 10.9;

            var distCalcMock = new Mock<IStationDistanceCalculator>(MockBehavior.Strict);
            distCalcMock.Setup(calc =>
                    calc.GetDistance(It.IsAny<IStation>(), It.IsAny<IStation>()))
                .Returns(distance);

            var distCalcController = new DistanceCalculatorController(distCalcMock.Object, stationProviderMock.Object);

            distCalcController.Request = new HttpRequestMessage();
            distCalcController.Configuration = new HttpConfiguration();

            // Act

            var response = distCalcController.GetDistance(station1.Name, null);

            stationProviderMock.Verify(p => p.GetStation(It.IsAny<string>()), Times.Exactly(2));

            distCalcMock.Verify(calc =>
                calc.GetDistance(It.IsAny<IStation>(), It.IsAny<IStation>()), Times.Never);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void GetDistance_Failed()
        {
            var station1 = StationData.Station1;
            var station2 = StationData.Station2;

            var stationProviderMock = new Mock<IStationProvider>(MockBehavior.Strict);
            stationProviderMock.SetupSequence(p => p.GetStation(It.IsAny<string>()))
                .Returns(station1)
                .Returns(station2);

            double distance = 10.9;

            var distCalcMock = new Mock<IStationDistanceCalculator>(MockBehavior.Strict);
            distCalcMock.Setup(calc =>
                    calc.GetDistance(It.IsAny<IStation>(), It.IsAny<IStation>()))
                .Throws<Exception>();

            var distCalcController = new DistanceCalculatorController(distCalcMock.Object, stationProviderMock.Object);

            distCalcController.Request = new HttpRequestMessage();
            distCalcController.Configuration = new HttpConfiguration();

            // Act

            var response = distCalcController.GetDistance(station1.Name, station2.Name);

            stationProviderMock.Verify(p => p.GetStation(It.IsAny<string>()), Times.Exactly(2));

            distCalcMock.Verify(calc =>
                calc.GetDistance(It.IsAny<IStation>(), It.IsAny<IStation>()), Times.Once);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public void Constructor_PassStationDistanceCalculator_Failed()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new DistanceCalculatorController(null, Mock.Of<IStationProvider>()));
        }

        [Test]
        public void Constructor_PassStationProvider_Failed()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new DistanceCalculatorController(Mock.Of<IStationDistanceCalculator>(), null));
        }
    }
}
