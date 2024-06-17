using cursoADVapi.Model.ViewModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProAdvCore.Model.Interface;
using System.Collections.Generic;
using System;

namespace cursoADVapi.Model._Models.Curso
{
    [BsonIgnoreExtraElements]
    public class CursoModel : IMongoModel
    {

        public string CollectionName() => "curso";

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Titulo { get; set; }

        public DateTime DataLançamento { get; set; }

        public string Subtitulo { get; set; }

        public string MaterialApoio { get; set; }

        public string Estrutura { get; set; }

        public string Objetivo { get; set; }

        public List<videoModel> listaVideos { get; set; }

        public List<string> Topcos { get; set; }

        public List<ArquivosCursos> listaArquivosApoio { get; set; }

        public string Src { get; set; }

        public string Valor { get; set; }


    }

    public class videoModel
    {
        public int Modulo { get; set; }
        
        public string Aula { get; set; }
        
        public string Src { get; set; }
        
        public string Descricao { get; set; }

        public bool AulaAtual { get; set; }

        public List<arquivoModel> Arquivos { get; set; }
    }

    public class arquivoModel
    {
        public string Src { get; set; }
        public string Nome { get; set; }
    }
}
