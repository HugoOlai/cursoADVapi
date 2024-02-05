using cursoADVapi.Business._Interface;
using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Repository.Inferface;
using System.ComponentModel;

namespace cursoADVapi.Business._Busines.Usuario
{
    public  class UsuarioBusiness: IUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioBusiness(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public string cadastrar(UsuarioViewModel usuario)
        {

            _usuarioRepository.Cadastrar(new UsuarioModel { 
                Nome = usuario.Nome,
                Email = usuario.Email,
                Sobrenome = usuario.Sobrenome,
                Telefone = usuario.Telefone,
                Senha = usuario.Senha
            });
            return "usuario registrado com sucesso";
        }

    }
}
