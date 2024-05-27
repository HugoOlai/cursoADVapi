using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Repository.Inferface;
using MongoDB.Driver;
using ProAdvCore.Repository.Context;
using System;
using System.Collections.Generic;

namespace cursoADVapi.Repository.Repositorios.Usuario
{
    public class LoginRepository : MongoGeneric<UsuarioModel>, IUsuarioRepository
    {
        public void Cadastrar(UsuarioModel usuario)
        {
            Collection.InsertOne(usuario);
        }

        public bool AtualizarUsuario(UsuarioModel usuario)
        {
            var filter = Builders<UsuarioModel>.Filter.Eq(x => x.Id, usuario.Id);
            var update = Builders<UsuarioModel>.Update
                .Set(x => x.ListaCursos, usuario.ListaCursos);

            var result = Collection.UpdateOne(filter, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;

        }

        public List<UsuarioModel> pegarUsuarios()
        {
           var res = Collection.Find("{}").ToList();
           return res;
        }

        public UsuarioModel pegarUsuario(string usuarioId)
        {
            var res = Collection.Find(usuario => usuario.Id == usuarioId).SingleOrDefault();
            return res;
        }

        public UsuarioModel Login(string Email, string senha)
        {
            var res = Collection.Find(usuario => usuario.Email == Email && usuario.Senha == senha).FirstOrDefault();
            return res;
        }
    }
}
