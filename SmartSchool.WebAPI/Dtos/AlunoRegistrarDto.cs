using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Dtos
{
    ///<summary>
    /// Dto de view de Aluno
    ///</summary>
    public class AlunoRegistrarDto
    {
        ///<summary>
        /// Identificador Id do Aluno
        ///</summary>
        public int? Id { get; set; }
        
        public int Matricula { get; set; }

        public string Nome { get; set; }

        public string? Sobrenome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; } = null;

        public bool Ativo { get; set; } = true;
    }
}