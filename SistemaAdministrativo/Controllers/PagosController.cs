using DAL;
using Newtonsoft.Json;
using PagedList;
using SistemaAdministrativo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace SistemaAdministrativo.Controllers
{
    public class PagosController : Controller
    {
        public PagosDAO _pagosDAO;
        private SistemaGestionEntities context;
        public ClientesDAO _clientesDAO;
        public VentasDAO _ventasDAO;

        public PagosController()
        {
            _pagosDAO = new PagosDAO();
            context = new SistemaGestionEntities();
            _clientesDAO = new ClientesDAO();
        }

       
        public ActionResult MostrarListaDePagos() {

            return View();
        }
        public string GuardarAjax(DateTime fechaInicial, DateTime fechaFinal)
        {

            var listaDeClientes = new List<ListaDePagosViewModel>();

            var filtroDeFechas = _pagosDAO.FiltroPorFechaDePagos(fechaInicial, fechaFinal);

            foreach (var item in filtroDeFechas)
            {
               
                listaDeClientes.Add(new ListaDePagosViewModel()
                {
                 
                    Nombre = item.Venta.Cliente.Nombre,
                    Apellido = item.Venta.Cliente.Apellido,
                    CodigoDeArticulo = item.Venta.Codigo_Articulo,
                    Producto = item.Venta.Producto_Vendido,
                    FechaDePago = (DateTime)item.Fecha_De_Pago,
                    SaldoActual = (decimal)item.Saldo_Actual

                });
            }

            return JsonConvert.SerializeObject(listaDeClientes);

        }


        public string buscarPedidosPorId(int id)
        {
            var comboDeProductos = new ComboProductoViewModel();


            var listaDeProductos = new List<SelectListItem>();



            foreach (var item in _pagosDAO.GetVentasPorCliente(id))
            {

                listaDeProductos.Add(new SelectListItem { Text = item.Producto_Vendido, Value = item.Id_Cliente.ToString() });

            }
            comboDeProductos.listaProductos = listaDeProductos;
            return JsonConvert.SerializeObject(comboDeProductos);
        }


        // GET: CargarPagos/Create
        public ActionResult RealizarPagos()
        {

            var viewModel = new PagosViewModel();
            var listaDeClientes = new List<SelectListItem>();
            var listaDeProductos = new List<SelectListItem>();
            ClientesDAO _clientesDAO = new ClientesDAO();
            VentasDAO ventasDAO = new VentasDAO();
            IEnumerable<Cliente> _clientes = _clientesDAO.GetListaCompletaClientes();


            foreach (var i in _clientes)
            {
                listaDeClientes.Add(new SelectListItem { Text = i.Nombre + i.Apellido, Value = i.Id_Cliente.ToString() });

            }



            viewModel.listaDeClientes = listaDeClientes;
            viewModel.listaDeProductos = listaDeProductos;

            return View(viewModel);
        }

        // POST: CargarPagos/Create
        [HttpPost]
        public ActionResult RealizarPagos(PagosViewModel pagos)
        {
           

            try
            {

                _pagosDAO.Insertar(new Pago()
                {
                    Id_Pago = pagos.Id_Pago,
                    Id_Venta = pagos.Id_Venta,
                    Fecha_De_Pago = pagos.FechaDePago,
                    Cantidad_Pagada = pagos.Cantidad_Pagada,
                    Saldo_Actual =  pagos.SaldoActual - pagos.Cantidad_Pagada,

    });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargarPagos/Edit/5
        public ActionResult EditarPago(int id)
        {
            return View();
        }

        // POST: CargarPagos/Edit/5
        [HttpPost]
        public ActionResult EditarPago(int id, FormCollection collection)
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
        public ActionResult EliminarPago(int id)
        {
            return View();
        }

        // POST: CargarPagos/Delete/5
        [HttpPost]
        public ActionResult EliminarPago(int id, FormCollection collection)
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
