using Domain.Reader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasIndex(r => r.Id);

        builder
            .HasMany(role => role.Readers)
            .WithMany(reader => reader.Roles);

        builder.HasData([
            new {
                Id = Guid.NewGuid(),
                Name = "Admin"
            },
            new {
                Id = Guid.NewGuid(),
                Name = "Reader"
            }
            ]);
    }
}
