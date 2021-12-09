using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Domain.Models
{
	public class Pregunta
	{
		public int Id { set; get; }
		[Required]
		[Column(TypeName="varchar(100)")]
		public string Descripcion { set; get; }
		public int CuestionarioId { set; get; }
		public Cuestionario Cuestionario { set; get; }
		public List<Respuesta> listaRespuesta { set; get; }
	}
}
