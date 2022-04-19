namespace Restaurant.RestApi
{
    public interface IReservationsRepository
    {
        Task Create(Reservation reservation);
    }
}
