using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Productss.Domain.Entities;

namespace Products.Infra.Data.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();
                

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("UpdatedAt")
                .IsRequired(false);

            builder.Property(c => c.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("DeletedAt")
                .IsRequired(false);

            builder.HasMany(c => c.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(c => c.IdCategory);
        }
    }
}
