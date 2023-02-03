using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.IdentityService.Identity;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Role.MaxNameLength);

        builder.Property(x => x.NormalizedName)
            .IsRequired()
            .HasMaxLength(Role.MaxNameLength);
    }
}