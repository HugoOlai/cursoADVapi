using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Repository.Inferface;
using MongoDB.Driver;
using ProAdvCore.Repository.Context;

namespace cursoADVapi.Repository.Repositorios.Usuario
{
    public class UsuarioRepository : MongoGeneric<UsuarioModel>, IUsuarioRepository
    {
        public void Cadastrar(UsuarioModel usuario)
        {
            Collection.InsertOne(usuario);
        }

        public void pegarUsuarios()
        {
            var res = Collection.Find("{}");
        }
    }
}
