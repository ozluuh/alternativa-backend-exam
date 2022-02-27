using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("tb_product");
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);

            builder
                .Property(p => p.Id)
                .HasColumnName("id");

            builder
                .Property(p => p.Name)
                .HasColumnName("name");

            builder
                .Property(p => p.Description)
                .HasColumnName("description");

            builder
                .Property(p => p.Value)
                .HasColumnName("value");

            builder
                .Property(p => p.Brand)
                .HasColumnName("brand");

            builder
                .Property(p => p.CategoryId)
                .HasColumnName("category_id");
        }
    }
}
