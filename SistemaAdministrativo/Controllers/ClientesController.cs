using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using PagedList;
using PagedList.Mvc;
using SistemaAdministrativo.Models;
using Newtonsoft.Json;


namespace AdminGestion.Controllers
{
    public class ClientesController : Controller
    {
        public ClientesDAO _clientesDAO;
        private SistemaGestionEntities context;

        //Cargo los DAO en el constructor para poder utilizarlo en los Controladores que traigo de los DAO.
        public ClientesController()
        {
            _clientesDAO = new ClientesDAO();
            context = new SistemaGestionEntities();
        }

        //Se muestra la tabla con los datos de los clientes con un paginador
        public ViewResult PaginaPrincipalClientes(int? pagina)
        {
            int tamañoDePagina = 10;
            int numeroPagina = pagina ?? 1;
            return View(context.Clientes.OrderBy(p => p.Id_Cliente).ToPagedList(numeroPagina, tamañoDePagina));
        }
        //Levanta la vista SearchClient
        [HttpGet]
        public ActionResult SearchClient()
        {
            return View();
        }

        /*
        Mapeo a ClientesConsultaViewModel los datos que requiero para mostrar en la tabla Search
        Después genero objeto Json con la busqueda que hace el usuario del cliente.
        */
        [HttpPost]
        public ActionResult SearchClient(string text)
        {
            var devolverListaDeClientesFiltrada = _clientesDAO.CustomersSearch(text);

            var ListaDeClientesFiltrados = new List<ClientesConsultaViewModel>();

            foreach (var cliente in devolverListaDeClientesFiltrada)
            {
                ListaDeClientesFiltrados.Add(new ClientesConsultaViewModel()
                {
                    Id_Cliente = cliente.Id_Cliente,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = (int)cliente.Telefono,
                    Direccion = cliente.Direccion,
                    NumeroDeCalle = (int)cliente.Numero_De_Calle,
                    FechaDeAlta = cliente.Fecha_Alta,
                    Recorrido = cliente.Recorrido,
                    Talle = cliente.Talle
                });
            }


            string jsonResults = JsonConvert.SerializeObject(ListaDeClientesFiltrados);

            return Json(jsonResults, JsonRequestBehavior.AllowGet);
        }

        //Levanta vista del Create usuario
        public ActionResult CrearCliente()
        {

            return View();
        }


        //Se crea un Cliente 
        [HttpPost]
        public ActionResult CrearCliente(ClientesViewModel cliente)
        {

            var fechaString = DateTime.Today.ToString();
            //Guardo dentro de fechaSinHora la fecha con formato Dia-Mes-Año
            var fechaSinHora = fechaString.Split(' ').FirstOrDefault().Replace("/", "-");

            //Intenta insertar un cliente en la base de datos que ingresa el usuario mediante el ClientesViewModel
            try
            {
                _clientesDAO.Insertar(new Cliente()
                {
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion,
                    Numero_De_Calle = cliente.NumeroDeCalle,
                    Fecha_Alta = fechaSinHora,
                    Recorrido = cliente.Recorrido,
                    Talle = cliente.Talle

                });
            }
            catch (Exception ex)
            {
                //Enviar a pantalla con Tipo de error
            }
            return Redirect("PaginaPrincipalClientes");
        }

        // Se muestran los datos del cliente que se requiere buscar (por Id)
        public ActionResult ActualizarCliente(int id)
        {
            var Cliente = _clientesDAO.BuscarPorId(id);
            //Muestra las propiedades que va a editar del cliente seleccionado por el usuario
            var ClienteId = new ClientesViewModel
            {
                Nombre = Cliente.Nombre,
                Apellido = Cliente.Apellido,
                Direccion = Cliente.Direccion,
                NumeroDeCalle = (int)Cliente.Numero_De_Calle,
                Telefono = (int)Cliente.Telefono,
                Recorrido = Cliente.Recorrido,
                Talle = Cliente.Talle

            };
            return View(ClienteId);

        }

        //Se actualizan los datos del cliente que el usuario elige (por Id)
        [HttpPost]
        public ActionResult ActualizarCliente(ClientesViewModel cliente, int id)
        {
            var idCliente = _clientesDAO.BuscarPorId(id);

            //Actualiza las propiedades cambiadas del cliente elegido por el usuario 
            try
            {
                idCliente.Telefono = cliente.Telefono;
                idCliente.Direccion = cliente.Direccion;
                idCliente.Numero_De_Calle = cliente.NumeroDeCalle;
                idCliente.Recorrido = cliente.Recorrido;
                idCliente.Talle = cliente.Talle;

                _clientesDAO.Actualizar(idCliente);
            }
            catch (Exception ex)
            {
                //Hacer cartel de error
            }

            return RedirectToAction("PaginaPrincipalClientes");

        }

        //Se muestra los datos del usuario que se van a eliminar.
        //GET
        public ActionResult EliminarCliente(int id)
        {
            var fechaString = DateTime.Today.ToString();
            //Guardo dentro de fechaSinHora la fecha con formato Dia-Mes-Año
            var fechaSinHora = fechaString.Split(' ').FirstOrDefault().Replace("/", "-");

            var Cliente = _clientesDAO.BuscarPorId(id);
            try
            {
                var datosDelClienteAEliminar = new ClientesViewModel
                {
                    Nombre = Cliente.Nombre,
                    Apellido = Cliente.Apellido,
                    Direccion = Cliente.Direccion,
                    FechaDeAlta = fechaSinHora,
                    Telefono = (int)Cliente.Telefono,
                    NumeroDeCalle = (int)Cliente.Numero_De_Calle,
                    Recorrido = Cliente.Recorrido,
                    Talle = Cliente.Talle

                };
                return View(datosDelClienteAEliminar);
            }
            catch (Exception ex)
            {
                // Pantalla de error
            }
            //Revisar eso
            return View();
        }
        //Se elimina el cliente seleccionado
        [HttpPost]
        public ActionResult EliminarCliente(int id, string s)
        {

            _clientesDAO.Eliminar(id);

            return RedirectToAction("PaginaPrincipalClientes");

        }

    }

}
