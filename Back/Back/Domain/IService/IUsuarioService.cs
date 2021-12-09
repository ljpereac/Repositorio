using Back.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Domain.IService
{
	public interface IUsuarioService
	{
		Task SaveUser(Usuario usuario);
		Task<bool> ValidateExistence(Usuario usuario);
		Task<Usuario> ValidatePassword(int id, string passwordAnterior);

		Task UpdatePassword(Usuario usuario);
	}
}
