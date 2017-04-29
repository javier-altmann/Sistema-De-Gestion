using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaAdministrativo.Models
{
    public class PedidosViewModel
    {

        public int Id_Pedidos { get; set; }

        public string NombreDeProducto { get; set; }

        [StringLength(200), Required(ErrorMessage = "El campo Talle es obligatorio")]
        public string TalleDelProducto { get; set; }

        public string NombreDelCliente { get; set; }

        public string FechaDelPedido { get; set; }

        public string Articulo { get; set; }
    }
}