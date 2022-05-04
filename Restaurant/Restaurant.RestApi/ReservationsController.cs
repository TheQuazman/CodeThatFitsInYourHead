using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
        public async Task<ActionResult> Post(ReservationDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
            if (!DateTime.TryParse(dto.At, out var d))
                return new BadRequestResult();
            if (dto.Email is null)
                return new BadRequestResult();
            if (dto.Quantity < 1)
                return new BadRequestResult();

            var reservations = await Repository.ReadReservations(d).ConfigureAwait(false);
            int reservedSeats = reservations.Sum(r => r.Quantity);
            if(10 < reservedSeats + dto.Quantity)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            var r = new Reservation(d, dto.Email, dto.Name ?? "", dto.Quantity);
            await Repository.Create(r).ConfigureAwait(false);

            return new NoContentResult();
        }
    }
}
