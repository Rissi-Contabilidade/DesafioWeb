using System.Threading.Tasks;
using DesafioWeb.Models;
using DesafioWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        
        public LoginController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public ActionResult<dynamic> Login(Users u)
        {
            var user = _userService.Login(u.User, u.Password);

            if(user == null)
                return NotFound(new {message = "Usuário ou senha inválidos" });

            var token = _userService.GenerateToken(user);
            return new
            {
                Autheticated = "true",
                token = token
            };
        }
    }
}