

using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Model.ViewModel;

namespace cursoADVapi.Business._Interface
{
    public interface IUsuario
    {
        string cadastrar(UsuarioViewModel usuario);

        UsuarioModel Login(string Email, string senha);
    }
}
