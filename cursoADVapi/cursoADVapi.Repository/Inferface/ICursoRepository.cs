using cursoADVapi.Model._Models.Curso;
using cursoADVapi.Model.ViewModel;
using ProAdvCore.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursoADVapi.Repository.Inferface
{
    public interface ICursoRepository : IMongoGeneric<CursoModel>
    {
        void Cadastrar(CursoModel curso);

        CursoModel Pegar(CursoViewModel Curso);

        List<CursoModel> PegarTodos();


    }
}
