using Microsoft.EntityFrameworkCore;

namespace Record.Models
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions<RecordContext> options) : base(options)
        {
               
        }
        public DbSet<Record> Records { get; set; }
    }
}
