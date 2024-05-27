using cursoADVapi.Business._Busines.Usuario;
using cursoADVapi.Business._Interface;
using cursoADVapi.Model._Models.Usuario;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace cursoADVapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AutenticadorController : ControllerBase
    {
        private readonly IUsuario usuarioBusiness;
        //private readonly ILogin LoginBusiness;
        

        public AutenticadorController(
            IUsuario _usuarioBusiness
            //ILogErrorRepository _logRepository,
            //IUsuarioAdicionalBusiness usuarioAdicionalBusiness,
            //IPerfilUsuarioAdicionalBusiness perfilUsuarioAdicionalBusiness
            )
        {
            usuarioBusiness = _usuarioBusiness;
            //logRepository = _logRepository;
            //_usuarioAdicionalBusiness = usuarioAdicionalBusiness;
            //_perfilUsuarioAdicionalBusiness = perfilUsuarioAdicionalBusiness;
        }


        [AllowAnonymous, HttpPost]
        public object Post([FromBody] AuthViewModel auth, [FromServices] Models.SigningConfigurations signingConf, [FromServices] Models.TokenConfigurations tokenConf)
        {
            bool credenciaisValidas = false;
            string email = new string(auth.Email);
            UsuarioModel usuario = null;

            var view = new UsuarioViewModel();
            //var usuarioAdicional = new UsuarioAdicionalModel();

            try
            {
                if (auth != null && auth.Validar())
                {
                    if (auth.AccessKey.Equals(tokenConf.AccessKey))
                        credenciaisValidas = true;
                }

                if (credenciaisValidas)
                {
                    usuario = usuarioBusiness.Login(auth.Email, auth.Senha);

                    if (usuario == null)
                        return Ok("Usuário ou senha incorreto");

                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(usuario.Id, "Login"),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim("usuario", usuario.Id)
                            }
                        );

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = DateTime.UtcNow.AddDays(3);
                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConf.Issuer,
                        Audience = tokenConf.Audience,
                        SigningCredentials = signingConf.SigningCredentials,
                        Subject = identity,
                        //NotBefore = dataCriacao,
                        //Expires = dataExpiracao
                    });

                    var token = handler.WriteToken(securityToken);

                    view.Email = usuario.Email;
                    view.Id = usuario.Id;
                    view.CpfCnpj = usuario.CpfCnpj;
                    view.Nome = usuario.Nome;
                    view.Telefone = usuario.Telefone;
                    view.Src = usuario.Src;
                    return signingConf.Success(token, view, dataCriacao, dataExpiracao);

                }

                //RegistrarLog(new Exception("Credênciais Inválidas"), auth, status: (int)HttpStatusCode.Unauthorized);
                return signingConf.Failed();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Usuário inativo"))
                    return StatusCode(428, ex.Message);

                //RegistrarLog(ex, auth, status: 500);
                return signingConf.Failed();
            }
        }

        [AllowAnonymous, HttpGet]
        public string get()
        {
            return "API funcionando";
        }
    }
}
