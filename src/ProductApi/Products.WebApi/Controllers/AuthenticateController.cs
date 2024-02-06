using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Products.Application.Configuration;
using Products.Application.Users.Commands;
using Products.Application.Users.Queries;
using Products.Application.Users.Requests;
using Products.Application.Users.Responses;
using Products.Domain.Entities;
using Products.Infra.Data.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Products.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ApiController
    {
        private readonly IMediatorHandler _mediator;
        private readonly AppJwtSettings _appJwtSettings;

        public AuthenticateController(IMediatorHandler mediator, IOptions<AppJwtSettings> appJwtSettings)
        {
            _mediator = mediator;
            _appJwtSettings = appJwtSettings.Value;
        }

        /// <summary>
        /// Register account.
        /// </summary>
        [HttpPost("sign-up")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> SignUp(RegisterUserRequest request)
        {
            var result = _mediator.SendCommand(new RegisterUserCommand(request.Name, request.Email, request.Password));
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await result);
        }

        /// <summary>
        /// Login account.
        /// </summary>
        [HttpPost("sign-in")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> SignIn(LoginUserRequest request)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);
            
            var signinResult = await _mediator.SendCommand(new LoginUserCommand(request.Email, request.Password));
            if(!signinResult.IsValid) return CustomResponse(signinResult);

            var user = await _mediator.SendQuery(new GetUserByEmailQuery(request.Email));
            string token = GetFullJwt(request.Email);

            return CustomResponse(new SigninResponse(user.Id, token));
        }

        private string GetFullJwt(string email)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            byte[] bytes = Encoding.ASCII.GetBytes(_appJwtSettings.SecretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
