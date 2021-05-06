using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DesafioWeb.Models;
using DesafioWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DesafioWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;

        private IConfiguration _config;
        public LoginController(IConfiguration Configuration, UserService userService)
        {
            _userService = userService;
            _config = Configuration;
        }

        private string GenerateToken(Users user)
        {
            var secret = _config["JWT:Secret"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.User)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost]
        public ActionResult<dynamic> Login(Users u)
        {
            var user = _userService.Login(u.User, u.Password);

            if(user == null)
                return NotFound(new {message = "Usuário ou senha inválidos" });

            var token = GenerateToken(user);
            return new
            {
                Autheticated = "true",
                token = token
            };
        }
    }
}