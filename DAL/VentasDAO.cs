using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class VentasDAO : BaseDAO<Venta>
    {
        public SistemaGestionEntities context; 

        public VentasDAO()
        {
            context = new SistemaGestionEntities();
        }

        public IEnumerable<Venta> devolverTodosLosClientes()
        {
            var todosLosClientes = context.Ventas.ToList();
            return todosLosClientes;
        }

    }
}