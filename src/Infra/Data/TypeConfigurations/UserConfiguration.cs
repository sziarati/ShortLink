using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.TypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired()
                .HasColumnType("numeric(18,0)")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.State)
                .IsRequired()
                .HasColumnType("tinyint");

            builder.Property(i => i.UserName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(i => i.Password)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(i => i.Email)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(i => i.CreateDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(i => i.EditDate)
                .HasColumnType("datetime2");

            builder.OwnsOne(i => i.Address);
        }
    }
}
