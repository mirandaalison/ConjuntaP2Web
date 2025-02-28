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
    public class SucursalDAL : CadenaDAL
    {
        public List<SucursalCLS> listarSucursales()
        {
            List<SucursalCLS> lista = new List<SucursalCLS>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarSucursal", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SucursalCLS sucursal = new SucursalCLS
                                {
                                    idSucursal = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    direccion = dr.GetString(2)
                                };
                                lista.Add(sucursal);
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

        public List<SucursalCLS> filtrarSucursales(SucursalCLS obj)
        {
            List<SucursalCLS> lista = new List<SucursalCLS>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarSucursal", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombresucursal", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", (object)obj.direccion ?? DBNull.Value);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SucursalCLS sucursal = new SucursalCLS
                                {
                                    idSucursal = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    direccion = dr.GetString(2)
                                };
                                lista.Add(sucursal);
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

        public int GuardarSucursal(SucursalCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into Sucursal(NOMBRE, DIRECCION, BHABILITADO)values (@nombre,@direccion,1);", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion);
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

        public SucursalCLS recuperarSucursal(int idSucursal)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDSUCURSAL as idSucursal, NOMBRE as nombre, DIRECCION as direccion FROM Sucursal WHERE BHABILITADO = 1 AND IIDSUCURSAL = @iidsucursal", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@iidsucursal", idSucursal);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            oSucursalCLS.idSucursal = dr.IsDBNull(dr.GetOrdinal("idSucursal")) ? 0 : dr.GetInt32(dr.GetOrdinal("idSucursal"));
                            oSucursalCLS.nombre = dr.IsDBNull(dr.GetOrdinal("nombre")) ? string.Empty : dr.GetString(dr.GetOrdinal("nombre"));
                            oSucursalCLS.direccion = dr.IsDBNull(dr.GetOrdinal("direccion")) ? string.Empty : dr.GetString(dr.GetOrdinal("direccion"));
                        }
                        dr.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return oSucursalCLS;
        }

        public int GuardarCambiosSucursal(SucursalCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Sucursal SET NOMBRE = @nombre, DIRECCION = @direccion WHERE IIDSUCURSAL = @idSucursal", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                        cmd.Parameters.AddWithValue("@idSucursal", obj.idSucursal);

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

        public int EliminarSucursal(int idSucursal)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Sucursal SET BHABILITADO = 0 WHERE IIDSUCURSAL = @idSucursal", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idSucursal", idSucursal);

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
