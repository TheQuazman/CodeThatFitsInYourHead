using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurant.RestApi
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        public ReservationsController(IReservationsRepository repository)
        {
            Repository = repository;
        }

        public IReservationsRepository Repository { get; }

        // POST <ReservationsController>
        [HttpPost]
        public async Task Post(ReservationDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            await Repository.Create(
                new Reservation(
                    new DateTime(2023, 11, 24, 19, 0, 0),
                    "juliad@example.net",
                    "Julia Domna",
                    5))
                .ConfigureAwait(false);
        }
    }
}
