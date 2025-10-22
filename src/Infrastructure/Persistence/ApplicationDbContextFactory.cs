using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        
        optionsBuilder.UseNpgsql("Host=localhost;Database=restapi_db;Username=restapi_user;Password=restapi");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
