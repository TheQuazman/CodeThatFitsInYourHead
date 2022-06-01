namespace Restaurant.RestApi
{
    public sealed class Table
    {
        private Table(bool isStandard, int seats)
        {
            Seats = seats;
            IsStandard = isStandard;
            IsCommunal = !isStandard;
        }

        public static Table Standard(int seats)
        {
            return new Table(true, seats);
        }

        public static Table Communal(int seats)
        {
            return new Table(false, seats);
        }

        public int Seats { get; }
        public bool IsStandard { get; }
        public bool IsCommunal { get; }

        public Table WithSeats(int newSeats)
        {
            return new Table(IsStandard, newSeats);
        }

        internal bool Fits(int quantity)
        {
            return quantity <= Seats;
        }

        internal Table Reserve(int seatsToReserve)
        {
            return WithSeats(Seats - seatsToReserve);
        }

        public override bool Equals(object? obj)
        {
            return obj is Table table &&
                   Seats == table.Seats &&
                   IsStandard == table.IsStandard &&
                   IsCommunal == table.IsCommunal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Seats, IsStandard, IsCommunal);
        }
    }
}
