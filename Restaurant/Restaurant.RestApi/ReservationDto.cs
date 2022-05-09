namespace Restaurant.RestApi
{
    public class ReservationDto
    {
        public string? At { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }

        internal Reservation? Validate()
        {
            if(!DateTime.TryParse(At, out var d))
                return null;
            if (Email is null)
                return null;
            if(Quantity < 1)
                return null;

            return new Reservation(d, Email, Name ?? "", Quantity);
        }
    }
}
