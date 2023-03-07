using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        
        public int Registro { get; set; }

        public string? NomeCompleto { get; set; }

        public string? Telefone { get; set; }

        public int TempoLecionando { get; set; }

        public DateTime DataInicio { get; set; }

        public bool Ativo { get; set; } = true;
    }
}