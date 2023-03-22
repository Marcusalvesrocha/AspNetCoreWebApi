using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public IActionResult Post(ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = _mapper.Map<Professor>(professorRegistrarDto);
            _repository.Add(professor);
            return _repository.SaveChanges() 
                ? Created($"/api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor)) 
                : BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = _repository.GetProfessoreById(id);
            if(professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(professorRegistrarDto, professor);
            _repository.Update(professor);
            return _repository.SaveChanges() 
                ? Created($"/api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor)) 
                : BadRequest("Professor não Atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = _repository.GetProfessoreById(id);
            if(professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(professorRegistrarDto, professor);
            _repository.Update(professor);
            return _repository.SaveChanges() ? Ok(professor) : BadRequest("Professor não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessoreById(id);
            if (professor == null) return BadRequest("professor não encontrado");
            _repository.Delete(professor);
            return _repository.SaveChanges() 
                ? Ok("professor deletado") 
                : BadRequest("Professor Deletado");
        }
    }
}