using Back.Domain.IRepositories;
using Back.Domain.Models;
using Back.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Persistence.Repository
{
	public class LoginRepository:ILoginRepository
	{
		private readonly ApplicationDbContext _context;
		public LoginRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Usuario> ValidateUser (Usuario usuario)
		{
			var user = await _context.Usuarios.Where(x => x.NombreUsuario == usuario.NombreUsuario && x.Password == usuario.Password).FirstOrDefaultAsync();
			return  user;
		}
	}
}
