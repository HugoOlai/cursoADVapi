using cursoADVapi.Business._Interface;
using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Repository.Inferface;
using System.Collections.Generic;
using System.ComponentModel;

namespace cursoADVapi.Business._Busines.Usuario
{
    public  class UsuarioBusiness: IUsuario
    {
        private readonly IUsuarioRepository usuarioRepository;
        public UsuarioBusiness(IUsuarioRepository _usuarioRepository)
        {
            usuarioRepository = _usuarioRepository;
        }

        public string cadastrar(UsuarioViewModel usuarioNovo)
        {
            List<UsuarioModel> usuarios = usuarioRepository.pegarUsuarios();
            var usuarioCadastrado = false;
            usuarios.ForEach(usuario =>
            {
                if (usuario.Email == usuarioNovo.Email)
                {
                    usuarioCadastrado = true;
                }
            });

            if(!usuarioCadastrado)
            {
                usuarioRepository.Cadastrar(new UsuarioModel
                {
                    Nome = usuarioNovo.Nome,
                    Email = usuarioNovo.Email,
                    Telefone = usuarioNovo.Telefone,
                    Senha = usuarioNovo.Senha,
                    cpfCnpj = usuarioNovo.CpfCnpj,
                    Src = usuarioNovo.Src,
                });
            } else
            {
                return "Usuario já foi cadastrado";
            }
            return "usuario registrado com sucesso";
        }


        public UsuarioModel Login(string Email, string senha)
        {
            var res = usuarioRepository.Login(Email, senha);
            return res;
        }
    }
}
