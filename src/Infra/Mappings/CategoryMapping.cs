using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("tb_category");
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("id");

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("name");

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("description");
        }
    }
}
