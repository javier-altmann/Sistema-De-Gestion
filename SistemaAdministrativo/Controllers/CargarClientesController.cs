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


namespace SistemaAdministrativo.Controllers
{
    public class CargarClientesController : Controller
    {
        public CargarClientesDAO _clientesDAO;
        private SistemaGestionEntities context;

        //Cargo los DAO en el constructor para poder utilizarlo en los Controladores que traigo de los DAO.
        public CargarClientesController()
        {
            _clientesDAO = new CargarClientesDAO();
            context = new SistemaGestionEntities();
        }

        //Se muestra la tabla con los datos de los clientes con un paginador
        public ViewResult Index(int? pagina)
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
            var devolverListaDeClientes = _clientesDAO.CustomersSearch(text);
            var filtroDeClientes = new List<ClientesConsultaViewModel>();
            foreach (var cliente in devolverListaDeClientes)
            {
                filtroDeClientes.Add(new ClientesConsultaViewModel()
                {
                    Id_Cliente = cliente.Id_Cliente,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = (int)cliente.Telefono,
                    Direccion = cliente.Direccion,
                    Numero_De_Calle = (int)cliente.Numero_De_Calle,
                    Fecha_Alta = cliente.Fecha_Alta,
                    Recorrido = cliente.Recorrido,
                    Talle = cliente.Talle
                });
            }
            

            string jsonResults = JsonConvert.SerializeObject(filtroDeClientes);

            return Json(jsonResults, JsonRequestBehavior.AllowGet);
        }

        //Levanta vista del Create usuario
        public ActionResult Create()
        {

            return View();
        }


        //Se crea un Cliente 
        [HttpPost]
        public ActionResult Create(ClientesViewModel cliente)
        {
           
            var fechaString = DateTime.Today.ToString();
            //Guardo dentro de fechaSinHora la fecha con formato Dia-Mes-Año
            var fechaSinHora = fechaString.Split(' ').FirstOrDefault().Replace("/", "-");

            //Intenta insertar un cliente en la base de datos que ingresa el usuario mediante el ClientesViewModel
            try { 
                _clientesDAO.Insertar(new Cliente()
                { 
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion,
                    Numero_De_Calle = cliente.Numero_De_Calle,
                    Fecha_Alta = fechaSinHora,
                    Recorrido = cliente.Recorrido,
                    Talle = cliente.Talle

                });
            }
            catch(Exception ex) { 
                //Enviar a pantalla con Tipo de error
            }
            return Redirect("Index");
        }

        // Se muestran los datos del cliente que se requiere buscar (por Id)
        public ActionResult Update(int id)
        {
            var Cliente = _clientesDAO.BuscarPorId(id);
            //Muestra las propiedades que va a editar del cliente seleccionado por el usuario
            var ClienteId = new ClientesViewModel
            {
                Nombre = Cliente.Nombre,
                Apellido = Cliente.Apellido,
                Direccion = Cliente.Direccion,
                Numero_De_Calle = (int)Cliente.Numero_De_Calle,
                Telefono = (int)Cliente.Telefono,
                Recorrido = Cliente.Recorrido,
                Talle = Cliente.Talle

            };
            return View(ClienteId);

        }

        //Se actualizan los datos del cliente que el usuario elige (por Id)
        [HttpPost]
        public ActionResult Update(ClientesViewModel cliente, int id)
        {
            var idCliente = _clientesDAO.BuscarPorId(id);

            //Actualiza las propiedades cambiadas del cliente elegido por el usuario 
            try
            {
                idCliente.Telefono = cliente.Telefono;
                idCliente.Direccion = cliente.Direccion;
                idCliente.Numero_De_Calle = cliente.Numero_De_Calle;
                idCliente.Numero_De_Calle = cliente.Numero_De_Calle;
                idCliente.Recorrido = cliente.Recorrido;
                idCliente.Talle = cliente.Talle;

                _clientesDAO.Actualizar(idCliente);
            }
            catch (Exception ex) {
                //Hacer cartel de error
            }

            return RedirectToAction("Index");

        }

        //Se muestra los datos del usuario que se van a eliminar.
        //GET
        public ActionResult Delete(int id)
        {
            var fechaString = DateTime.Today.ToString();
            //Guardo dentro de fechaSinHora la fecha con formato Dia-Mes-Año
            var fechaSinHora = fechaString.Split(' ').FirstOrDefault().Replace("/", "-");

            var Cliente = _clientesDAO.BuscarPorId(id);
            try {
            var datosDelClienteAEliminar = new ClientesViewModel
            {
                Nombre = Cliente.Nombre,
                Apellido = Cliente.Apellido,
                Direccion = Cliente.Direccion,
                Fecha_Alta = fechaSinHora,
                Telefono = (int)Cliente.Telefono,
                Numero_De_Calle = (int)Cliente.Numero_De_Calle,
                Recorrido = Cliente.Recorrido,
                Talle = Cliente.Talle

            };
            return View(datosDelClienteAEliminar);
            }
            catch (Exception ex)
            {

            }
            //Revisar eso
            return View ();
        }
        //Se elimina el cliente seleccionado
        [HttpPost]
        public ActionResult Delete(int id, string s)
        {

            _clientesDAO.Eliminar(id);

            return RedirectToAction("Index");

        }
    }
}