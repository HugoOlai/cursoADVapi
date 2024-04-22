using cursoADVapi.Model._Models.Curso;
using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Repository.Inferface;
using MongoDB.Driver;
using ProAdvCore.Repository.Context;
using System.Collections.Generic;

namespace cursoADVapi.Repository.Repositorios.Curso
{
    public class CursoRepository : MongoGeneric<CursoModel>, ICursoRepository
    {
        public void Cadastrar(CursoModel curso)
        {
            Collection.InsertOne(curso);
        }

        public CursoModel Pegar(CursoViewModel Curso)
        {
           var res = Collection.Find(x=> x.Id == Curso.Id || x.Titulo == Curso.Titulo).FirstOrDefault();
           return res;
        }

        public List<CursoModel> PegarTodos()
        {
                var res = Collection.Find("{}").ToList();
                return res;
            }
        }
}
