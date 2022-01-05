using Microsoft.EntityFrameworkCore;

namespace DomainSiakad.Contexts
{
    public class SiakadDbContext : DbContext
    {
        public SiakadDbContext(DbContextOptions<SiakadDbContext> options) : base(options) { }
    }
}
