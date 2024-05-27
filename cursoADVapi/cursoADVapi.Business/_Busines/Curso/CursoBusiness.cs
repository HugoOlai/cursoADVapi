using cursoADVapi.Business._Interface;
using cursoADVapi.Model._Models.Curso;
using cursoADVapi.Model._Models.Usuario;
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
        private readonly IUsuario usuarioBusiness;
        public CursoBusiness(ICursoRepository _cursoRepository, IUsuario _usuarioBusiness)
        {
            cursoRepository = _cursoRepository;
            usuarioBusiness = _usuarioBusiness;
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
                obj.Topcos = cursoNovo.Topcos;
                obj.listaArquivosApoio = cursoNovo.listaArquivosApoio;

                cursoRepository.Cadastrar(obj);
            }
            else
            {
                return "Curso já foi cadastrado";
            }
            return "Curso registrado com sucesso";
        }

        public string Contratar(string usuarioId, CursoViewModel curso)
        {
            var usuario = usuarioBusiness.PegarUsuario(usuarioId);
            if (usuario.ListaCursos == null)
                usuario.ListaCursos = new List<cursoContratado>();

            usuario.ListaCursos.Add(new cursoContratado { 
                Id = curso.Id,
                DataContratacao = DateTime.Now,
                ValorPago = curso.valor
            });
            try
            {
                return usuarioBusiness.AtualizarUsuario(new UsuarioModel { 
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    CpfCnpj = usuario.CpfCnpj,
                    Cargo = usuario.Cargo,
                    Src = usuario.Src,
                    ListaCursos = usuario.ListaCursos,
                });
            }
            catch (Exception ex) {
                return "Erro na contratação";

            }

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
                Topcos = cursoPego.Topcos,

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
                    Topcos = curso.Topcos,
                    Src = curso.Src,

                });
            });
            //var cursos = AMapper.RegisterMappings().Map(cursosPegos, new List<CursoViewModel>());

            return lista;
        }


    }
}
