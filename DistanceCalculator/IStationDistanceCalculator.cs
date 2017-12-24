using Stations;

namespace DistanceCalculator
{
    public interface IStationDistanceCalculator
    {
        double GetDistance(IStation station1, IStation station2);
    }
}
