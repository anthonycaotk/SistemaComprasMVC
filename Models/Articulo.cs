namespace SistemaComprasMVC.Models
{
    public class Articulo
    {
        public int Id { get; set; } // Identificador
        public string Descripcion { get; set; } // Descripción del artículo
        public string Marca { get; set; } // Marca del artículo
        public int UnidadDeMedidaId { get; set; } // Clave foránea a UnidadDeMedida
        public UnidadDeMedida UnidadDeMedida { get; set; } // Navegación a UnidadDeMedida
        public int Existencia { get; set; } // Cantidad en existencia
        public bool Estado { get; set; } // Estado (activo/inactivo)
    }
}
