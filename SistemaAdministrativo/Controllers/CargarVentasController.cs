using DAL;
using SistemaAdministrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAdministrativo.Controllers
{
    public class CargarVentasController : Controller
    {
        private SistemaGestionEntities context;
        private CargarVentasDAO _ventasDAO;
        private CargarClientesDAO _clientesDAO;

        public CargarVentasController()
        {
            context = new SistemaGestionEntities();
            _ventasDAO = new CargarVentasDAO();
        }
        // GET: CargarVentas
        public ActionResult Index()
        {
            return View();
        }

        // GET: CargarVentas/Create
        public ActionResult Create()
        {
            var listaDeClientesParaDropDown = new VentasViewModel();
            var listadoDeClientes = new List<SelectListItem>();
            CargarClientesDAO _clientesDAO = new CargarClientesDAO();
            IEnumerable<Cliente> _clientes = _clientesDAO.GetListaCompletaClientes();

            foreach (var item in _clientes)
            {
                listadoDeClientes.Add(new SelectListItem
                { Text = item.Nombre + " "+item.Apellido, Value = item.Id_Cliente.ToString() });

            }

            listaDeClientesParaDropDown.listadoDeClientes = listadoDeClientes;
            return View(listaDeClientesParaDropDown);
        }
//     

        // POST: CargarVentas/Create
        [HttpPost]
        public ActionResult Create(VentasViewModel clientes)
        {
            try
            {
                _ventasDAO.Insertar(new Venta()
                {
                    Id_Cliente = clientes.Id_Cliente,
                    Producto_Vendido = clientes.Producto_Vendido,
                    Talle = clientes.Talle,
                    Codigo_Articulo = clientes.Codigo_Articulo,
                    Precio_Real_Del_Producto = clientes.Precio_Real_Del_Producto,
                    Precio_De_Venta_Del_Producto = clientes.Precio_De_Venta_Del_Producto,
                    Fecha_De_Venta = clientes.Fecha_De_Venta,

                    
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargarVentas/Updat/5
        public ActionResult Update(int id)
        {
            var venta =_ventasDAO.BuscarPorId(id);
            var datosDeVenta = new VentasViewModel
            {
                Producto_Vendido = venta.Producto_Vendido,
                Talle = venta.Talle,
                Codigo_Articulo = venta.Codigo_Articulo,
                Precio_Real_Del_Producto = (decimal)venta.Precio_Real_Del_Producto,
                Precio_De_Venta_Del_Producto = (decimal)venta.Precio_De_Venta_Del_Producto,
                Fecha_De_Venta = venta.Fecha_De_Venta,
            };
            return View(datosDeVenta);
        }

        // POST: CargarVentas/Update/5
        [HttpPost]
        public ActionResult Update(int id, VentasViewModel ventas)
        {
            try
            {
                var idVentas = _ventasDAO.BuscarPorId(id);

                idVentas.Producto_Vendido = ventas.Producto_Vendido;
                idVentas.Talle = ventas.Talle;
                idVentas.Codigo_Articulo = ventas.Codigo_Articulo;
                idVentas.Precio_Real_Del_Producto = (decimal)ventas.Precio_Real_Del_Producto;
                idVentas.Precio_De_Venta_Del_Producto = (decimal)ventas.Precio_De_Venta_Del_Producto;
                idVentas.Fecha_De_Venta = ventas.Fecha_De_Venta;

                _ventasDAO.Actualizar(idVentas);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargarVentas/Delete/5
        public ActionResult Delete(int id)
        {
            var ventaAEliminar = _ventasDAO.BuscarPorId(id);

            var datosDeVentaAEliminar = new VentasViewModel {

                Producto_Vendido = ventaAEliminar.Producto_Vendido,
                Talle = ventaAEliminar.Talle,
                Codigo_Articulo = ventaAEliminar.Codigo_Articulo,
                Precio_Real_Del_Producto = (decimal)ventaAEliminar.Precio_Real_Del_Producto,
                Precio_De_Venta_Del_Producto = (decimal)ventaAEliminar.Precio_De_Venta_Del_Producto,
                Fecha_De_Venta = ventaAEliminar.Fecha_De_Venta,

            };
            return View(datosDeVentaAEliminar);
        }

        // POST: CargarVentas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,string sobrecarga)
        {
            try
            {
                _ventasDAO.Eliminar(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
