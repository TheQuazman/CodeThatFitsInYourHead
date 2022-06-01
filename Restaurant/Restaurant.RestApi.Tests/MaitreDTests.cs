using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.RestApi.Tests
{
    public class MaitreDTests
    {
        [SuppressMessage(
            "Performance",
            "CA1812: Avoid uninstantiated internal classes",
            Justification = "This class is instantiated via Reflection.")]
        private class AcceptTestCases :
            TheoryData<MaitreD, DateTime, IEnumerable<Reservation>>
        {
            public AcceptTestCases()
            {
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Communal(12) }),
                    Some.Now,
                    Array.Empty<Reservation>());
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Communal(8), Table.Communal(11) }),
                    Some.Now,
                    Array.Empty<Reservation>());
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Communal(2), Table.Communal(11) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(2) });
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Communal(11) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(11).TheDayBefore() });
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Communal(11) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(11).TheDayAfter() });
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(2.5),
                        new[] { Table.Standard(12) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(11).AddDate(
                        TimeSpan.FromHours(-2.5)) });
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(1),
                        new[] { Table.Standard(14) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(9).AddDate(
                        TimeSpan.FromHours(1)) });
            }
        }

        [SuppressMessage(
            "Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Parametrised test.")]
        [Theory, ClassData(typeof(AcceptTestCases))]
        public void Accept(
            MaitreD sut,
            DateTime now,
            IEnumerable<Reservation> reservations)
        {
            var r = Some.Reservation.WithQuantity(11);
            var actual = sut.WillAccept(now, reservations, r);
            Assert.True(actual);
        }

        [SuppressMessage(
            "Performance",
            "CA1812: Avoid uninstantiated internal classes",
            Justification = "This class is instantiated via Reflection.")]
        private class RejectTestCases :
            TheoryData<MaitreD, DateTime, IEnumerable<Reservation>>
        {
            public RejectTestCases()
            {
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Communal(6), Table.Communal(6) }),
                    Some.Now,
                    Array.Empty<Reservation>());
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Standard(12) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(1) });
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Standard(11) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(1).OneHourBefore() });
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Standard(12) }),
                    Some.Now,
                    new[] { Some.Reservation.WithQuantity(2).OneHourLater() });
                /* Some.Reservation.At is the time of the 'hard-coded'
                 * reservation in the test below. Adding 30 minutes to it means
                 * that the restaurant opens 30 minutes later than the desired
                 * reservation time, and therefore must be rejected. */
                Add(new MaitreD(
                        Some.Reservation.At.AddMinutes(30).TimeOfDay,
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        new[] { Table.Standard(12) }),
                    Some.Now,
                    Array.Empty<Reservation>());
                /* Some.Reservation.At is the time of the 'hard-coded'
                 * reservation in the test below. Subtracting 30 minutes from
                 * it means that the restaurant's last seating is 30 minutes
                 * before the reservation time, and therefore the reservation
                 * must be rejected. */
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        Some.Reservation.At.AddMinutes(-30).TimeOfDay,
                        TimeSpan.FromHours(6),
                        new[] { Table.Standard(12) }),
                    Some.Now,
                    Array.Empty<Reservation>());
                Add(new MaitreD(
                        TimeSpan.FromHours(18),
                        TimeSpan.FromHours(21),
                        TimeSpan.FromHours(6),
                        Table.Standard(12)),
                    Some.Now.AddDays(30),
                    Array.Empty<Reservation>());
            }
        }

        [SuppressMessage(
            "Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Parametrised test.")]
        [Theory, ClassData(typeof(RejectTestCases))]
        public void Reject(
            MaitreD sut,
            DateTime now,
            IEnumerable<Reservation> reservations)
        {
            var r = Some.Reservation.WithQuantity(11);
            var actual = sut.WillAccept(now, reservations, r);
            Assert.False(actual);
        }
    }
}
