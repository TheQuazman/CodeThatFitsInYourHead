using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.RestApi.Tests
{
    public static class Some
    {
        public readonly static DateTime Now =
            new DateTime(2022, 4, 1, 20, 15, 0);

        public readonly static Reservation Reservation =
            new Reservation(
                Now,
                "x@example.net",
                "",
                1);

        public readonly static MaitreD MaitreD =
            new MaitreD(
                TimeSpan.FromHours(16),
                TimeSpan.FromHours(21),
                TimeSpan.FromHours(12),
                Table.Communal(10));
    }
}
