﻿using Back.Domain.IRepositories;
using System;
using Back.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Persistence.Repository
{
	public class UsuarioRepository: IUsuarioRepository
	{
		private readonly ApplicationDbContext _context;
	  public UsuarioRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task SaveUser(Usuario usuario)
		{
			 _context.Add(usuario);
		 await	_context.SaveChangesAsync();
		}

		public async Task<bool> ValidateExistence(Usuario usuario)
		{
			var validateExistence = await _context.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);
			return validateExistence;
		}
		public async Task<Usuario> ValidatePassword(int idUsuario,string passwordAnterior)
		{
			var usuario = await _context.Usuarios.Where(x=>x.Id ==idUsuario && x.Password== passwordAnterior).FirstOrDefaultAsync();
			return usuario;
		}
		public  async Task UpdatePassword(Usuario usuario)
		{
			_context.Update(usuario);
			await _context.SaveChangesAsync();
		}
	}
}
