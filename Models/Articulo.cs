using System.ComponentModel.DataAnnotations;

namespace SistemaComprasMVC.Models
{
    public class Articulo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
        public int UnidadDeMedidaId { get; set; }

        [Required(ErrorMessage = "La existencia es obligatoria.")]
        public int Existencia { get; set; }

        public bool Estado { get; set; }

        public virtual UnidadDeMedida UnidadDeMedida { get; set; }
    }
}
