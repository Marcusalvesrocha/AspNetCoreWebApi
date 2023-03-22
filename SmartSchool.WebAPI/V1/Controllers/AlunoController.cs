using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    ///
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly  IMapper _mapper;
       
        ///<summary>
        ///
        ///</summary>
        public AlunoController(IRepository repository, IMapper mapper) {
           _repository = repository;
           _mapper = mapper;
        }

        /// <summary>
        ///     Método responsável por retornar todos os alunos
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);
            
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        /// <summary>
        ///     Método responsável por retornar um AlunoDTO
        /// </summary>
        [HttpGet("getRegistrar")]
        public IActionResult getRegister(){
            return Ok(new AlunoRegistrarDto());
        }

        /// <summary>
        ///     Método responsável por retornar um Auno por ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id);
            return aluno == null 
                ? BadRequest("Aluno não encontrado") 
                : Ok(_mapper.Map<AlunoDto>(aluno));
        }

        /// <summary>
        ///     Método para inserir aluno
        /// </summary>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto alunoDto)
        {
            var aluno = _mapper.Map<Aluno>(alunoDto);
            _repository.Add(aluno);
            return _repository.SaveChanges() 
                ? Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno)) 
                : BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto alunoRegistrarDto)
        {
            var aluno = _repository.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(alunoRegistrarDto, aluno);
            _repository.Update(aluno);

            return _repository.SaveChanges() 
                ? Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno)) 
                : BadRequest("Aluno não Atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto alunoRegistrarDto)
        {
            var aluno = _repository.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(alunoRegistrarDto, aluno);
            _repository.Update(aluno);
            
            return _repository.SaveChanges() 
                ? Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno)) 
                : BadRequest("Aluno não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");
             _repository.Delete(aluno);
            return _repository.SaveChanges() 
                ? Ok("aluno deletado") 
                : BadRequest("Aluno não Atualizado");
        }
    }
}