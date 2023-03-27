using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V2.Dtos;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.V2.Controllers
{
    ///
    [ApiController]
    [ApiVersion("2.0.BETA")]
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
        ///     Método responsável por retornar todos os alunos Version 2 - Async
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repository.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPage);
            
            return Ok(alunosResult);
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