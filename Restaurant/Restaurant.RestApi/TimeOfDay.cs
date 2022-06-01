namespace Restaurant.RestApi
{
    public struct TimeOfDay : IEquatable<TimeOfDay>
    {
        private readonly TimeSpan durationSinceMidnight;

        public TimeOfDay(TimeSpan durationSinceMidnight)
        {
            if (durationSinceMidnight < TimeSpan.Zero ||
                TimeSpan.FromHours(24) < durationSinceMidnight)
                throw new ArgumentOutOfRangeException(
                    nameof(durationSinceMidnight),
                    "Please supply a TimeSpan between 0 and 24 hours.");

            this.durationSinceMidnight = durationSinceMidnight;
        }

        public static implicit operator TimeOfDay(TimeSpan timeSpan)
        {
            return new TimeOfDay(timeSpan);
        }

        public static TimeOfDay ToTimeOfDay(TimeSpan timeSpan)
        {
            return new TimeOfDay(timeSpan);
        }

        public override bool Equals(object? obj)
        {
            return obj is TimeOfDay day && Equals(day);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(durationSinceMidnight);
        }

        public static bool operator ==(TimeOfDay left, TimeOfDay right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimeOfDay left, TimeOfDay right)
        {
            return !(left == right);
        }

        public static bool operator <(TimeOfDay left, TimeOfDay right)
        {
            return left.durationSinceMidnight < right.durationSinceMidnight;
        }

        public static bool operator >(TimeOfDay left, TimeOfDay right)
        {
            return left.durationSinceMidnight > right.durationSinceMidnight;
        }

        public bool Equals(TimeOfDay other)
        {
            return durationSinceMidnight.Equals(other.durationSinceMidnight);
        }

        public int CompareTo(TimeOfDay other)
        {
            return
                durationSinceMidnight.CompareTo(other.durationSinceMidnight);
        }
    }
}
