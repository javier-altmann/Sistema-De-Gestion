using DAL;
using SistemaAdministrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAdministrativo.Controllers
{
    public class CargarPagosController : Controller
    {
        public CargarPagosDAO _pagosDAO;
        private SistemaGestionEntities context;
        public CargarClientesDAO _clientesDAO;
        public CargarVentasDAO _ventasDAO;

        public CargarPagosController()
        {
            _pagosDAO = new CargarPagosDAO();
            context = new SistemaGestionEntities();
            _clientesDAO = new CargarClientesDAO();
        }
        // GET: CargarPagos
        public ActionResult Index()
        {
            return View();
        }

        // GET: CargarPagos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CargarPagos/Create
        public ActionResult Create()
        {
            var viewModel = new PagosViewModel();
            var listaDeClientes = new List<SelectListItem>();
            var listaDeProductos = new List<SelectListItem>();
            CargarClientesDAO _clientesDAO = new CargarClientesDAO();
            CargarVentasDAO ventasDAO = new CargarVentasDAO();
            IEnumerable<Cliente> _clientes = _clientesDAO.GetListaCompletaClientes();
            IEnumerable<Venta> _ventas = ventasDAO.devolverTodosLosClientes();

            foreach(var i in _clientes)
            {
               listaDeClientes.Add(new SelectListItem { Text = i.Nombre + i.Apellido, Value = i.Id_Cliente.ToString()});

            }

            foreach (var item in _ventas)
            {
                
                listaDeProductos.Add(new SelectListItem { Text = item.Producto_Vendido, Value = item.Id_Venta.ToString() });
                
            }

            //viewModel.listaDeClientes = listaDeClientes;
            viewModel.listaDeProductos = listaDeProductos;

            return View(viewModel);
        }

        // POST: CargarPagos/Create
        [HttpPost]
        public ActionResult Create(PagosViewModel pagos)
        {
            
            try
            {
                
                _pagosDAO.Insertar(new Pago()
                {
                    Id_Pago = pagos.Id_Pago,
                    Id_Venta = pagos.Id_Venta,        
                    Fecha_De_Pago = pagos.FechaDePago,
                    Cantidad_Pagada = pagos.Cantidad_Pagada,
                   
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargarPagos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CargarPagos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargarPagos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CargarPagos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
