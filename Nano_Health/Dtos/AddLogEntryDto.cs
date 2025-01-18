namespace Nano_Health.Dtos
{
    public class AddLogEntryDto
    {
        public string Service { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
