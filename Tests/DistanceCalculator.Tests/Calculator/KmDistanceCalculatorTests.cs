using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DistanceCalculator.Tests.Calculator
{
    [TestFixture]
    public class KmDistanceCalculatorTests
    {
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
