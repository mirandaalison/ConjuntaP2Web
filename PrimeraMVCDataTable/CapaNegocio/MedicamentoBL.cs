using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class MedicamentoBL
    {
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
