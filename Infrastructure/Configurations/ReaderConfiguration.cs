using Domain.Reader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
{
    public void Configure(EntityTypeBuilder<Reader> builder)
    {
        builder.ToTable("readers");

        builder.HasKey(r => r.Id);

        builder.OwnsOne(r => r.FullName);

        builder.Property(r => r.Email)
            .HasConversion(
            email => email!.Value,
            value => Email.Create(value));

        builder.HasIndex(r => r.Email).IsUnique();
    }
}
