using System.Diagnostics.CodeAnalysis;

namespace Restaurant.RestApi.Settings
{
    public class RestaurantSettings
    {
        public TimeSpan OpensAt { get; set; }
        public TimeSpan LastSeating { get; set; }
        public TimeSpan SeatingDuration { get; set; }

        [SuppressMessage(
            "Performance",
            "CA1819:Properties should not return arrays",
            Justification = "With the .NET configuration system, it seems like it's either this, or some collection object with a public setter, which causes other code analysis warnings.")]
        public TableSettings[]? Tables { get; set; }

        internal MaitreD ToMaitreD()
        {
            return new MaitreD(
                OpensAt,
                LastSeating,
                SeatingDuration,
                Tables!.Select(ts => ts.ToTable()));
        }
    }
}
