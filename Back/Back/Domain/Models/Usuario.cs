using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Domain.Models
{
    public class Usuario
    {
        public int Id { set; get; }
        [Required]
        [Column(TypeName ="varchar(20)")]
        public string NombreUsuario { set; get; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Password { set; get; }
    }
}
