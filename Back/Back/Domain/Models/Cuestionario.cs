using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Domain.Models
{
	public class Cuestionario
	{
		public int Id { set; get; }

		[Required]
		[Column(TypeName="varchar(100)")]
		public string Nombre {set;get;}
		[Required]
		[Column(TypeName = "varchar(150)")]
		public string Descripcion { set; get; }
		public DateTime FechaCreacion { set; get; }
		public int Activo { set; get; }
		public int UsuarioId { set; get; }
		public Usuario Usuario { set; get; }
		public List<Pregunta> listaPreguntas { set; get; }
	}
}
