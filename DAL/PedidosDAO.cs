using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class PedidosDAO : BaseDAO<Pedido>
    {
        private SistemaGestionEntities context;

        public PedidosDAO()
        {
            context = new SistemaGestionEntities();
        }

        //Devuelve una lista con los pedidos que corresponden a la fecha que indique el usuario.
        public List<Pedido> Fecha (string fecha)
        {
            var consultaFecha = context.Pedidos.Where(c => c.Fecha_Del_Pedido == fecha);
         
            return consultaFecha.ToList();
        } 


    }
}