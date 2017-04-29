using DAL;
using SistemaAdministrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAdministrativo.Controllers
{
    public class VentasController : Controller
    {
        private SistemaGestionEntities context;
        private VentasDAO _ventasDAO;
        private ClientesDAO _clientesDAO;

        public VentasController()
        {
            context = new SistemaGestionEntities();
            _ventasDAO = new VentasDAO();
        }
        // GET: CargarVentas
        public ActionResult Index()
        {
            return View();
        }

        // GET: CargarVentas/Create
        public ActionResult CrearVenta()
        {
            var listaDeClientesParaDropDown = new VentasViewModel();
            var listadoDeClientes = new List<SelectListItem>();
            ClientesDAO _clientesDAO = new ClientesDAO();
            IEnumerable<Cliente> _clientes = _clientesDAO.GetListaCompletaClientes();

            foreach (var item in _clientes)
            {
                listadoDeClientes.Add(new SelectListItem
                { Text = item.Nombre + " " + item.Apellido, Value = item.Id_Cliente.ToString() });

            }

            listaDeClientesParaDropDown.listadoDeClientes = listadoDeClientes;
            return View(listaDeClientesParaDropDown);
        }
        //     

        // POST: CargarVentas/Create
        [HttpPost]
        public ActionResult CrearVenta(VentasViewModel clientes)
        {
            try
            {
                _ventasDAO.Insertar(new Venta()
                {
                    Id_Cliente = clientes.Id_Cliente,
                    Producto_Vendido = clientes.ProductoVendido,
                    Talle = clientes.Talle,
                    Codigo_Articulo = clientes.CodigoArticulo,
                    Precio_Real_Del_Producto = clientes.PrecioRealDelProducto,
                    Precio_De_Venta_Del_Producto = clientes.PrecioDeVentaDelProducto,
                    Fecha_De_Venta = clientes.FechaDeVenta,


                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargarVentas/Updat/5
        public ActionResult ActualizarVenta(int id)
        {
            var venta = _ventasDAO.BuscarPorId(id);
            var datosDeVenta = new VentasViewModel
            {
                ProductoVendido = venta.Producto_Vendido,
                Talle = venta.Talle,
                CodigoArticulo = venta.Codigo_Articulo,
                PrecioRealDelProducto = (decimal)venta.Precio_Real_Del_Producto,
                PrecioDeVentaDelProducto = (decimal)venta.Precio_De_Venta_Del_Producto,
                FechaDeVenta = venta.Fecha_De_Venta,
            };
            return View(datosDeVenta);
        }

        // POST: CargarVentas/Update/5
        [HttpPost]
        public ActionResult ActualizarVenta(int id, VentasViewModel ventas)
        {
            try
            {
                var idVentas = _ventasDAO.BuscarPorId(id);

                idVentas.Producto_Vendido = ventas.ProductoVendido;
                idVentas.Talle = ventas.Talle;
                idVentas.Codigo_Articulo = ventas.CodigoArticulo;
                idVentas.Precio_Real_Del_Producto = (decimal)ventas.PrecioRealDelProducto;
                idVentas.Precio_De_Venta_Del_Producto = (decimal)ventas.PrecioDeVentaDelProducto;
                idVentas.Fecha_De_Venta = ventas.FechaDeVenta;

                _ventasDAO.Actualizar(idVentas);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargarVentas/Delete/5
        public ActionResult EliminarVenta(int id)
        {
            var ventaAEliminar = _ventasDAO.BuscarPorId(id);

            var datosDeVentaAEliminar = new VentasViewModel
            {

                ProductoVendido = ventaAEliminar.Producto_Vendido,
                Talle = ventaAEliminar.Talle,
                CodigoArticulo = ventaAEliminar.Codigo_Articulo,
                PrecioRealDelProducto = (decimal)ventaAEliminar.Precio_Real_Del_Producto,
                PrecioDeVentaDelProducto = (decimal)ventaAEliminar.Precio_De_Venta_Del_Producto,
                FechaDeVenta = ventaAEliminar.Fecha_De_Venta,

            };
            return View(datosDeVentaAEliminar);
        }

        // POST: CargarVentas/Delete/5
        [HttpPost]
        public ActionResult EliminarVenta(int id, string sobrecarga)
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
