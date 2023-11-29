namespace Products.Application.Categories.Requests
{
    public class RegisterCategoryRequest
    {
        public string Name { get; set; }

        public RegisterCategoryRequest(string name)
        {
            this.Name = name;
        }
    }
}
