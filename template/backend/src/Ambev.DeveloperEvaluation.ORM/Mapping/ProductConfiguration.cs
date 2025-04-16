using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products"); 

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .HasColumnName("UnitPrice")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt);

            builder.OwnsOne(p => p.Rating, rating =>
            {
                rating.Property(r => r.Rate)
                    .HasColumnName("Rate")
                    .HasColumnType("decimal(3,2)");

                rating.Property(r => r.Count)
                    .HasColumnName("Count");
            });
        }
    }
}
