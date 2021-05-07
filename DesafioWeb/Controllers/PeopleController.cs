using System;
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
        
        private ActionResult<Peoples> GetPerCpf(string cpf)
        {
            var user = _peopleService.GetPerCpf(cpf);

            if (user == null)
            {
                return NotFound();
            }
            
            return user;
        }

        private bool VerifyCep(Peoples people)
        {
            var cep = _cepService.Get(Convert.ToString(people.Endereco.Cep));
            
            if (cep != null)
            {
                people.Endereco.Cep = cep.Cep;
                people.Endereco.Logradouro = cep.Logradouro;
                people.Endereco.Bairro = cep.Bairro;
                people.Endereco.Cidade = cep.Cidade;
                people.Endereco.Estado = cep.Estado;

                return true;
            }

            return false;
        }
        
        [HttpPost]
        public ActionResult<Users> Create(Peoples people)
        {
            var peopleDb = GetPerCpf(people.Cpf.ToString());

            if (peopleDb.Value != null && people.Cpf == peopleDb.Value.Cpf)
            {
                return BadRequest("Error: Já existe uma pessoa com esse cpf");
            }
            
            var cepValid = VerifyCep(people);

            if (cepValid)
            {
                switch (people.Sexo)
                {
                    case Sexo.Masculino:
                        people.Sexo = Sexo.Masculino;
                        break;
                    case Sexo.Feminino:
                        people.Sexo = Sexo.Feminino;
                        break;
                }
                _peopleService.Create(people);
                return CreatedAtRoute("GetPeople", new { id = people.Id }, people);
            }

            return BadRequest("CEP não existe em nosso banco de dados.");
        }
        
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Peoples peoplesIn)
        {
            var peopleDb = GetPerCpf(peoplesIn.Cpf.ToString());

            if (peopleDb.Value != null && peoplesIn.Cpf == peopleDb.Value.Cpf)
            {
                return BadRequest("Error: Já existe uma pessoa com esse cpf");
            }
            var user = _peopleService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            var cepValid = VerifyCep(peoplesIn);

            if (cepValid)
            {
                peoplesIn.Id = id;

                _peopleService.Update(id, peoplesIn);
                return Ok("Dados atualizados");

            }
            return BadRequest("CEP inexistente em nosso banco.");

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