using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestBackEndApi.Data.Maps;
using TestBackEndApi.Domain;

namespace TestBackEndApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        
        public DbSet<Provider> Providers { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<ApplicationDbContext> operationalStoreOptions) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CompanyMap());
            builder.ApplyConfiguration(new ProviderMap());
            builder.ApplyConfiguration(new UserMap());
        }

    }
}
