using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapaDatos
{
    public class TipoMedicamentoDAL:CadenaDAL
    {
        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            List<TipoMedicamentoCLS> lista = new List<TipoMedicamentoCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDTIPOMEDICAMENTO, NOMBRE , DESCRIPCION FROM TipoMedicamento WHERE BHABILITADO=1", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TipoMedicamentoCLS tipoMedicamento = new TipoMedicamentoCLS
                                {
                                    idTipoMedicamento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? " " : dr.GetString(1),
                                    descripcion = dr.IsDBNull(2) ? " " : dr.GetString(2)
                                };

                                lista.Add(tipoMedicamento);
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

        public List<TipoMedicamentoCLS> filtrarTipoMedicamento(TipoMedicamentoCLS obj)
        {
            List<TipoMedicamentoCLS> lista = new List<TipoMedicamentoCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarTipoMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombretipomedicamento", obj.nombre == null ? "" : obj.nombre);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TipoMedicamentoCLS tipoMedicamento = new TipoMedicamentoCLS
                                {
                                    idTipoMedicamento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? " " : dr.GetString(1),
                                    descripcion = dr.IsDBNull(2) ? " " : dr.GetString(2)
                                };

                                lista.Add(tipoMedicamento);
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


        public int GuardarTipoMedicamento(TipoMedicamentoCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into TipoMedicamento(NOMBRE, DESCRIPCION, BHABILITADO)values (@nombre,@descripcion,1);", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
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










        public TipoMedicamentoCLS recuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoCLS oTipoMedicamentoCLS = new TipoMedicamentoCLS();  // Inicializamos el objeto para evitar null

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDTIPOMEDICAMENTO as idTipoMedicamento, NOMBRE, DESCRIPCION FROM TipoMedicamento WHERE BHABILITADO = 1 AND IIDTIPOMEDICAMENTO = @iidtipomedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@iidtipomedicamento", idTipoMedicamento);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (dr.HasRows)  
                        {
                            int posIdTipoMedicamento = dr.GetOrdinal("idTipoMedicamento");
                            int posNombre = dr.GetOrdinal("nombre");
                            int posDescripcion = dr.GetOrdinal("descripcion");

                            while (dr.Read())
                            {
                                oTipoMedicamentoCLS.idTipoMedicamento = dr.IsDBNull(posIdTipoMedicamento) ? 0 : dr.GetInt32(posIdTipoMedicamento);
                                oTipoMedicamentoCLS.nombre = dr.IsDBNull(posNombre) ? " " : dr.GetString(posNombre);
                                oTipoMedicamentoCLS.descripcion = dr.IsDBNull(posDescripcion) ? " " : dr.GetString(posDescripcion);
                            }
                        }
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, logueamos el error y retornamos un objeto vacío
                    Console.WriteLine("Error: " + ex.Message);
                    oTipoMedicamentoCLS = new TipoMedicamentoCLS();  // Retornamos un objeto vacío
                }
            }

            return oTipoMedicamentoCLS;
        }

        public int GuardarCambiosTipoMedicamento(TipoMedicamentoCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE TipoMedicamento SET NOMBRE = @nombre, DESCRIPCION = @descripcion WHERE IIDTIPOMEDICAMENTO = @idTipoMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                        cmd.Parameters.AddWithValue("@idTipoMedicamento", obj.idTipoMedicamento);

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


        public int EliminarTipoMedicamento(int idTipoMedicamento)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE TipoMedicamento SET BHABILITADO = 0 WHERE IIDTIPOMEDICAMENTO = @idTipoMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idTipoMedicamento", idTipoMedicamento);

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
