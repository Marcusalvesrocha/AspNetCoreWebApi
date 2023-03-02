using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Helpers
{
    public class SmarthSchoolProfile : Profile
    {
        public SmarthSchoolProfile()
        {
          CreateMap<Aluno, AlunoDto>()
            .ForMember(
              dest => dest.Nome,
              opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
            )
            .ForMember(
              dest => dest.Idade,
              opt => opt.MapFrom(src => src.DataNascimento.GetCurrencyAge())
            );
        }
    }
}