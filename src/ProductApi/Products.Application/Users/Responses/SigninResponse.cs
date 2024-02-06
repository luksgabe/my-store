namespace Products.Application.Users.Responses
{
    public class SigninResponse
    {
        public SigninResponse(Guid id, string token)
        {
            this.Id = id;
            this.Token = token;
        }


        public Guid Id { get; set; }
        public string Token { get; set; }
    }
}
