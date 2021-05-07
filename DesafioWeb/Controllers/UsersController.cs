using System.Collections.Generic;
using DesafioWeb.Models;
using DesafioWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<List<Users>> Get() =>
            _userService.Get();

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<Users> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private bool VerifyUser(Users user)
        {
            var userFind = _userService.GetPerUser(user.User);
            if (userFind != null && user.User == userFind.User)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public ActionResult<Users> Create(Users user)
        {
            var userFound = VerifyUser(user);

            if (userFound)
            {
                return BadRequest("Error: Usuário já existe!");
            }
            
            _userService.Create(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Users userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            userIn.Id = id;
            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.Id);

            return NoContent();
        }
        
    }

}