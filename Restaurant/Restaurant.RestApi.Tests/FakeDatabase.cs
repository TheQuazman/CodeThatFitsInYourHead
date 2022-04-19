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
    }
}
