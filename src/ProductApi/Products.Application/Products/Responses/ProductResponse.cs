using Productss.Domain.Entities;

namespace Products.Application.Products.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public Category Category { get; set; }
        public char? Genre { get; set; }
    }
}
