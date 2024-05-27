using cursoADVapi.Model._Models.Curso;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProAdvCore.Model.Interface;
using System;
using System.Collections.Generic;

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

        public string CpfCnpj { get; set; }

        public string Cargo { get; set; }

        public string Src { get; set; }

        public List<cursoContratado> ListaCursos { get; set; }

    }

    public class cursoContratado
    {
        public string Id { get; set; }
        
        public DateTime DataContratacao { get; set; }
        
        public string ValorPago { get; set; }


    }
}
