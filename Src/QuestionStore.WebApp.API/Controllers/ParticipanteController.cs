﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuestionStore.Core.Service;
using QuestionStore.WebApp.API.Models;

namespace QuestionStore.WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipanteController : ControllerBase
    {
        private readonly IServiceParticipante serviceParticipante;

        public ParticipanteController(IServiceParticipante serviceParticipante)
        {
            this.serviceParticipante = serviceParticipante;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var participantes = Task.Run(async () =>
            {
                return await serviceParticipante.Consulte();
            }).Result;

            return JsonConvert.SerializeObject(participantes);
        }

        // POST api/participante
        [HttpPost]
        public void Post([FromBody] CadastroParticipanteModel model)
        {
            var insertCommand = new InsertParticipanteCommand()
            {
                Nome = model.Nome
            };

            serviceParticipante.Insert(insertCommand);
        }
    }
}