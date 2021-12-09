using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Domain.Models
{
	public class Respuesta
	{
		public int  RespuestaId {set;get;}

		[Required]
		[Column(TypeName = "varchar(50)")]
		public string Descripcion { set; get; }
		[Required]
		public bool esCorrecta { set; get; }

		public int PreguntaId { set; get; }

		public Pregunta Pregunta { set; get; }
	}
}
