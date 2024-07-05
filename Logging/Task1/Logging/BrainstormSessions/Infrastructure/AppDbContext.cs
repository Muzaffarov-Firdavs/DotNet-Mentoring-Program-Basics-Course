using BrainstormSessions.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace BrainstormSessions.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) :
            base(dbContextOptions)
        {
        }

        public DbSet<BrainstormSession> BrainstormSessions { get; set; }
    }
}
