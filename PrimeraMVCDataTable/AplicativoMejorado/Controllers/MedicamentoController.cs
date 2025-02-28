using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace AplicativoMejorado.Controllers
{
    public class MedicamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<MedicamentoCLS> listarMedicamentos()
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.listarMedicamentos();
        }

        public List<MedicamentoCLS> filtrarMedicamentos(MedicamentoCLS objMedicamento)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.filtrarMedicamentos(objMedicamento);
        }

        public int GuardarMedicamento(MedicamentoCLS objMedicamento)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.GuardarMedicamento(objMedicamento);
        }

        public MedicamentoCLS recuperarMedicamento(int idMedicamento)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.recuperarMedicamento(idMedicamento);
        }

        public int GuardarCambiosMedicamento(MedicamentoCLS objMedicamento)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.GuardarCambiosMedicamento(objMedicamento);
        }

        public int EliminarMedicamento(int idMedicamento)
        {
            MedicamentoDAL objDAL = new MedicamentoDAL();
            return objDAL.EliminarMedicamento(idMedicamento);
        }
    }
}
