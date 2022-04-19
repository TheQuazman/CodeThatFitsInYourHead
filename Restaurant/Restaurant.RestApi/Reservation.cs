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
            At = at;
            Email = email;
            Name = name;
            Quantity = quantity;
        }

        public DateTime At { get; }
        public string Email { get; }
        public string Name { get; }
        public int Quantity { get; }

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
