using Domain.Entities.ShortLinkAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations
{
    public class ShortLinkConfiguration : IEntityTypeConfiguration<ShortLink>
    {
        public void Configure(EntityTypeBuilder<ShortLink> builder)
        {
            builder.ToTable("ShortLinks");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(i => i.OriginUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.UniqueCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(i => i.CreateDate)
                .IsRequired();

            builder.Property(i => i.EditDate);

            builder.Property(i => i.ExpireDate);

            builder.Property(i => i.UserId);

            builder.HasOne(i => i.User)
                .WithMany(i => i.ShortLinks)
                .HasForeignKey(i => i.UserId)
                .HasConstraintName("FK_ShortLink_User");
        }
    }
}
