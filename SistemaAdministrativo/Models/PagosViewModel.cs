using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAdministrativo.Models
{
    public class PagosViewModel
    {
        public List<SelectListItem> listaDeClientes;
        public List<SelectListItem> listaDeProductos;

        [Required]
        public int Id_Pago { get; set; }

        [Required]
        public int Id_Cliente { get; set; }

        [Required]
        public int Id_Venta { get; set; }

        [Required(ErrorMessage = "El campo Cantidad Pagada es obligatorio")]
        public decimal Cantidad_Pagada { get; set; }

        public DateTime FechaDePago { get; set; }

        public decimal SaldoActual { get; set; }






    }
}