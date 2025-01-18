using Nano_Health.Data;
using Nano_Health.Loc.Interfaces;
using Nano_Health.Models;

namespace Nano_Health.Loc.Repositories
{
    public class LogEntryRepository : Repository<LogEntry>, ILogEntryRepository
    {
        private readonly NanoHealthDbContext _context;

        public LogEntryRepository(NanoHealthDbContext context) : base(context)
        {
            _context = context;
        }
        public void SoftDelete(int id)
        {
            var entity = _context.Set<LogEntry>().Find(id);
            if (entity != null)
            {
                context.Set<LogEntry>().Update(entity);
            }
        }
    }
}
