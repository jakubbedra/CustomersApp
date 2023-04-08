using CustomersApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomersApp;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    
    public CustomerDbContext() : base(){}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=2137;Database=Customers-new;User Id=postgres;Password=admin;";
        optionsBuilder.UseNpgsql(connectionString);
    }
}