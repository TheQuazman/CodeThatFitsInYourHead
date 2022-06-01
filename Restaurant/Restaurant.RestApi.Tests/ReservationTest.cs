using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.RestApi.Tests
{
    public class ReservationTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void QuantityMustBePositive(int invalidQantity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Reservation(
                    new DateTime(2024, 8, 19, 11, 30, 0),
                    "mail@example.com",
                    "Marie Ilsøe",
                    invalidQantity));
        }
    }
}
