using Microsoft.EntityFrameworkCore;

namespace Magament.Host
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
