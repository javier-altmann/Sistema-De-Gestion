using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class PagosDAO : BaseDAO<Pago>
    {
        private SistemaGestionEntities context;

        public PagosDAO()
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
            var ventasPorCliente = context.Ventas.Where(c => c.Id_Cliente == id);
            return ventasPorCliente;

        }

        public IEnumerable<Pago> FiltroPorFechaDePagos(DateTime fechaInicial, DateTime FechaFinal)
        {

            var result = from c in context.Pagos
                         where c.Fecha_De_Pago >= fechaInicial && c.Fecha_De_Pago <= FechaFinal
                         select c;
            return result.ToList();
        }

        public IEnumerable<Pago> FiltroPorFechaAndFullName(DateTime fechaInicial, DateTime FechaFinal, string nombre, string apellido)
        {
            var result = from c in context.Pagos
                         where (c.Fecha_De_Pago >= fechaInicial && c.Fecha_De_Pago <= FechaFinal)
                         && c.Venta.Cliente.Nombre == nombre && c.Venta.Cliente.Apellido == apellido
                         select c;
            return result.ToList();
        }

        public IEnumerable<Pago> AgruparPorCliente()
        {
            var result = from c in context.Pagos
                         orderby c.Venta.Id_Cliente
                         select c;

            return result;
        }



    }
}