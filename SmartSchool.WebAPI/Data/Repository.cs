using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
  public class Repository : IRepository
  {
    private readonly SmartContext _context;
    public Repository(SmartContext context)
    {
        _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() > 0);
    }

    public Aluno[] GetAllAlunos(bool includeDisciplina = false)
    {
      IQueryable<Aluno> query = _context.Alunos;

      if (includeDisciplina)
      {
        query = query.Include(a => a.AlunosDiciplinas)
                     .ThenInclude(ad => ad.Disciplina)
                     .ThenInclude(d => d.Professor);
      }
      query = query.AsNoTracking().OrderBy(a => a.Id);

      return query.ToArray();
    }

    public async Task<PageList<Aluno>> GetAllAlunosAsync(
      PageParams pageParams,
      bool includeDisciplina = false)
    {
      IQueryable<Aluno> query = _context.Alunos;

      if (includeDisciplina)
      {
        query = query.Include(a => a.AlunosDiciplinas)
                     .ThenInclude(ad => ad.Disciplina)
                     .ThenInclude(d => d.Professor);
      }
      query = query.AsNoTracking().OrderBy(a => a.Id);

      if (!string.IsNullOrEmpty(pageParams.Nome))
      {
        query = query.Where(aluno => aluno.Nome
                                            .ToUpper()
                                            .Contains(pageParams.Nome.ToUpper()) ||
                                     aluno.Sobrenome
                                            .ToUpper()
                                            .Contains(pageParams.Nome.ToUpper()));
      }

      if (pageParams.Matricula != null)
        query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);

      if (pageParams.Ativo != null)
        query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));

      //return await query.ToListAsync();
      return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
    }

    public Aluno[] GetAllAlunoByDisciplinaId(int disciplinaId, bool includeDisciplina)
    {
      IQueryable<Aluno> query = _context.Alunos;

      if (includeDisciplina)
      {
        query = query.Include(a => a.AlunosDiciplinas)
                     .ThenInclude(ad => ad.Disciplina)
                     .ThenInclude(d => d.Professor);
      }

      query = query.AsNoTracking()
                   .OrderBy(a => a.Id)
                   .Where(aluno => aluno.AlunosDiciplinas!.Any(a => a.DisciplinaId == disciplinaId));

      return query.ToArray();
    }

    public Aluno GetAlunoById(int AlunoId, bool includeDisciplina = false)
    {
      IQueryable<Aluno> query = _context.Alunos;

      if (includeDisciplina)
      {
        query = query.Include(a => a.AlunosDiciplinas)
                     .ThenInclude(ad => ad.Disciplina)
                     .ThenInclude(d => d.Professor);
      }

      query = query.AsNoTracking()
                   .OrderBy(a => a.Id)
                   .Where(aluno => aluno.Id == AlunoId);

      return query.FirstOrDefault();
    }

    public Professor[] GetAllProfessores(bool includeAlunos = false)
    {
      IQueryable<Professor> query = _context.Professores;

      if (includeAlunos)
      {
        query = query.Include(p => p.Disciplinas)
                      .ThenInclude(d => d.AlunosDiciplinas)
                      .ThenInclude(ad => ad.Aluno);
      }

      query = query.AsNoTracking().OrderBy(p => p.Id);

      return query.ToArray();
    }

    public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos)
    {
      IQueryable<Professor> query = _context.Professores;

      if (includeAlunos)
      {
        query = query.Include(p => p.Disciplinas)
                      .ThenInclude(d => d.AlunosDiciplinas)
                      .ThenInclude(ad => ad.Aluno);
      }

      query = query.AsNoTracking()
                   .OrderBy(a => a.Id)
                   .Where(aluno => aluno.Disciplinas.Any(d => d.AlunosDiciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

      return query.ToArray();
    }

    public Professor GetProfessoreById(int professorId, bool includeAlunos)
    {
      IQueryable<Professor> query = _context.Professores;

      if (includeAlunos)
      {
        query = query.Include(p => p.Disciplinas)
                      .ThenInclude(d => d.AlunosDiciplinas)
                      .ThenInclude(ad => ad.Aluno);
      }

      query = query.AsNoTracking()
                   .OrderBy(p => p.Id)
                   .Where(professor => professor.Id == professorId);

      return query.FirstOrDefault();
    }
  }
}