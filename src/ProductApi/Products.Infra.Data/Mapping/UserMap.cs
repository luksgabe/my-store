using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Entities;

namespace Products.Infra.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(p => p.Email);

            builder.Property(p => p.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar(max)")
                .IsRequired();

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
