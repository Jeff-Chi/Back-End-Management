namespace Management.Host
{
    public sealed class AuditlogOptions
    {
        public static string SectionName { get; set; } = "AuditLog";
        public bool IsEnabled { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
