namespace Nano_Health.Models
{
    public class LogEntry: BaseEntity
    {
        public int Id { get; set; }
        public string Service { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty; 
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string BackendStorageType { get; set; } = string.Empty;
    }
}
