using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaAdministrativo.Models
{
    public class ClientesViewModel
    {

        public int Id_Cliente { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(15)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Apellido { get; set; }

        public int Telefono { get; set; }

        [StringLength(200), Required]
        public string Direccion { get; set; }

        public string Talle { get; set; }

        public string Recorrido { get; set; }

        public int NumeroDeCalle { get; set; }

        public string FechaDeAlta { get; set; }

    }
}