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
        public AutenticadorController(
            //IUsuarioBusiness _business,
            //ILogErrorRepository _logRepository,
            //IUsuarioAdicionalBusiness usuarioAdicionalBusiness,
            //IPerfilUsuarioAdicionalBusiness perfilUsuarioAdicionalBusiness
            )
        {
            //usuarioBusiness = _business;
            //logRepository = _logRepository;
            //_usuarioAdicionalBusiness = usuarioAdicionalBusiness;
            //_perfilUsuarioAdicionalBusiness = perfilUsuarioAdicionalBusiness;
        }


        [AllowAnonymous, HttpPost]
        public object Post([FromBody] AuthViewModel auth, [FromServices] Models.SigningConfigurations signingConf, [FromServices] Models.TokenConfigurations tokenConf)
        {
            bool credenciaisValidas = false;
            string email = new string(auth.Email);
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
                    ClaimsIdentity identity = new ClaimsIdentity();

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
}
