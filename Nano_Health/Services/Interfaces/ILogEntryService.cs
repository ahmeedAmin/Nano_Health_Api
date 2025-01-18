using Nano_Health.Dtos;
using Nano_Health.Models;

namespace Nano_Health.Services.Interfaces
{
    public interface ILogEntryService
    {
        Task<Response> AddLogEntry(AddLogEntryDto dto);
        Response GetAll();

    }
}
