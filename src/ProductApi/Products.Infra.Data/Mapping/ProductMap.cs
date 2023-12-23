using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Entities;

namespace Products.Infra.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasColumnType("text");

            builder.Property(p => p.Color)
                .HasColumnName("Color")
                .HasColumnType("varchar(10)")
                .HasMaxLength(10);


            builder.Property(p => p.Size)
                .HasColumnName("Size")
                .HasColumnType("varchar(5)")
                .HasMaxLength(5);

            builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.IdCategory)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Genre)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.IdGenre)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("UpdatedAt")
                .IsRequired(false);

            builder.Property(c => c.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("DeletedAt")
                .IsRequired(false);
        }
    }
}
