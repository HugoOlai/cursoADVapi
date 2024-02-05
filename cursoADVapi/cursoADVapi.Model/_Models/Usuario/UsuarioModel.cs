using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProAdvCore.Model.Interface;

namespace cursoADVapi.Model._Models.Usuario
{
    [BsonIgnoreExtraElements]
    public class UsuarioModel : IMongoModel
    {

        public string CollectionName() => "usuario";

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Senha { get; set; }
    }
}
