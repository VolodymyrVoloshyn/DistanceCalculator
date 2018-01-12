using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistanceCalculator.Tests.Stubs;
using Moq;
using NUnit.Framework;
using Stations;

namespace DistanceCalculator.Tests.Calculator
{
    [TestFixture]
    public class KmStationDistanceCalculatorTests
    {
        [Test]
        public void GetDistance_Success()
        {
            double distance = 10;

            var distCalcMock = new Mock<IDistanceCalculator>();
            distCalcMock.Setup(calc =>
                    calc.GetDistance(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .Returns(distance);

            // Act
            var stationDistCalc = new DistanceCalculator.KmStationDistanceCalculator(distCalcMock.Object);

            var result = stationDistCalc.GetDistance(Mock.Of<IStation>(), Mock.Of<IStation>());

            distCalcMock.Verify(calc =>
                calc.GetDistance(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);

            Assert.AreEqual(result, distance, "Result distance is incorrect");
        }

        [Test]
        public void GetDistance_Station1IsNull_Failed()
        {
            // Act
            var stationDistCalc = new DistanceCalculator.KmStationDistanceCalculator(Mock.Of<KmDistanceCalculator>());

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("station1"),
                () => stationDistCalc.GetDistance(null, Mock.Of<IStation>()));
        }

        [Test]
        public void GetDistance_Station2IsNull_Failed()
        {
            // Act
            var stationDistCalc = new DistanceCalculator.KmStationDistanceCalculator(Mock.Of<KmDistanceCalculator>());

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("station2"),
                () => stationDistCalc.GetDistance(Mock.Of<IStation>(), null));
        }

        [Test]
        public void GetDistance_KmDistanceCalculatorIsNull_Failed()
        {
            // Act
            Assert.Throws<ArgumentNullException>(()=> new DistanceCalculator.KmStationDistanceCalculator(null));
        }
    }
}
