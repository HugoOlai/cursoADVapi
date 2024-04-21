using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Repository.Inferface;
using MongoDB.Driver;
using ProAdvCore.Repository.Context;
using System.Collections.Generic;

namespace cursoADVapi.Repository.Repositorios.Usuario
{
    public class LoginRepository : MongoGeneric<UsuarioModel>, IUsuarioRepository
    {
        public void Cadastrar(UsuarioModel usuario)
        {
            Collection.InsertOne(usuario);
        }

        public List<UsuarioModel> pegarUsuarios()
        {
           var res = Collection.Find("{}").ToList();
           return res;
        }

        public UsuarioModel Login(string Email, string senha)
        {
            var res = Collection.Find(usuario => usuario.Email == Email && usuario.Senha == senha).FirstOrDefault();
            return res;
        }
    }
}
