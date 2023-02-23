using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Aluno[] GetAllAlunos(bool includeDisciplina);

        Aluno[] GetAllAlunoByDisciplinaId(int disciplinaId, bool includeDisciplina = false);

        Aluno GetAlunoById(int AlunoId, bool includeDisciplina = false);

        Professor[] GetAllProfessores(bool includeAlunos = false);

        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);

        Professor GetProfessoreById(int professorId, bool includeAlunos = false);
    }
}