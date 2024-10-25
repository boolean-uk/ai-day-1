using Microsoft.EntityFrameworkCore;

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }

    public DbSet<Car> Cars { get; set; }
}
