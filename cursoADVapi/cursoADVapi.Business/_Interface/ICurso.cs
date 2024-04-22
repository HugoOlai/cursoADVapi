

using cursoADVapi.Model.ViewModel;
using System.Collections.Generic;

namespace cursoADVapi.Business._Interface
{
    public interface ICurso
    {
        string Cadastrar(CursoViewModel cursoNovo);

        public CursoViewModel Pegar(CursoViewModel curso);

        public List<CursoViewModel> PegarTodos();
    }
}
