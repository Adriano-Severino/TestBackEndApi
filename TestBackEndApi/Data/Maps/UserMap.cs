using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestBackEndApi.Domain;

namespace TestBackEndApi.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(34).HasColumnType("varchar(34)");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(1024).HasColumnType("varchar(1024)");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(34).HasColumnType("varchar(34)");
        }
    }
}