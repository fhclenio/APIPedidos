using APIPedidos.Model.Product;
using Microsoft.EntityFrameworkCore;

namespace APIPedidos.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(255);
        modelBuilder.Entity<Product>()
            .Property(p => p.Name).IsRequired();

        modelBuilder.Entity<Category>()
            .Property(c => c.Name).IsRequired();
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(100);
    }
}
