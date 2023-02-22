using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
    public SmartContext Context { get; }

        private readonly SmartContext _context;
        private readonly IRepository _repository;
       
        public AlunoController(SmartContext context,
                               IRepository repository) {
           _context = context;
           _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            return aluno == null ? BadRequest("Aluno não encontrado") : Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
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
            var alunoPut = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alunoPut == null) return BadRequest("Aluno não encontrado");
            _repository.Update(aluno);
            return _repository.SaveChanges() ? Ok(aluno) : BadRequest("Aluno não Atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoPatch = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alunoPatch == null) return BadRequest("Aluno não encontrado");
            _repository.Update(aluno);
            return _repository.SaveChanges() ? Ok(aluno) : BadRequest("Aluno não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado");
             _repository.Delete(aluno);
            return _repository.SaveChanges() ? Ok("aluno deletado") : BadRequest("Aluno não Atualizado");
        }
    }
}