using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursoADVapi.Model.ViewModel
{
    public class CursoViewModel
    {
        public string Id { get; set; }

        public string Titulo { get; set; }

        public DateTime DataLançamento { get; set; }

        public string Subtitulo { get; set; }

        public string MaterialApoio { get; set; }

        public string Estrutura { get; set; }

        public string Objetivo { get; set; }

        public string Video { get; set; }

        public string Arquivo { get; set; }

        public List<string> listaVideos { get; set; }

        public List<ArquivosCursos> listaArquivosApoio { get; set; }

        public string Src { get; set; }
    }

    public class ArquivosCursos
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string file { get; set; }
        public string base64 { get; set; }

        //public string Id { get; set; }
        //public double lastModified { get; set; }
        //public DateTime lastModifiedDate { get; set; }
        //public string name { get; set; }
        //public double size { get; set; }
        //public string type { get; set; }
        //public string webkitRelativePath { get; set; }
    }
}
