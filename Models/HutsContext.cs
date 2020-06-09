using Microsoft.EntityFrameworkCore;

namespace BalkanaAPI.Models
{
    public class HutsContext : DbContext
    {
        public HutsContext(DbContextOptions<HutsContext> options): base (options) 
        {

        }

        public DbSet<Hut> HutItems { get; set; }
    }
}
