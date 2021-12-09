using Back.Domain.IRepositories;
using Back.Domain.IService;
using Back.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Services
{
	public class LoginService:ILoginService
	{
		private readonly ILoginRepository _loginrepository;
		public LoginService(ILoginRepository loginRepository)
		{
			_loginrepository = loginRepository;
		}
		public async Task<Usuario> ValidateUser(Usuario usuario)
		{
			return await _loginrepository.ValidateUser(usuario);
		}
	}
}
