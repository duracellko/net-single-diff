using System;
using System.Collections.Generic;

namespace Duracellko.GlobeTime.Domain.Model
{
    public struct Range<T> : IEquatable<Range<T>>
    {
        public Range(T start, T end)
        {
            Start = start;
            End = end;
        }

        public T Start { get; }

        public T End { get; }

        public static bool operator ==(Range<T> left, Range<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Range<T> left, Range<T> right)
        {
            return !(left == right);
        }

        public bool Equals(Range<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Start, other.Start) && EqualityComparer<T>.Default.Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Range<T> other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (Start?.GetHashCode()).GetValueOrDefault(0) ^ (End?.GetHashCode()).GetValueOrDefault(0);
        }

        public override string ToString()
        {
            return $"<{Start}, {End}>";
        }
    }
}
