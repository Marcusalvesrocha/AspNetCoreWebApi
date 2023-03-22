using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.V2.Dtos
{
    ///<summary>
    /// Dto de view de Aluno
    ///</summary>
    public class AlunoDto
    {
        ///<summary>
        /// Identificador Id do Aluno
        ///</summary>
        public int Id { get; set; }
        
        public int Matricula { get; set; }

        public string? Nome { get; set; }

       public string? Telefone { get; set; }

        public int Idade { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataInicio { get; set; }

        public bool Ativo { get; set; }
    }
}