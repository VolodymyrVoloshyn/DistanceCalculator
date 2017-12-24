using Stations;

namespace StationProvider
{
    public interface IStationParcer<in T>
    {
        IStation Parce(T input);
    }
}