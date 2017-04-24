using DAL;
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
    public class CargarPedidosController : Controller
    {

        public PedidosDAO _pedidosDAO;
        private SistemaGestionEntities context;

        public CargarPedidosController()
        {
            _pedidosDAO = new PedidosDAO();
            context = new SistemaGestionEntities();
        }

        //Genero la tabla con los pedidos indicados por el usuario en el campo fecha.
        [HttpGet]
        public ActionResult index(String fecha)
        {
            //Hago consulta en el dao para comparar la fecha de la base de datos contra la que pone el usuario
            var results = _pedidosDAO.Fecha(fecha);
            Session["fecha"] = fecha;

            var resultado = new List<PedidosViewModel>();
            foreach (var item in results)
            {
                resultado.Add(new PedidosViewModel()
                {
                    Id_Pedidos = item.Id_Pedidos,
                    Nombre_Del_Cliente = item.Nombre_Del_Cliente,
                    Nombre_De_Producto = item.Nombre_De_Producto,
                    Articulo = item.Articulo,
                    Talle_Del_Producto = item.Talle_Del_Producto,
                    Fecha_Del_Pedido = item.Fecha_Del_Pedido
                });
            }

            return View(resultado);
           
        }

        public FileResult ExportarAExcel(string Fecha)
        {
            //Traigo la fecha del Controller index que el usuario ingreso en la busqueda 
            var fecha = this.Session["fecha"];

           
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Add(
                    System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workbook.ActiveSheet;

            //Indicio el nombre de cada columna del excel
                worksheet.Cells[1, 1] = "Nombre Del Producto";
                worksheet.Cells[1, 2] = "Talle Del Producto";
                worksheet.Cells[1, 3] = "Cliente";
                worksheet.Cells[1, 4] = "Articulo";
                worksheet.Cells[1, 5] = "Fecha del Pedido";
              
                int row = 2;

                PedidosDAO pedidos = new PedidosDAO();

            //Guardo en el Excel los pedidos de la fecha indicada por el usuario
                foreach (Pedido p in pedidos.Fecha(fecha.ToString()))
                {
                    worksheet.Cells[row, 1] = p.Nombre_De_Producto;
                    worksheet.Cells[row, 2] = p.Talle_Del_Producto;
                    worksheet.Cells[row, 3] = p.Nombre_Del_Cliente;
                    worksheet.Cells[row, 4] = p.Articulo;
                    worksheet.Cells[row, 5] = p.Fecha_Del_Pedido;
                    row++;
                }
                var ruta = "D:\\" + fecha + ".xls";
                var nombreDeExcel = fecha + ".xls";
                workbook.SaveAs(ruta);
                workbook.Close();
                Marshal.ReleaseComObject(workbook);

                application.Quit();
                Marshal.FinalReleaseComObject(application);

            //Retorno la ruta, aplicacion y el nombre del Excel para que se muestre en el explorador que se guardo.
            return File(ruta, "application/vnd.ms-excel", nombreDeExcel);

        }

        public ActionResult Create()
        {
            return View();
        }


        // POST: CargarPedidos/Create
        [HttpPost]
        public ActionResult Create(PedidosViewModel pedidos)
        {

            _pedidosDAO.Insertar(new Pedido()
            {
                Id_Pedidos = pedidos.Id_Pedidos,
                Nombre_De_Producto = pedidos.Nombre_De_Producto,
                Nombre_Del_Cliente = pedidos.Nombre_Del_Cliente,
                Articulo = pedidos.Articulo,
                Talle_Del_Producto = pedidos.Talle_Del_Producto,
                Fecha_Del_Pedido = pedidos.Fecha_Del_Pedido
            });
          

                return Redirect("Index");

        }

        // GET: CargarPedidos/Update/5
        public ActionResult Update(int id)
        {
            var Pedido = _pedidosDAO.BuscarPorId(id);

            var pedidosMap = new PedidosViewModel {

                Id_Pedidos = Pedido.Id_Pedidos,
                Nombre_De_Producto = Pedido.Nombre_De_Producto,
                Nombre_Del_Cliente = Pedido.Nombre_Del_Cliente,
                Articulo = Pedido.Articulo,
                Talle_Del_Producto = Pedido.Talle_Del_Producto,
                Fecha_Del_Pedido = Pedido.Fecha_Del_Pedido
            };
         
            return View(pedidosMap);
        }
        //Actualiza los pedidos por id. 
        // POST: CargarPedidos/Update/5
        [HttpPost]
        public ActionResult Update(PedidosViewModel pedidos, int id)
        {
            try
            {
                var idPedido = _pedidosDAO.BuscarPorId(id);

                idPedido.Nombre_De_Producto = pedidos.Nombre_De_Producto;
                idPedido.Talle_Del_Producto = pedidos.Talle_Del_Producto;
                idPedido.Nombre_Del_Cliente = pedidos.Nombre_Del_Cliente;
                idPedido.Talle_Del_Producto = pedidos.Talle_Del_Producto;
                idPedido.Fecha_Del_Pedido = pedidos.Fecha_Del_Pedido;
                idPedido.Articulo = pedidos.Articulo;
           
                _pedidosDAO.Actualizar(idPedido);

                return RedirectToAction("Index");
            }
            
            catch (Exception ex)
            {
                
                return View();
            }
        }

        // GET: CargarPedidos/Delete/5
        public ActionResult Delete(int id)
        {
            //Busco el pedido por id
            var Pedido = _pedidosDAO.BuscarPorId(id);

            //Muestro los datos en pantalla del pedido que se va a eliminar
            var PedidoMap = new PedidosViewModel
            {
                Id_Pedidos = Pedido.Id_Pedidos,
                Nombre_Del_Cliente = Pedido.Nombre_Del_Cliente,
                Nombre_De_Producto = Pedido.Nombre_De_Producto,
                Articulo = Pedido.Articulo,
                Fecha_Del_Pedido = Pedido.Fecha_Del_Pedido,
                Talle_Del_Producto = Pedido.Talle_Del_Producto

            };
            return View(PedidoMap);
        }
        

        // POST: CargarPedidos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
            //Elimina el pedido                
                _pedidosDAO.Eliminar(id);

                return RedirectToAction("Index");
            }
            catch
            {
                //Hacer patanalla de error 
                return View();
            }
        }
    }
}
