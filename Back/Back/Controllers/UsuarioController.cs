using Back.Domain.IService;
using Back.Domain.Models;
using Back.DTO;
using Back.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
	public class UsuarioController : Controller
	{
		private readonly IUsuarioService _usuarioService;
		public UsuarioController (IUsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}
		public async Task<IActionResult> Post(Usuario usuario)
		{
			try
			{
				var validateExitence = await _usuarioService.ValidateExistence(usuario);
				if (validateExitence)
				{
					return BadRequest(new { message = "El usuario "+usuario.NombreUsuario+" ya esxite" });
				}
				else
				{
					usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
					await _usuarioService.SaveUser(usuario);
					return Ok(new { message = "Usuario registrado con exito" });
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}
		 
		[Route("CambiarPassword")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPut]
		public async Task<IActionResult> CambiarPassword([FromBody] CambiarPassword cambiarPassword)
		{
			try
			{
				var identity = HttpContext.User.Identity as ClaimsIdentity;
			   int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
				string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPassword.passwordAnterior);
				var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncriptado);
				if (usuario == null)
				{
					return BadRequest(new { message = "Password Incorrecta" });
				}
				else
				{
					usuario.Password = Encriptar.EncriptarPassword(cambiarPassword.nuevaPassword);
					await _usuarioService.UpdatePassword(usuario);
					return Ok(new { mesaage = "La password fue actualizada con exito" });
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}


		}

	}
	
	
}
