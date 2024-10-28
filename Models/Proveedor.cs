namespace SistemaComprasMVC.Models
{
    public class Proveedor
    {
        public int Id { get; set; } // Identificador
        public string CedulaORNC { get; set; } // Cédula o RNC
        public string NombreComercial { get; set; } // Nombre del proveedor
        public bool Estado { get; set; } // Estado (activo/inactivo)
    }
}
