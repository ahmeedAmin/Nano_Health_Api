using Nano_Health.Data;
using Nano_Health.Loc.Interfaces;
using Nano_Health.Loc.Repositories;

namespace Nano_Health.Loc
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly NanoHealthDbContext _context;
        public UnitOfWork(NanoHealthDbContext context)
        {
            _context = context;
            LogEntryRepository = new LogEntryRepository(context);
        }

        public ILogEntryRepository LogEntryRepository { get; private set; }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
