using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nano_Health.Dtos;
using Nano_Health.Services.Interfaces;

namespace Nano_Health.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogEntryController : ControllerBase
    {
        private readonly ILogEntryService _logEntryService;

        public LogEntryController(ILogEntryService logEntryService)
        {
            _logEntryService = logEntryService;
        }

        //[Authorize]
        [HttpPost("add")]
        public IActionResult Add([FromForm] AddLogEntryDto dto)
        {
            var result = _logEntryService.AddLogEntry(dto).Result;
            return Ok(result);
        }
        //[Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = _logEntryService.GetAll();
            return Ok(result);
        }
    }
}
