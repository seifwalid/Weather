using Microsoft.EntityFrameworkCore;
using Weather.Model;

namespace Weather.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Userr>().HasKey(u => u.UserId);
  
    }
    public DbSet<Userr> Users { get; set; }
    public DbSet<PermissibleLimits>PermissibleLimits { get; set; }
    public DbSet<Alert>Alerts { get; set; }

    
}