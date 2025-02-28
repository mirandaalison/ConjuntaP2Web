using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class TipoMedicamentoBL
    {
        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.listarTipoMedicamento();
        }

        public List<TipoMedicamentoCLS> filtrarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.filtrarTipoMedicamento(objTipoMedicamento);
        }

        public int GuardarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.GuardarTipoMedicamento(objTipoMedicamento);
        }

        public TipoMedicamentoCLS recuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.recuperarTipoMedicamento(idTipoMedicamento);
        }

        public int GuardarCambiosTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.GuardarCambiosTipoMedicamento(objTipoMedicamento);
        }

        public int EliminarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoDAL objDAL = new TipoMedicamentoDAL();
            return objDAL.EliminarTipoMedicamento(idTipoMedicamento);
        }

    }
}
