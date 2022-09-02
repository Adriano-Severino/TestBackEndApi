using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Models;

namespace TestBackEndApi.Data.Maps
{
    public class ProviderMap : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Provider");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(34).HasColumnType("varchar(34)");
            builder.Property(x => x.CpfCnpj).IsRequired().HasMaxLength(18).HasColumnType("varchar(18)");
            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
            builder.Property(x => x.Telephone).IsRequired().HasMaxLength(1024).HasColumnType("varchar(64)");
            builder.Property(x => x.CustomData).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
            builder.HasOne(x => x.Company).WithMany(x => x.Providers);

        }
    }
}
