using cursoADVapi.Business._Interface;
using cursoADVapi.Model._Models.Curso;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Repository.Inferface;
using ProAdvCore.Model.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace cursoADVapi.Business._Busines.Curso
{
    public class CursoBusiness : ICurso
    {
        private readonly ICursoRepository cursoRepository;
        public CursoBusiness(ICursoRepository _cursoRepository)
        {
            cursoRepository = _cursoRepository;
        }

        public string Cadastrar(CursoViewModel cursoNovo)
        {
            List<CursoModel> cursos = cursoRepository.PegarTodos();
            var cursoCadastrado = false;
            cursos.ForEach(curso =>
            {
                if (curso.Titulo == cursoNovo.Titulo)
                {
                    cursoCadastrado = true;
                }
            });

            if (!cursoCadastrado)
            {
                var obj = new CursoModel();
                obj.Estrutura = cursoNovo.Estrutura;
                obj.MaterialApoio = cursoNovo.MaterialApoio;
                obj.Objetivo = cursoNovo.Objetivo;
                obj.Subtitulo = cursoNovo.Subtitulo;
                obj.Titulo = cursoNovo.Titulo;
                obj.DataLançamento = DateTime.Now;
                obj.listaVideos = cursoNovo.listaVideos;
                obj.listaArquivosApoio = cursoNovo.listaArquivosApoio;

                cursoRepository.Cadastrar(obj);
            }
            else
            {
                return "Curso já foi cadastrado";
            }
            return "Curso registrado com sucesso";
        }

        public CursoViewModel Pegar(CursoViewModel curso)
        {
            var cursoPego = cursoRepository.Pegar(curso);
            return new CursoViewModel()
            {
                Id = cursoPego.Id,
                Titulo = cursoPego.Titulo,
                DataLançamento = cursoPego.DataLançamento,
                Subtitulo = cursoPego.Subtitulo,
                listaVideos = cursoPego.listaVideos,
                listaArquivosApoio = cursoPego.listaArquivosApoio,
                Src = cursoPego.Src,
                Estrutura = cursoPego.Estrutura,
                MaterialApoio = cursoPego.MaterialApoio,
                Objetivo = cursoPego.Objetivo,

            };
        }

        public List<CursoViewModel> PegarTodos()
        {
            var cursosPegos = cursoRepository.PegarTodos();
            var lista = new List<CursoViewModel>();
            cursosPegos.ForEach(curso =>
            {
                lista.Add(new CursoViewModel()
                {
                    Id = curso.Id,
                    Titulo = curso.Titulo,
                    DataLançamento = curso.DataLançamento,
                    Subtitulo = curso.Subtitulo,
                    MaterialApoio = curso.MaterialApoio,
                    Estrutura = curso.Estrutura,
                    Objetivo = curso.Objetivo,
                });
            });
            //var cursos = AMapper.RegisterMappings().Map(cursosPegos, new List<CursoViewModel>());

            return lista;
        }


    }
}
