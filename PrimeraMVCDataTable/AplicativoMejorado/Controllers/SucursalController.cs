using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
namespace AplicativoMejorado.Controllers
{
    public class SucursalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<SucursalCLS> listarSucursales()
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.listarSucursales();
        }
        public List<SucursalCLS> filtrarSucursales(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.filtrarSucursales(objSucursal);
        }
        public int GuardarSucursal(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.GuardarSucursal(objSucursal);
        }

        public SucursalCLS recuperarSucursal(int idSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.recuperarSucursal(idSucursal);
        }

        public int GuardarCambiosSucursal(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.GuardarCambiosSucursal(objSucursal);
        }

        public int EliminarSucursal(int idSucursal)
        {
            SucursalDAL objDAL = new SucursalDAL();
            return objDAL.EliminarSucursal(idSucursal);
        }
    }
}