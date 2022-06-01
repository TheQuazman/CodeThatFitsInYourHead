using System.Data.SqlClient;

namespace Restaurant.RestApi
{
    public class SqlReservationsRepository : IReservationsRepository
    {
        public SqlReservationsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public async Task Create(Reservation reservation)
        {
            if (reservation is null)
                throw new ArgumentNullException(nameof(reservation));

            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(createReservationSql, conn);
            cmd.Parameters.Add(new SqlParameter("@At", reservation.At));
            cmd.Parameters.Add(new SqlParameter("@Name", reservation.Name));
            cmd.Parameters.Add(new SqlParameter("@Email", reservation.Email));
            cmd.Parameters.Add(
                new SqlParameter("@Quantity", reservation.Quantity));

            await conn.OpenAsync().ConfigureAwait(false);
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        private const string createReservationSql = @"
            INSERT INTO
                [dbo].[Reservations] ([At], [Name], [Email], [Quantity])
            VALUES (@At, @Name, @Email, @Quantity)";

        public async Task<IReadOnlyCollection<Reservation>> ReadReservations(
            DateTime dateTime)
        {
            var result = new List<Reservation>();

            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(readByRangeSql, conn);
            cmd.Parameters.AddWithValue("@at", dateTime.Date);

            await conn.OpenAsync().ConfigureAwait(false);
            using var rdr =
                await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            while (rdr.Read())
                result.Add(
                    new Reservation(
                        (DateTime)rdr["At"],
                        (string)rdr["Name"],
                        (string)rdr["Email"],
                        (int)rdr["Quantity"]));

            return result.AsReadOnly();
        }

        private const string readByRangeSql = @"
            SELECT [At], [Name], [Email], [Quantity]
            FROM [dbo].[Reservations]
            WHERE CONVERT(DATE, [At]) = @At";
    }
}
