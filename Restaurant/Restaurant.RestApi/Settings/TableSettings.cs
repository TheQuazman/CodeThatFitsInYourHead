namespace Restaurant.RestApi.Settings
{
    public class TableSettings
    {
        public TableType TableType { get; set; }
        public int Seats { get; set; }

        internal Table ToTable()
        {
            switch (TableType)
            {
                case TableType.Communal:
                    return Table.Communal(Seats);
                default:
                    return Table.Standard(Seats);
            }
        }
    }
}
