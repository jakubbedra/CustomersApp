using CustomersApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomersApp;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    
    public CustomerDbContext() : base(){}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        YamlPropertiesProvider provider = ServiceProvider.YamlPropertiesProviderInstance();
        var connectionString = $"Server={provider.GetDbServer()};" +
                               $"Port={provider.GetServerPort()};" +
                               $"Database={provider.GetDbName()};" +
                               $"User Id={provider.GetDbUserId()};" +
                               $"Password={provider.GetDbPassword()};";
        optionsBuilder.UseNpgsql(connectionString);
    }
}