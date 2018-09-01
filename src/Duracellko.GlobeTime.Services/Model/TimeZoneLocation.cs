using System;
using System.Collections.Generic;

namespace Duracellko.GlobeTime.Domain.Model
{
    public class TimeZoneLocation : TimeZoneLocationBase
    {
        private float _timeOffset;

        public float TimeOffset
        {
            get
            {
                return _timeOffset;
            }

            set
            {
                if (value != _timeOffset)
                {
                    _timeOffset = value;
                    Location = new Coordinates(0, GetCenterLongitude(value));
                }
            }
        }

        public IReadOnlyList<IEnumerable<Range<float>>> Boundaries { get; set; }

        private static double GetCenterLongitude(double timeOffset)
        {
            timeOffset = Math.Round(timeOffset);
            if (timeOffset > 12)
            {
                timeOffset = 12;
            }
            else if (timeOffset < -12)
            {
                timeOffset = -12;
            }

            double result = timeOffset * Math.PI / 12.0;
            if (result > Math.PI)
            {
                result -= 2 * Math.PI;
            }
            else if (result <= -Math.PI)
            {
                result += 2 * Math.PI;
            }

            return result;
        }
    }
}
