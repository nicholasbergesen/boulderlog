namespace Boulderlog.Domain
{
    public class AppConfigOptions
    {
        public const string AppConfig = "AppConfig";
        public string? AdminUserEmail { get; set; }
        public string? DoNotReplyEmail { get; set; }
    }
}
