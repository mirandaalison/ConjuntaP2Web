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
    public class LaboratorioDAL : CadenaDAL
    {
        public List<LaboratorioCLS> listarLaboratorios()
        {
            List<LaboratorioCLS> lista = new List<LaboratorioCLS>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new LaboratorioCLS
                                {
                                    idLaboratorio = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    direccion = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    personaContacto = dr.IsDBNull(3) ? null : dr.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en listarLaboratorios: {ex.Message}");
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        public List<LaboratorioCLS> filtrarLaboratorios(LaboratorioCLS obj)
        {
            List<LaboratorioCLS> lista = new List<LaboratorioCLS>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion == null ? "" : obj.direccion);
                        cmd.Parameters.AddWithValue("@personacontacto", obj.personaContacto == null ? "" : obj.personaContacto);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new LaboratorioCLS
                                {
                                    idLaboratorio = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    direccion = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    personaContacto = dr.IsDBNull(3) ? null : dr.GetString(3)
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

        public int GuardarLaboratorio(LaboratorioCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Laboratorio (NOMBRE, DIRECCION, PERSONACONTACTO, BHABILITADO) VALUES (@nombre, @direccion, @personaContacto, 1)", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                        cmd.Parameters.AddWithValue("@personaContacto", obj.personaContacto);
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

        public LaboratorioCLS recuperarLaboratorio(int idLaboratorio)
        {
            LaboratorioCLS oLaboratorioCLS = new LaboratorioCLS();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDLABORATORIO as idLaboratorio, NOMBRE, DIRECCION, PERSONACONTACTO FROM Laboratorio WHERE BHABILITADO = 1 AND IIDLABORATORIO = @idLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idLaboratorio", idLaboratorio);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (dr.HasRows)
                        {
                            int posIdLaboratorio = dr.GetOrdinal("idLaboratorio");
                            int posNombre = dr.GetOrdinal("nombre");
                            int posDireccion = dr.GetOrdinal("direccion");
                            int posPersonaContacto = dr.GetOrdinal("PERSONACONTACTO");

                            while (dr.Read())
                            {
                                oLaboratorioCLS.idLaboratorio = dr.IsDBNull(posIdLaboratorio) ? 0 : dr.GetInt32(posIdLaboratorio);
                                oLaboratorioCLS.nombre = dr.IsDBNull(posNombre) ? " " : dr.GetString(posNombre);
                                oLaboratorioCLS.direccion = dr.IsDBNull(posDireccion) ? " " : dr.GetString(posDireccion);
                                oLaboratorioCLS.personaContacto = dr.IsDBNull(posPersonaContacto) ? " " : dr.GetString(posPersonaContacto);
                            }
                        }
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    oLaboratorioCLS = new LaboratorioCLS();
                }
            }

            return oLaboratorioCLS;
        }

        public int GuardarCambiosLaboratorio(LaboratorioCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Laboratorio SET NOMBRE = @nombre, DIRECCION = @direccion, PERSONACONTACTO = @personaContacto WHERE IIDLABORATORIO = @idLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                        cmd.Parameters.AddWithValue("@personaContacto", obj.personaContacto);
                        cmd.Parameters.AddWithValue("@idLaboratorio", obj.idLaboratorio);

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

        public int EliminarLaboratorio(int idLaboratorio)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Laboratorio SET BHABILITADO = 0 WHERE IIDLABORATORIO = @idLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idLaboratorio", idLaboratorio);

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