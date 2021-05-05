using System;
using System.Collections.Generic;
using DesafioWeb.Models;
using DesafioWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleService _peopleService;
        private readonly CepService _cepService;
        
        public PeopleController(PeopleService peopleService, CepService cepService)
        {
            _peopleService = peopleService;
            _cepService = cepService;
        }
        
        [HttpGet]
        public ActionResult<List<Peoples>> Get() =>
            _peopleService.Get();

        [HttpGet("{id:length(24)}", Name = "GetPeople")]
        public ActionResult<Peoples> Get(string id)
        {
            var user = _peopleService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            
            return user;
        }
        
        [HttpPost]
        public ActionResult<Users> Create(Peoples people)
        {
            var cep = _cepService.Get(Convert.ToString(people.Endereco.Cep.Cep));

            if (cep != null)
            {
                people.Endereco.Cep = cep;
                _peopleService.Create(people);
                return CreatedAtRoute("GetPeople", new { id = people.Id }, people);
            }
            else
            {
                return BadRequest("Error: CEP não existe no banco");
            }
        }
        
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Peoples peoplesIn)
        {
            var user = _peopleService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _peopleService.Update(id, peoplesIn);

            return NoContent();
        }
        
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _peopleService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _peopleService.Remove(user.Id);

            return NoContent();
        }
        
    }
}