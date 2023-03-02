using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.IdentityService.Identity;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users")
            .HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(User.MaxNameLength);

        builder.Property(x => x.NormalizedName)
            .IsRequired()
            .HasMaxLength(User.MaxNameLength);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(User.MaxFirstNameLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(User.MaxLastNameLength);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(User.MaxEmailLength);

        builder.Property(x => x.NormalizedEmail)
            .IsRequired()
            .HasMaxLength(User.MaxEmailLength);

        builder.Property(x => x.PasswordHash)
            .IsRequired();
    }
}