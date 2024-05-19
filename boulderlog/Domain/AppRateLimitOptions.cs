namespace Boulderlog.Domain
{
    public class AppRateLimitOptions
    {
        public const string AppRateLimit = "AppRateLimit";
        public string Policy { get; set; } = "fixed";
        public int PermitLimit { get; set; } = 100;
        public int Window { get; set; } = 10;
        public int ReplenishmentPeriod { get; set; } = 2;
        public int QueueLimit { get; set; } = 2;
        public int SegmentsPerWindow { get; set; } = 4;
        public int TokenLimit { get; set; } = 10;
        public int TokensPerPeriod { get; set; } = 4;
        public bool AutoReplenishment { get; set; } = false;
    }
}
