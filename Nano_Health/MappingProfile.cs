using AutoMapper;
using Nano_Health.Dtos;
using Nano_Health.Models;

namespace Nano_Health
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            LogProfile();
        }
        private void LogProfile()
        {
            CreateMap<AddLogEntryDto, LogEntry>();
        }
    }
}
