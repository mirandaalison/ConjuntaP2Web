using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class MedicamentoDAL : CadenaDAL
    {
        public List<MedicamentoCLS> listarMedicamentos()
        {
            List<MedicamentoCLS> lista = new List<MedicamentoCLS>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new MedicamentoCLS
                                {
                                    idMedicamento = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    laboratorio = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    tipoMedicamento = dr.IsDBNull(3) ? null : dr.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en listarMedicamentos: {ex.Message}");
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        public List<MedicamentoCLS> filtrarMedicamentos(MedicamentoCLS obj)
        {
            List<MedicamentoCLS> lista = new List<MedicamentoCLS>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@idlaboratorio", obj.laboratorio == null ? "" : obj.laboratorio);
                        cmd.Parameters.AddWithValue("@idmedicamento", obj.tipoMedicamento == null ? "" : obj.tipoMedicamento);
                        cmd.Parameters.AddWithValue("@idtipomedicamento", obj.tipoMedicamento == null ? "" : obj.tipoMedicamento);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new MedicamentoCLS
                                {
                                    idMedicamento = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    laboratorio = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    tipoMedicamento = dr.IsDBNull(3) ? null : dr.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        public int GuardarMedicamento(MedicamentoCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Medicamento (NOMBRE, LABORATORIO, TIPO_MEDICAMENTO, BHABILITADO) VALUES (@nombre, @laboratorio, @tipoMedicamento, 1)", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@laboratorio", obj.laboratorio);
                        cmd.Parameters.AddWithValue("@tipoMedicamento", obj.tipoMedicamento);
                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }

        public MedicamentoCLS recuperarMedicamento(int idMedicamento)
        {
            MedicamentoCLS oMedicamentoCLS = new MedicamentoCLS();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDMEDICAMENTO as idMedicamento, NOMBRE, LABORATORIO, TIPO_MEDICAMENTO FROM Medicamento WHERE BHABILITADO = 1 AND IIDMEDICAMENTO = @idMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (dr.HasRows)
                        {
                            int posIdMedicamento = dr.GetOrdinal("idMedicamento");
                            int posNombre = dr.GetOrdinal("nombre");
                            int posLaboratorio = dr.GetOrdinal("laboratorio");
                            int posTipoMedicamento = dr.GetOrdinal("tipoMedicamento");

                            while (dr.Read())
                            {
                                oMedicamentoCLS.idMedicamento = dr.IsDBNull(posIdMedicamento) ? 0 : dr.GetInt32(posIdMedicamento);
                                oMedicamentoCLS.nombre = dr.IsDBNull(posNombre) ? " " : dr.GetString(posNombre);
                                oMedicamentoCLS.laboratorio = dr.IsDBNull(posLaboratorio) ? " " : dr.GetString(posLaboratorio);
                                oMedicamentoCLS.tipoMedicamento = dr.IsDBNull(posTipoMedicamento) ? " " : dr.GetString(posTipoMedicamento);
                            }
                        }
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    oMedicamentoCLS = new MedicamentoCLS();
                }
            }

            return oMedicamentoCLS;
        }

        public int GuardarCambiosMedicamento(MedicamentoCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Medicamento SET NOMBRE = @nombre, LABORATORIO = @laboratorio, TIPO_MEDICAMENTO = @tipoMedicamento WHERE IIDMEDICAMENTO = @idMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@laboratorio", obj.laboratorio);
                        cmd.Parameters.AddWithValue("@tipoMedicamento", obj.tipoMedicamento);
                        cmd.Parameters.AddWithValue("@idMedicamento", obj.idMedicamento);

                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }

        public int EliminarMedicamento(int idMedicamento)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Medicamento SET BHABILITADO = 0 WHERE IIDMEDICAMENTO = @idMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);

                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }
    }
}
