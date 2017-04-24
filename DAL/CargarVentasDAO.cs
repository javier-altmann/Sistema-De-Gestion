using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class CargarVentasDAO : BaseDAO<Venta>
    {
        public SistemaGestionEntities context; 

        public CargarVentasDAO()
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