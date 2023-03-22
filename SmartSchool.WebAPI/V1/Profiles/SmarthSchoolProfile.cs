using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.V1.Profiles
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
          CreateMap<AlunoDto, Aluno>();
          CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

          CreateMap<Professor, ProfessorDto>()
            .ForMember(
              dest => dest.NomeCompleto,
              opt =>opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
            )
            .ForMember(
              dest => dest.TempoLecionando,
              opt => opt.MapFrom(src => src.DataInicio.GetCurrencyAge())
            );
          CreateMap<ProfessorDto, Professor>();
          CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();
        }
    }
}