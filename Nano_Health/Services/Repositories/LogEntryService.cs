using AutoMapper;
using Nano_Health.Dtos;
using Nano_Health.Loc;
using Nano_Health.Models;
using Nano_Health.Services.Interfaces;
using System.Net;

namespace Nano_Health.Services.Repositories
{
    public class LogEntryService: ILogEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LogEntryService( IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response> AddLogEntry(AddLogEntryDto dto)
        {
            var model = _mapper.Map<LogEntry>(dto);
            await _unitOfWork.LogEntryRepository.AddAsync(model);
            var affectedRows = _unitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                return new Response
                {
                    Data = model,
                    Message = "Created Successfully"
                };
            }
            else
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Errore found"
                };
            }
        }
        public Response GetAll()
        {
            var models = _unitOfWork.LogEntryRepository.Table.Where(x=>x.IsDeleted==false).ToList();

            var modelDto = models.Select(x => new LogEntry
            {
                
                Service = x.Service,
                Level = x.Level,
                Message = x.Message,
                BackendStorageType = x.BackendStorageType,
                Timestamp = x.Timestamp,
                Id = x.Id
            }).ToList();

            long totalCount = models.Count();
            return new Response
            {
                Data = modelDto,
                Total=totalCount
            };
        }
    }
}
