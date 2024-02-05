using cursoADVapi.Business._Interface;
using cursoADVapi.Model.ViewModel;
using System.ComponentModel;

namespace cursoADVapi.Business._Busines.Usuario
{
    public  class UsuarioBusiness: IUsuario
    {
        //private IUsuarioRepository usuarioRepository = Container.Get<IUsuarioRepository>();

        public string cadastrar(UsuarioViewModel usuario)
        {
            //usuarioRepository.Cadastrar(usuario);
            return "usuario registrado com sucesso";
        }

    }
}
