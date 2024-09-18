using Microsoft.EntityFrameworkCore;
using TodoListBackend.Models;

namespace TodoListBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.TodoList)
                .HasForeignKey(t => t.UserId);
        }
    }
}