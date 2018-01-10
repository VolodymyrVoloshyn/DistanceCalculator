using Stations;

namespace StationProvider.StationParcer
{
    public interface IStationParcer<in T>
    {
        IStation Parce(T input);
    }
}