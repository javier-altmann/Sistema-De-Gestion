using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAdministrativo.Models
{
    public class VentasViewModel
    {

        public int Id_Cliente { get; set; }

        public decimal PrecioRealDelProducto { get; set; }

        public decimal PrecioDeVentaDelProducto { get; set; }

        public string Talle { get; set; }

        public string CodigoArticulo { get; set; }

        public string ProductoVendido { get; set; }

        public string FechaDeVenta { get; set; }

        public List<SelectListItem> listadoDeClientes;
    }
}