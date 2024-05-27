using cursoADVapi.Business._Interface;
using cursoADVapi.Model.ViewModel;
using cursoADVapi.Seguranca;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace cursoADVapi.Controllers
{
    [Authorize("Bearer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]"), ApiController]

    public class UsuarioController : ControllerBase
    {
        //private IUsuario _usuario = Container.Get<IUsuario>();

        private readonly IUsuario _usuario;

        //private readonly ILogger<WeatherForecastController> _logger;

        //public UsuarioController(IUsuario<UsuarioController> usuasrio)
        //{
        //    _usuario = usuario;
        //}

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpPost, AllowAnonymous, Route("Cadastrar")]
        public ActionResult Cadastrar([FromBody] UsuarioViewModel view)
        {
            try
            {
                var resultado = _usuario.cadastrar(view);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet, Route("PegarUsuario")]
        public ActionResult PegarUsuario()
        {
            string usuarioId = User.FindFirst("usuario").Value;

            try
            {
                var resultado = _usuario.PegarUsuario(usuarioId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
