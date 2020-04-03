using Microsoft.EntityFrameworkCore;

namespace Kanban_CRUD.Models
{
    public class KanbanDataContext : DbContext
    {
        public KanbanDataContext(DbContextOptions<KanbanDataContext> options)
          : base(options)
        { }

        public DbSet<KanbanCard> KanbanCards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KanbanCard>().ToTable("KanbanCard");
        }
    }
}
