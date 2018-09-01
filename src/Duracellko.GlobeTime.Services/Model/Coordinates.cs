using System;

namespace Duracellko.GlobeTime.Domain.Model
{
    public struct Coordinates : IEquatable<Coordinates>
    {
        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }

        public double Longitude { get; }

        public static bool operator ==(Coordinates coordinates1, Coordinates coordinates2)
        {
            return coordinates1.Equals(coordinates2);
        }

        public static bool operator !=(Coordinates coordinates1, Coordinates coordinates2)
        {
            return !coordinates1.Equals(coordinates2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Coordinates))
            {
                return false;
            }

            return Equals((Coordinates)obj);
        }

        public bool Equals(Coordinates other)
        {
            return other.Latitude == Latitude && other.Longitude == Longitude;
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", Latitude, Longitude);
        }
    }
}
