using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.RestApi.Tests
{
    public class TimeOfDayTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(25)]
        public void AttemptNegativeTimeOfDay(int hours)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new TimeOfDay(TimeSpan.FromHours(hours)));
        }
    }
}
