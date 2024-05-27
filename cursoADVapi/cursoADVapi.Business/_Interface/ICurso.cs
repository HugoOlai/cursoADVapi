

using cursoADVapi.Model.ViewModel;
using System.Collections.Generic;

namespace cursoADVapi.Business._Interface
{
    public interface ICurso
    {
        string Cadastrar(CursoViewModel cursoNovo);

        string Contratar(string usuarioId, CursoViewModel curso);
        
        public CursoViewModel Pegar(CursoViewModel curso);

        public List<CursoViewModel> PegarTodos();
    }
}
