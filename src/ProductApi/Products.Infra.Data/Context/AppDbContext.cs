using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Products.Application.Configuration.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.SeedWork;
using Productss.Domain.Entities;


namespace Products.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            var currentAssembly = typeof(AppDbContext).Assembly;
            var efMappingTypes = currentAssembly.GetTypes().Where(t =>
                t.FullName.StartsWith("Products.Infra.Data.Mapping.") &&
                t.FullName.EndsWith("Map"));

            foreach (var map in efMappingTypes.Select(Activator.CreateInstance))
            {
                modelBuilder.ApplyConfiguration((dynamic)map);
            }

            //modelBuilder.Seed();
        }
    }
}
