using System;
using System.Collections.Generic;
using DesafioWeb.Models;
using DesafioWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DesafioWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CepsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly CepService _cepService;

        public CepsController(CepService cepService, IConfiguration configuration)
        {
            _config = configuration;
            _cepService = cepService;
        }

        [HttpGet]
        public ActionResult<List<Ceps>> Get() => _cepService.Get();
        
        [HttpGet("get")]
        public ActionResult<Ceps> Get([FromQuery(Name = "cep")] string cep)
        {
            cep = cep.Replace("-", "").Replace(".", "");
            var cepFind = _cepService.Get(cep);
        
            if (cepFind == null)
                return NotFound("Error: CEP Not Found");

            return cepFind;
        }
        
        private void Update(string cep, Ceps cepIn)
        {
            _cepService.Update(cep, cepIn);
        }
        
        [HttpGet("createOrUpdate")]
        public ActionResult<Ceps> CreateOrUpdate([FromQuery(Name = "cep")] string cep)
        {
            var tokenApi = _config["APICEP:Token"];
            cep = cep.Replace("-", "").Replace(".", "");
            var client = new RestClient("https://www.cepaberto.com/api/v3/");
            var request = new RestRequest($"cep?cep={cep}");
            request.AddHeader("Authorization", $"Token token={tokenApi}");

            var response = client.Get(request).Content;
            JObject json = JObject.Parse(response);

            if (response == "{}")
            {
                return NotFound("CEP inexistente.");
            }

            var cidadeApi = json["cidade"]["nome"].ToString();
            var cepApi = Convert.ToInt64(json["cep"].ToString());
            var logadrouroApi = json["logradouro"].ToString();
            var bairroApi = json["bairro"].ToString();
            var estadoApi = json["estado"]["sigla"].ToString();

            var cepFind = Get(cep).Value;

            if (cepFind == null )
            { 
                var cepCreated = new Ceps(){
                    Bairro = bairroApi,
                    Cep = cepApi,
                    Cidade = cidadeApi,
                    Logradouro = logadrouroApi,
                    Estado = estadoApi,
                };
                var created = _cepService.Create(cepCreated);
                return created;
            }
            else
            {
                if (cepFind.Bairro != bairroApi || cepFind.Logradouro != logadrouroApi || cepFind.Cidade != cidadeApi || cepFind.Estado != estadoApi)
                {
                    cepFind.Bairro = bairroApi;
                    cepFind.Logradouro = logadrouroApi;
                    cepFind.Cidade = cidadeApi;
                    cepFind.Estado = estadoApi;
                    Update(cep, cepFind);
                }
            }

            return cepFind;
        }

    }
}