namespace Restaurant.RestApi
{
    public interface IReservationsRepository
    {
        Task Create(Reservation reservation);

        Task<IReadOnlyCollection<Reservation>> ReadReservations(DateTime dateTime);
    }
}
