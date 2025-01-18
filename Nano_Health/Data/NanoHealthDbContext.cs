using Nano_Health.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Nano_Health.Data
{
    public class NanoHealthDbContext :IdentityDbContext<User>
    {
       
        public NanoHealthDbContext(DbContextOptions<NanoHealthDbContext> options) : base(options) { }
        public virtual DbSet<LogEntry> LogEntry { get; set; }
       

    }
}
