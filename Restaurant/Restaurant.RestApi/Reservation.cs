namespace Restaurant.RestApi
{
    public sealed class Reservation
    {
        public Reservation(
            DateTime at,
            string email,
            string name,
            int quantity)
        {
            if (quantity < 1)
                throw new ArgumentOutOfRangeException(
                    nameof(quantity),
                    "The value must be a positive (non-zero) number.");

            At = at;
            Email = email;
            Name = name;
            Quantity = quantity;
        }

        public DateTime At { get; }
        public string Email { get; }
        public string Name { get; }
        public int Quantity { get; }

        public Reservation WithDate(DateTime newAt)
        {
            return new Reservation(newAt, Email, Name, Quantity);
        }

        public Reservation WithEmail(string newEmail)
        {
            return new Reservation(At, newEmail, Name, Quantity);
        }

        public Reservation WithName(string newName)
        {
            return new Reservation(At, Email, newName, Quantity);
        }

        public Reservation WithQuantity(int newQuantity)
        {
            return new Reservation(At, Email, Name, newQuantity);
        }

        public override bool Equals(object? obj)
        {
            return obj is Reservation reservation &&
                   At == reservation.At &&
                   Email == reservation.Email &&
                   Name == reservation.Name &&
                   Quantity == reservation.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(At, Email, Name, Quantity);
        }
    }
}
