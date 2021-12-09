﻿using Back.Domain.IRepositories;
using Back.Domain.IService;
using Back.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Services
{
	public class CuestionarioService: ICuestionarioService
	{
		private ICuestionarioRepository _cuestionarioRepository;
		public CuestionarioService(ICuestionarioRepository cuestionarioRepository)
		{
			_cuestionarioRepository = cuestionarioRepository;
		}
		public async Task CreateCuestionario (Cuestionario cuestionario)
		{
			await _cuestionarioRepository.CreateCuestionario(cuestionario);
		}

		public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
		{
			return await _cuestionarioRepository.GetListCuestionarioByUser(idUsuario);
		}
		public async Task<Cuestionario> GetCuestionario(int idCuestionario)
		{
			return await _cuestionarioRepository.GetCuestionario(idCuestionario);
		}

		public async Task<Cuestionario> BuscarCuestionario(int idCuestionario,int idUsuario)
		{
			return await _cuestionarioRepository.BuscarCuestionario(idCuestionario,idUsuario);
		}

		public async Task EliminarCuestionario(Cuestionario cuestionario)
		{
			  await _cuestionarioRepository.EliminarCuestionario(cuestionario);
		}

		public async Task<List<Cuestionario>> GetListCuestionarios()
		{
			return await _cuestionarioRepository.GetListCuestionarios();
		}
	}
}
