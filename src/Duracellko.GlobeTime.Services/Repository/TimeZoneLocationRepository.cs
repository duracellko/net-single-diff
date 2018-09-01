using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Duracellko.GlobeTime.Domain.Model;
using Duracellko.GlobeTime.Domain.Repository;

namespace Duracellko.GlobeTime.Services.Repository
{
    public class TimeZoneLocationRepository : ITimeZoneLocationRepository
    {
        private const float PI = (float)Math.PI;

        private readonly object _entitiesLock = new object();
        private readonly IRepositoryFiles _repositoryFiles;
        private List<TimeZoneLocation> _timeZonesCache;

        public TimeZoneLocationRepository(IRepositoryFiles repositoryFiles)
        {
            _repositoryFiles = repositoryFiles ?? throw new ArgumentNullException(nameof(repositoryFiles));
        }

        public async Task<IEnumerable<TimeZoneLocation>> GetTimeZones()
        {
            return await Task.Run(() =>
            {
                List<TimeZoneLocation> timeZones = null;
                lock (_entitiesLock)
                {
                    timeZones = _timeZonesCache;
                }

                if (timeZones == null)
                {
                    timeZones = LoadTimeZones();
                    lock (_entitiesLock)
                    {
                        if (_timeZonesCache == null)
                        {
                            _timeZonesCache = timeZones;
                        }
                        else
                        {
                            timeZones = _timeZonesCache;
                        }
                    }
                }

                return timeZones.Select(CloneTimeZoneLocation).ToList();
            }).ConfigureAwait(false);
        }

        private static float Deg2Rad(float value)
        {
            return value * PI / 180;
        }

        private List<TimeZoneLocation> LoadTimeZones()
        {
            using (var sourceStream = _repositoryFiles.GetTimeZonesDataStream())
            {
                using (var reader = new BinaryReader(sourceStream))
                {
                    var timeZonesCount = reader.ReadInt32();
                    var result = new List<TimeZoneLocation>(timeZonesCount);

                    for (int timeZoneIndex = 0; timeZoneIndex < timeZonesCount; timeZoneIndex++)
                    {
                        var id = reader.ReadInt32();
                        var timeOffset = reader.ReadSingle();
                        var latitudesCount = reader.ReadInt32();

                        var timeZone = new TimeZoneLocation()
                        {
                            Id = -id,
                            TimeOffset = timeOffset
                        };
                        result.Add(timeZone);

                        var latitudes = new IEnumerable<Range<float>>[latitudesCount];
                        for (int latitudeIndex = 0; latitudeIndex < latitudesCount; latitudeIndex++)
                        {
                            var linesCount = reader.ReadByte();
                            if (linesCount > 0)
                            {
                                var lines = new List<Range<float>>(linesCount);
                                for (int lineIndex = 0; lineIndex < linesCount; lineIndex++)
                                {
                                    var start = reader.ReadSingle();
                                    var end = reader.ReadSingle();
                                    lines.Add(new Range<float>(Deg2Rad(start), Deg2Rad(end)));
                                }

                                latitudes[latitudeIndex] = lines.ToImmutableArray();
                            }
                        }

                        timeZone.Boundaries = latitudes.ToImmutableArray();
                    }

                    return result;
                }
            }
        }

        private TimeZoneLocation CloneTimeZoneLocation(TimeZoneLocation timeZone)
        {
            return new TimeZoneLocation
            {
                Id = timeZone.Id,
                TimeOffset = timeZone.TimeOffset,
                Boundaries = timeZone.Boundaries
            };
        }
    }
}
