using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAdministrativo.Models
{
    public class VentasViewModel
    {
        public int Id_Venta { get; set; }

        public int Id_Cliente { get; set; }

        public decimal Precio_Real_Del_Producto { get; set; }

        public decimal Precio_De_Venta_Del_Producto { get; set; }

        public string Talle { get; set; }

        public string Codigo_Articulo { get; set; }

        public string Producto_Vendido { get; set; }

        public string Fecha_De_Venta { get; set; }

        public List<SelectListItem> listadoDeClientes;
        public ICollection<ClientesViewModel> Clientes { get; set; }
        public ICollection<PagosViewModel> Pagos { get; set; }
    }
}