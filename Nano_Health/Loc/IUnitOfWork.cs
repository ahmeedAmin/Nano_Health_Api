using Nano_Health.Loc.Interfaces;

namespace Nano_Health.Loc
{
    public interface IUnitOfWork : IDisposable
    {
        ILogEntryRepository LogEntryRepository { get; }
        int SaveChanges();
    }
}
