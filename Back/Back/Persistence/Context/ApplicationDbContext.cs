using Back.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Persistence.Context
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Usuario> Usuarios {set;get;}

		public DbSet<Cuestionario> Cuestionario { set; get; }
		public DbSet<Pregunta> Pregunta { set; get; }
		public DbSet<Respuesta> Respuesta { set; get; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
		{


		}

	}
}
