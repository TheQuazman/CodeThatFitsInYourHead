using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurant.RestApi
{
    [ApiController, Route("[controller]")]
    public class ReservationsController
    {
        public ReservationsController(
            IReservationsRepository repository,
            MaitreD maitreD)
        {
            Repository = repository;
            MaitreD = maitreD;
        }

        public IReservationsRepository Repository { get; }
        public MaitreD MaitreD { get; }

        public async Task<ActionResult> Post(ReservationDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            Reservation? r = dto.Validate();
            if (r is null)
                return new BadRequestResult();

            var reservations = await Repository
                .ReadReservations(r.At)
                .ConfigureAwait(false);
            if (!MaitreD.WillAccept(DateTime.Now, reservations, r))
                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);

            await Repository.Create(r).ConfigureAwait(false);

            return new NoContentResult();
        }
    }
}
