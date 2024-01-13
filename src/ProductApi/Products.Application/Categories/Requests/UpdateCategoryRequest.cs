namespace Products.Application.Categories.Requests
{
    public class UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UpdateCategoryRequest(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
