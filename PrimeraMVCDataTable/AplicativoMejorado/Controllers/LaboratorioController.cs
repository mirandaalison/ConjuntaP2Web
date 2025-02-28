using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace AplicativoMejorado.Controllers
{
    public class LaboratorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<LaboratorioCLS> listarLaboratorios()
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.listarLaboratorios();
        }

        public List<LaboratorioCLS> filtrarLaboratorios(LaboratorioCLS objLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.filtrarLaboratorios(objLaboratorio);
        }

        public int GuardarLaboratorio(LaboratorioCLS objLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.GuardarLaboratorio(objLaboratorio);
        }

        public LaboratorioCLS recuperarLaboratorio(int idLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.recuperarLaboratorio(idLaboratorio);
        }

        public int GuardarCambiosLaboratorio(LaboratorioCLS objLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.GuardarCambiosLaboratorio(objLaboratorio);
        }

        public int EliminarLaboratorio(int idLaboratorio)
        {
            LaboratorioDAL objDAL = new LaboratorioDAL();
            return objDAL.EliminarLaboratorio(idLaboratorio);
        }
    }
}
