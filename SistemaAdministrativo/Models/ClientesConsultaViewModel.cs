using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaAdministrativo.Models
{
    public class ClientesConsultaViewModel
    {
        public int Id_Cliente { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Telefono { get; set; }

        public string Direccion { get; set; }

        public string Talle { get; set; }

        public string Recorrido { get; set; }

        public int Numero_De_Calle { get; set; }

        public string Fecha_Alta { get; set; }

      

    }
}