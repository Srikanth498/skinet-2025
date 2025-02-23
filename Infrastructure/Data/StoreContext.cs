using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

//Primary constructor for StoreContext class that inherits from DbContext class from EntityFrameworkCore library 
//and takes DbContextOptions as a parameter and passes it to the base class constructor. 
public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //below line of code applies the configurations from the ProductConfiguration class to the model builde
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
