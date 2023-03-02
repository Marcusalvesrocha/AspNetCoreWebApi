using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly  IMapper _mapper;
       
        public AlunoController(IRepository repository, IMapper mapper) {
           _repository = repository;
           _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);
            
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAllAlunoByDisciplinaId(id);
            return aluno == null ? BadRequest("Aluno não encontrado") : Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            return _repository.SaveChanges() ? Ok(aluno) : BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alunoPut = _repository.GetAlunoById(id);
            if(alunoPut == null) return BadRequest("Aluno não encontrado");
            _repository.Update(aluno);
            return _repository.SaveChanges() ? Ok(aluno) : BadRequest("Aluno não Atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoPatch = _repository.GetAlunoById(id);
            if(alunoPatch == null) return BadRequest("Aluno não encontrado");
            _repository.Update(aluno);
            return _repository.SaveChanges() ? Ok(aluno) : BadRequest("Aluno não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");
             _repository.Delete(aluno);
            return _repository.SaveChanges() ? Ok("aluno deletado") : BadRequest("Aluno não Atualizado");
        }
    }
}