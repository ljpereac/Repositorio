using Back.Domain.IService;
using Back.Domain.Models;
using Back.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Back.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuestionarioController : ControllerBase
	{
		private readonly ICuestionarioService _cuestionarioService;
		public CuestionarioController(ICuestionarioService cuestionarioService)
		{
			_cuestionarioService = cuestionarioService;
		}


		
		[HttpPost]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Post([FromBody]Cuestionario cuestionario)
		{
			try
			{
				var identity = HttpContext.User.Identity as ClaimsIdentity;
				int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
				cuestionario.UsuarioId = idUsuario;
				cuestionario.Activo = 1;
				cuestionario.FechaCreacion = DateTime.Now;
				await _cuestionarioService.CreateCuestionario(cuestionario);
				return Ok(new { message = "Se agrego el cuestionario exitosamente" });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[Route("GetListCuestioanrioByUser")]
		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetListCuestioanrioByUser()
		{
			try
			{
				var identity = HttpContext.User.Identity as ClaimsIdentity;
				int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
				var listCuestionario = await _cuestionarioService.GetListCuestionarioByUser(idUsuario); 
				return Ok(listCuestionario);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet("(idCuestionario)")]
		public async Task<IActionResult> Get(int idCuestionario)
		{
			try
			{
				var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);
				return Ok(cuestionario);

			}catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("(idCuestionario)")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Delete(int idCuestionario,int idUsuario)
		{
			try
			{
				var cuestionario = await _cuestionarioService.BuscarCuestionario(idCuestionario,idUsuario);
				if (cuestionario == null)
				{
					return BadRequest(new { message = "No se encontro ningun cuestionario" });
				}
				await _cuestionarioService.EliminarCuestionario(cuestionario);
				return Ok(new { message="El cuestionario ha sido eliminado"});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[Route("GetListCuestioanrios")]
		public async Task<IActionResult> GetListCuestionarios()
		{
			try
			{
				var listCuestionario = await _cuestionarioService.GetListCuestionarios();
				return Ok(listCuestionario);

			}catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}
}
