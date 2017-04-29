using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaAdministrativo.Models
{
    public class ListaDePagosViewModel
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string CodigoDeArticulo { get; set; }

        public string Producto { get; set; }

        public decimal SaldoActual { get; set; }

        public DateTime FechaDePago { get; set; }
    }
}