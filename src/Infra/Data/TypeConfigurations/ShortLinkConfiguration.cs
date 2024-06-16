using Domain.Entities.ShortLinkAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.TypeConfigurations
{
    public class ShortLinkConfiguration : IEntityTypeConfiguration<ShortLink>
    {
        public void Configure(EntityTypeBuilder<ShortLink> builder)
        {
            builder.ToTable("ShortLinks");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired()
                .HasColumnType("numeric(18,0)")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(i => i.OriginUrl)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(i => i.UniqueCode)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(i => i.CreateDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(i => i.EditDate)
                .HasColumnType("datetime2");

            builder.Property(i => i.ExpireDate)
                .HasColumnType("datetime2");

            builder.HasOne(i => i.User)
                .WithMany(i => i.ShortLinks)
                .HasForeignKey(i => i.UserId)
                .HasConstraintName("FK_ShortLink_User");
        }
    }
}
