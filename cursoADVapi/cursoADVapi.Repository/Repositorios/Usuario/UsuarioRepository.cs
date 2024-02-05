using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Repository.Inferface;
using ProAdvCore.Repository.Context;

namespace cursoADVapi.Repository.Repositorios.Usuario
{
    public class UsuarioRepository : MongoGeneric<UsuarioModel>, IUsuarioRepository
    {
        public void Cadastrar(UsuarioModel usuario)
        {
            Collection.InsertOne(usuario);
        }
    }
}
