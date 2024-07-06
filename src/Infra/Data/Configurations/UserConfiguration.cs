using Domain.Entities.UserAggregate;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(i => i.State)
                .IsRequired();

            builder.Property(i => i.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.CreateDate)
                .IsRequired();

            builder.Property(i => i.EditDate);

            var emailConverter = new ValueConverter<Email, string>(
                v => v.ToString(),
                v => Email.FromString(v)
                );

            builder.Property(i => i.Email)
                   .HasConversion(emailConverter)
                   .HasMaxLength(100);

            var passwordConverter = new ValueConverter<Password, string>(
                v => v.ToString(),
                v => Password.FromString(v)
                );

            builder.Property(i => i.Password)
                   .HasConversion(passwordConverter)
                   .HasMaxLength(100);

            var postalCodeConverter = new ValueConverter<PostalCode, string>(
                v => v.ToString(),
                v => PostalCode.FromString(v)
                );

            builder.ComplexProperty(i => i.Address)
                   .Property(i => i.PostalCode)
                   .HasConversion(postalCodeConverter)
                   .HasMaxLength(10);
        }
    }
}
