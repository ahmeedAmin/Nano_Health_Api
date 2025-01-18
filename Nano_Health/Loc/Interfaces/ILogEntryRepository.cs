using Nano_Health.Models;

namespace Nano_Health.Loc.Interfaces
{
    public interface ILogEntryRepository : IRepository<LogEntry>
    {
        void SoftDelete(int id);
    }
}
