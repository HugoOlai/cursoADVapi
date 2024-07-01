using cursoADVapi.Business._Interface;
using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Repository.Inferface;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using MongoDB.Bson.IO;
using System.Text;
using RestSharp;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Ninject.Activation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace cursoADVapi.Business._Busines.Usuario
{
    public  class UsuarioBusiness: IUsuario
    {
        private readonly IUsuarioRepository usuarioRepository;
        public UsuarioBusiness(IUsuarioRepository _usuarioRepository)
        {
            usuarioRepository = _usuarioRepository;
        }

        public string cadastrar(UsuarioViewModel usuarioNovo)
        {
            List<UsuarioModel> usuarios = usuarioRepository.pegarUsuarios();
            var usuarioCadastrado = false;
            usuarios.ForEach(usuario =>
            {
                if (usuario.Email == usuarioNovo.Email)
                {
                    usuarioCadastrado = true;
                }
            });

            if(!usuarioCadastrado)
            {
                //var client = GetClient("https://sandbox.asaas.com/api/v3/customers", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwODE1NjY6OiRhYWNoXzJjOWM3OTBkLTMxMzgtNGQxYy1hNmJkLWM2Nzk2MDU3OGI2Mg==");
                var obj = new
                {
                    name = usuarioNovo.Nome,
                    email = usuarioNovo.Email,
                    phone = usuarioNovo.Telefone,
                    mobilePhone = usuarioNovo.Telefone,
                    cpfCnpj = usuarioNovo.CpfCnpj
                };

                //var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                //var result = client.PostAsync(content).Result;


                var options = new RestClientOptions("https://sandbox.asaas.com/api/v3/customers");
                var client = new RestClient(options);
                var request = new RestRequest("");

                request.AddHeader("accept", "application/json");
                request.AddHeader("access_token", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwODE1NjY6OiRhYWNoXzJjOWM3OTBkLTMxMzgtNGQxYy1hNmJkLWM2Nzk2MDU3OGI2Mg==");
                
                request.AddJsonBody(Newtonsoft.Json.JsonConvert.SerializeObject(obj), false);
                CadastraClienteAsaas(request, client);

                options = new RestClientOptions($"https://sandbox.asaas.com/api/v3/customers");
                client = new RestClient(options);
                request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("access_token", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwODE1NjY6OiRhYWNoXzJjOWM3OTBkLTMxMzgtNGQxYy1hNmJkLWM2Nzk2MDU3OGI2Mg==");
                
                var response = PegarUsuariosAsaas(request, client);

                var usuarioAssas = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
                foreach ( var usuario in usuarioAssas.data)
                {
                    if(usuario.cpfCnpj == usuarioNovo.CpfCnpj)
                        usuarioNovo.idAsaas = usuario.id;
                }

                usuarioRepository.Cadastrar(new UsuarioModel
                {
                    Nome = usuarioNovo.Nome,
                    idAsaas = usuarioNovo.idAsaas,
                    Email = usuarioNovo.Email,
                    Telefone = usuarioNovo.Telefone,
                    Senha = usuarioNovo.Senha,
                    CpfCnpj = usuarioNovo.CpfCnpj,
                    Src = usuarioNovo.Src,
                });

            } else
            {
                return "Usuario já foi cadastrado";
            }
            return "usuario registrado com sucesso";
        }

        public static async Task CadastraClienteAsaas(RestRequest request, RestClient client)
        {
            var response = await client.PostAsync(request);

        }

        public static dynamic PegarUsuariosAsaas(RestRequest request, RestClient client)
        {
            var response = client.Get(request);
            return response.Content;
        }

        public static HttpClient GetClient(string uri, string apiKey = "")
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(uri),
                Timeout = TimeSpan.FromMinutes(30)
            };

            if (!string.IsNullOrEmpty(apiKey))
                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public string AtualizarUsuario(UsuarioModel usuario)
        {
            try
            {
              var result = usuarioRepository.AtualizarUsuario(usuario);
              return "Curso contratado";

            } catch(Exception ex)
            {
                return "Erro na atualização do usuário";
            }
        }


        public UsuarioModel Login(string Email, string senha)
        {
            var res = usuarioRepository.Login(Email, senha);
            return res;
        }

        public UsuarioViewModel PegarUsuario(string usuarioId)
        {
            var usuario = usuarioRepository.pegarUsuario(usuarioId);
            return new UsuarioViewModel()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                CpfCnpj = usuario.CpfCnpj,
                Cargo = usuario.Cargo,
                Src = usuario.Src,
                ListaCursos = usuario.ListaCursos,
    };
        }
    }
}
