using cursoADVapi.Business._Interface;
using cursoADVapi.Model.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cursoADVapi.Controllers
{
    [Authorize("Bearer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]"), ApiController]

    public class CursoController : ControllerBase
    {
        private readonly ICurso curso;

        public CursoController(ICurso _curso)
        {
            curso = _curso;
        }

        [HttpPost, AllowAnonymous, Route("Cadastrar")]
        public ActionResult Cadastrar([FromBody] CursoViewModel view)
        {
            try
            {
                var resultado = curso.Cadastrar(view);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost, Route("Contratar")]
        public ActionResult Contratar([FromBody] usuarioCurso view)
        {
            string usuarioId = User.FindFirst("usuario").Value;
            try
            {
                var resultado = curso.Contratar(usuarioId, view);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost, AllowAnonymous, Route("pegar")]
        public ActionResult Pegar([FromBody] CursoViewModel view)
        {
            try
            {
                var resultado = curso.Pegar(view);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet, AllowAnonymous, Route("pegarTodos")]
        public ActionResult PegarTodos()
        {
            try
            {
                var resultado = curso.PegarTodos();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
