using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.RestApi.Tests
{
    public class FakeDatabase : Collection<Reservation>, IReservationsRepository
    {
        public Task Create(Reservation reservation)
        {
            Add(reservation);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Reservation>> ReadReservations(DateTime dateTime)
        {
            var min = dateTime.Date;
            var max = min.AddDays(1).AddTicks(-1);

            return Task.FromResult<IReadOnlyCollection<Reservation>>(this.Where(r => min <= r.At && r.At <= max).ToList());
        }
    }
}
