using cursoADVapi.Model._Models.Usuario;
using ProAdvCore.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursoADVapi.Repository.Inferface
{
    public interface IUsuarioRepository : IMongoGeneric<UsuarioModel>
    {
        void Cadastrar(UsuarioModel usuario);

    }
}
