using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeAviones.Model
{
    [Table("Persona")]
    public class Avion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Modelo es requerido")]
        public string Modelo { get; set; }

        public Estado Estado { get; set; }
    }
}
