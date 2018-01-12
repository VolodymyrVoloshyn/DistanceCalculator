using System;
using Stations;

namespace DistanceCalculator
{
    public class KmStationDistanceCalculator : IStationDistanceCalculator
    {
        private readonly IDistanceCalculator _distanceCalculator;

        public KmStationDistanceCalculator(IDistanceCalculator distanceCalculator)
        {
            if (distanceCalculator == null)
            {
                throw new ArgumentNullException(nameof(distanceCalculator));
            }

            _distanceCalculator = distanceCalculator;
        }

        public double GetDistance(IStation station1, IStation station2)
        {
            if (station1 == null)
            {
                throw new ArgumentNullException(nameof(station1));
            }

            if (station2 == null)
            {
                throw new ArgumentNullException(nameof(station2));
            }

            return _distanceCalculator.GetDistance(station1.Lat, station1.Lon, station2.Lat, station2.Lon);
        }
    }
}