using cursoADVapi.Business._Interface;
using cursoADVapi.Model._Models.Curso;
using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Repository.Inferface;
using Microsoft.Extensions.Options;
using Ninject.Activation;
using ProAdvCore.Model.Mappers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public string Contratar(string usuarioId, usuarioCurso obj)
        {
            var usuario = usuarioBusiness.PegarUsuario(usuarioId);
            if (usuario.ListaCursos == null)
                usuario.ListaCursos = new List<cursoContratado>();

            usuario.ListaCursos.Add(new cursoContratado { 
                Id = obj.Curso.Id,
                DataContratacao = DateTime.Now,
                ValorPago = obj.Curso.valor
            });

            var options = new RestClientOptions("https://sandbox.asaas.com/api/v3/creditCard/tokenize");
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("accept", "application/json");
            request.AddHeader("access_token", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwODE1NjY6OiRhYWNoXzJjOWM3OTBkLTMxMzgtNGQxYy1hNmJkLWM2Nzk2MDU3OGI2Mg==");

            var creditCard = new
            {
                holderName = usuario.Cartao.NomeCartao,
                number = usuario.Cartao.NumeroCartao,
                expiryMonth = usuario.Cartao.MesExpira,
                expiryYear = usuario.Cartao.AnoExpira,
                cvv = usuario.Cartao.Cvv
            };

            var creditCardHolderInfo = new
            {
                name = usuario.Nome,
                email = usuario.Email ,
                cpfCnpj = usuario.CpfCnpj,
                postalCode = usuario.Endereco.Cep,
                addressNumber = usuario.Endereco.Numero,
                phone = usuario.Telefone,
                remoteIp = usuario.Ip,
            };

            var objBody = new
            {
                customer = usuario.Id,
                creditCard = creditCard,
                creditCardHolderInfo = creditCardHolderInfo
            };
        

            request.AddJsonBody(Newtonsoft.Json.JsonConvert.SerializeObject(objBody), false);
            //CadastraClienteAsaas(request, client);

            //var usuarioAssas = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

            try
            {
                //return usuarioBusiness.AtualizarUsuario(new UsuarioModel { 
                //    Id = usuario.Id,
                //    Nome = usuario.Nome,
                //    Sobrenome = usuario.Sobrenome,
                //    Email = usuario.Email,
                //    Telefone = usuario.Telefone,
                //    CpfCnpj = usuario.CpfCnpj,
                //    Cargo = usuario.Cargo,
                //    Src = usuario.Src,
                //    ListaCursos = usuario.ListaCursos,
                //});
                return null;
            }
            catch (Exception ex) {
                return "Erro na contratação";

            }

        }

        public static async Task CadastraClienteAsaas(RestRequest request, RestClient client)
        {
            var response = await client.PostAsync(request);

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
                valor = cursoPego.Valor,

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
