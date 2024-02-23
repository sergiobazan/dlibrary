using Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasIndex(c => c.Id);

        builder.Property(c => c.Name)
            .HasConversion(
            name => name!.Value,
            value => new Name(value));

        builder.Property(c => c.Description)
            .HasConversion(
            description => description!.Value,
            value => new Description(value));

        builder.HasMany(c => c.Books)
            .WithOne()
            .HasForeignKey(b => b.CategoryId);

    }
}