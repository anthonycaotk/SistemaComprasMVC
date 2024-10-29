using System.ComponentModel.DataAnnotations;

namespace SistemaComprasMVC.Models
{
    public class Proveedor
    {
        public int Id { get; set; } // Identificador

        [Required(ErrorMessage = "La cédula o RNC es obligatorio.")]
        [RegularExpression(@"^[0-9]{1,3}-?[0-9]{1,9}$", ErrorMessage = "Formato de cédula o RNC no válido.")]
        public string CedulaORNC { get; set; } // Cédula o RNC

        [Required(ErrorMessage = "El nombre comercial es obligatorio.")]
        public string NombreComercial { get; set; } // Nombre del proveedor

        public bool Estado { get; set; } // Estado (activo/inactivo)

        public static bool ValidaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            if (pLongCed != 11)
                return false;
            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }
            return vnTotal % 10 == 0;
        }

        public static bool EsUnRNCValido(string pRNC)
        {
            int vnTotal = 0;
            int[] digitoMult = new int[8] { 7, 9, 8, 6, 5, 4, 3, 2 };
            string vcRNC = pRNC.Replace("-", "").Replace(" ", "");
            string vDigito = vcRNC.Substring(8, 1);
            if (vcRNC.Length != 9)
                return false;
            if (!"145".Contains(vcRNC.Substring(0, 1)))
                return false;
            for (int vDig = 1; vDig <= 8; vDig++)
            {
                int vCalculo = Int32.Parse(vcRNC.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                vnTotal += vCalculo;
            }
            return (vnTotal % 11 == 0 && vDigito == "1") ||
                   (vnTotal % 11 == 1 && vDigito == "1") ||
                   (11 - (vnTotal % 11)).Equals(vDigito);
        }
    }
}
