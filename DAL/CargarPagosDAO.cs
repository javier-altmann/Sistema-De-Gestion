using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class CargarPagosDAO : BaseDAO<Pago>
    {
        private SistemaGestionEntities context;

        public CargarPagosDAO()
        {
            context = new SistemaGestionEntities();
        }

        public IEnumerable<Pago> GetListaCompletaPagos()
        {
            var pagos = context.Pagos.ToList();
            return pagos;
        }

        public IEnumerable<Venta> GetVentasPorCliente(int id)
        {
            var ventasPorCliente = context.Ventas.Where(c => c.Id_Venta == id);
            return ventasPorCliente;
           
        }


    }
}