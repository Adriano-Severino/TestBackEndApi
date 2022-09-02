using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection.Emit;
using TestBackEndApi.Data.Maps;
using TestBackEndApi.Models;

namespace TestBackEndApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Provider> Providers { get; set; }
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
        }

    }
}
