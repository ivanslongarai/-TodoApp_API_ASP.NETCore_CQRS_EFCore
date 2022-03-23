using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Domain.Infra.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
    }

    public DbSet<TodoItem>? Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().ToTable("Todos");

        modelBuilder.Entity<TodoItem>().Property(x => x.Id);

        modelBuilder.Entity<TodoItem>().Property(x => x.User)
            .HasMaxLength(120)
            .HasColumnType("VARCHAR(120)");

        modelBuilder.Entity<TodoItem>().Property(x => x.Title)
            .HasMaxLength(160)
            .HasColumnType("VARCHAR(160)");

        modelBuilder.Entity<TodoItem>().Property(x => x.Done)
            .HasColumnType("BIT");

        modelBuilder.Entity<TodoItem>().Property(x => x.CreatedAt);
        modelBuilder.Entity<TodoItem>().Property(x => x.ExecutionDate);
        modelBuilder.Entity<TodoItem>().HasIndex(x => x.User);
    }
}