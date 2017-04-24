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

        public string Nombre_De_Producto { get; set; }

        [StringLength(200), Required(ErrorMessage = "El campo Talle es obligatorio")]
        public string Talle_Del_Producto { get; set; }
           
        public string Nombre_Del_Cliente { get; set; }

        public string Fecha_Del_Pedido { get; set; }

        public string Articulo { get; set; }

    }
}