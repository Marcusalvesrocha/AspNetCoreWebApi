using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessoreById(id);
            return professor == null 
                ? BadRequest("Professor não encontrado") 
                : Ok(_mapper.Map<ProfessorDto>(professor));
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            return _repository.SaveChanges() ? Ok(professor) : BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var professorPut = _repository.GetProfessoreById(id);
            if(professorPut == null) return BadRequest("Professor não encontrado");
            _repository.Update(professor);
            return _repository.SaveChanges() ? Ok(professor) : BadRequest("Professor não Atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var professorPatch = _repository.GetProfessoreById(id);
            if(professorPatch == null) return BadRequest("Aluno não encontrado");
            _repository.Update(professor);
            return _repository.SaveChanges() ? Ok(professor) : BadRequest("Professor não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessoreById(id);
            if (professor == null) return BadRequest("professor não encontrado");
            _repository.Delete(professor);
            return _repository.SaveChanges() ? Ok(professor) : BadRequest("Professor Deletado");
        }
    }
}