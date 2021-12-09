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
	public class CuestionarioRepository:ICuestionarioRepository
	{
		private readonly ApplicationDbContext _context;

		public CuestionarioRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task CreateCuestionario(Cuestionario cuestionario)
		{
			_context.Add(cuestionario);
			await _context.SaveChangesAsync();
		}
		public async Task <List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
		{
			var listCuestionario= await _context.Cuestionario.Where(x=>x.Activo== 1 && x.UsuarioId==idUsuario).ToListAsync();
			return listCuestionario;
		}
		public async Task<Cuestionario> GetCuestionario(int idCuestionario)
		{
			var cuestionario = await _context.Cuestionario.Where(x => x.Id == idCuestionario && x.Activo == 1)
				.Include(x=>x.listaPreguntas)
				.ThenInclude(x=>x.listaRespuesta)
				.FirstOrDefaultAsync();
			return cuestionario;
		}
		public async Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUsuario)
		{
			var cuestionario = await _context.Cuestionario.Where(x => x.Id == idCuestionario && x.Activo == 1 &&  x.UsuarioId==idUsuario).FirstOrDefaultAsync();
			return cuestionario;
		}
		public async Task EliminarCuestionario(Cuestionario cuestionario)
		{
			cuestionario.Activo = 0;
			_context.Entry(cuestionario).State = EntityState.Modified;
			await _context.SaveChangesAsync();


		}

		public async Task<List<Cuestionario>> GetListCuestionarios()
		{
			var listCuestionarios = await _context.Cuestionario.Where(x => x.Activo == 1).Include(x => x.Usuario).ToListAsync();
			return listCuestionarios;
		}
	}
}
