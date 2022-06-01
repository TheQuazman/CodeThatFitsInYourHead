namespace Restaurant.RestApi
{
    internal sealed class Seating
    {
        internal Seating(TimeSpan seatingDuration, Reservation reservation)
        {
            SeatingDuration = seatingDuration;
            Reservation = reservation;
        }

        internal TimeSpan SeatingDuration { get; }
        internal Reservation Reservation { get; }

        internal DateTime Start
        {
            get { return Reservation.At; }
        }

        internal DateTime End
        {
            get { return Start + SeatingDuration; }
        }

        internal bool Overlaps(Reservation other)
        {
            var otherSeating = new Seating(SeatingDuration, other);
            return Start < otherSeating.End && otherSeating.Start < End;
        }

        public override bool Equals(object? obj)
        {
            return obj is Seating seating &&
                   SeatingDuration.Equals(seating.SeatingDuration) &&
                   EqualityComparer<Reservation>.Default.Equals(Reservation, seating.Reservation);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SeatingDuration, Reservation);
        }
    }
}
