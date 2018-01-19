using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DistanceCalculator.Tests.Calculator
{
    [TestFixture]
    public class KmStationDistanceCalculator
    {
        [Test]
        public void GetDistance_CheckResultValue_Success()
        {
            var calculator = new KmDistanceCalculator();
            var expectedResult = 3.719;
            // Act

            var result = calculator.GetDistance(40.781155, -73.958481, 40.810429, -73.937111);

            Assert.That(result, Is.EqualTo(expectedResult).Within(0.001));
        }

        [Test]
        public void GetDistance_ParamsAreZero()
        {
            var calculator = new KmDistanceCalculator();

            // Act

            var result = calculator.GetDistance(0, 0, 0, 0);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetDistance_ParamsAreEpsilon()
        {
            var calculator = new KmDistanceCalculator();

            // Act

            var result = calculator.GetDistance(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetDistance_ParamsAreNan()
        {
            var calculator = new KmDistanceCalculator();

            // Act

            Assert.Throws<Exception>(() => calculator.GetDistance(double.NaN, double.NaN, double.NaN, double.NaN));
        }

        [Test]
        public void GetDistance_ParamsAreMaxValue()
        {
            var calculator = new KmDistanceCalculator();

            // Act
            Assert.Throws<Exception>(() => calculator.GetDistance(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue));
        }

        [Test]
        public void GetDistance_ParamsAreMinValue()
        {
            var calculator = new KmDistanceCalculator();

            // Act
            Assert.Throws<Exception>(() => calculator.GetDistance(double.MinValue, double.MinValue, double.MinValue, double.MinValue));
        }

        [Test]
        public void GetDistance_ParamsAreNegativeInfinity()
        {
            var calculator = new KmDistanceCalculator();

            // Act
            Assert.Throws<Exception>(() => calculator.GetDistance(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity));
        }

        [Test]
        public void GetDistance_ParamsArePositiveInfinity()
        {
            var calculator = new KmDistanceCalculator();

            // Act
            Assert.Throws<Exception>(() => calculator.GetDistance(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity));
        }
    }
}
