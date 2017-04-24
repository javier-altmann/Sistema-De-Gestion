using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace DAL
{
    public class CargarClientesDAO : BaseDAO<Cliente>
    {
        private SistemaGestionEntities context;

        public CargarClientesDAO()
        {
            context = new SistemaGestionEntities();
        }

        //Devuelve una lista de todos los clientes
        public IEnumerable<Cliente> GetListaCompletaClientes()
        {
            var cliente = context.Clientes.ToList();
            return cliente;
        }

        /*
        Devuelve una lista de los clientes que se busquen. Como filtro se usa que contenga una letra que corresponda
         a algun nombre, apellido o direccion.      
        */

        public List<Cliente> CustomersSearch (string text)
        {
            
                var result = from c in context.Clientes
                             where
            c.Nombre.Contains(text) ||
            c.Apellido.Contains(text) ||
            c.Direccion.Contains(text)
                             select c;
                return result.ToList();
            }

    }
}