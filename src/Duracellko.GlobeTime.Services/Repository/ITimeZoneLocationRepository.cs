using System.Collections.Generic;
using System.Threading.Tasks;
using Duracellko.GlobeTime.Domain.Model;

namespace Duracellko.GlobeTime.Domain.Repository
{
    public interface ITimeZoneLocationRepository
    {
        Task<IEnumerable<TimeZoneLocation>> GetTimeZones();
    }
}
