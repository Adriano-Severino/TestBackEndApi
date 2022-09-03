using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Domain;

namespace TestBackEndApi.Data.Maps
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FantasyName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
            builder.Property(x => x.Uf).IsRequired().HasMaxLength(2).HasColumnType("varchar(2)");
            builder.Property(x => x.Cnpj).IsRequired().HasMaxLength(18).HasColumnType("varchar(18)");
        }
    }
}
