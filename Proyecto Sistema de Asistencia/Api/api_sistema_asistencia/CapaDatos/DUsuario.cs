using CapaEntidad;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DUsuario
    {
        private readonly DConexion _conexion;

        public DUsuario(DConexion conexion)
        {
            _conexion = conexion;
        }

        //Obtener la Lista de Roles
        public async Task<List<ERolesDB>> Obtener_Roles_Async()
        {
            var lst_pRoles = new List<ERolesDB>();

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_Listar_Roles", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ERolesDB pDato = new ERolesDB();

                                pDato.id_rol = reader.IsDBNull(reader.GetOrdinal("Id_Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Rol"));
                                pDato.descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? "" : reader.GetString(reader.GetOrdinal("Descripcion"));

                                lst_pRoles.Add(pDato);
                            }
                        }

                        if (lst_pRoles.Count == 0)
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener roles desde SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                }

                return lst_pRoles;
            }
        }

        //Para obtener los datos de los usuario para Login
        public async Task<EUsuarioDB> Obtener_Login_Usuario_Async(string pCorreo, string pContrasenia)
        {
            EUsuarioDB pDato = null;

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_OBTENER_DATOS_USUARIO_LOGIN", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("@Correo", SqlDbType.VarChar).Value = pCorreo;
                        command.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = pContrasenia;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                pDato = new EUsuarioDB();

                                pDato.id_usuario = reader.IsDBNull(reader.GetOrdinal("Id_Usuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Usuario"));
                                pDato.nombres_apellidos = reader.IsDBNull(reader.GetOrdinal("Nombres_Completos")) ? "" : reader.GetString(reader.GetOrdinal("Nombres_Completos"));
                                pDato.celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? "" : reader.GetString(reader.GetOrdinal("Celular"));
                                pDato.correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? "" : reader.GetString(reader.GetOrdinal("Correo"));
                                pDato.imagen = reader.IsDBNull(reader.GetOrdinal("Imagen")) ? "" : reader.GetString(reader.GetOrdinal("Imagen"));
                                pDato.id_rol = reader.IsDBNull(reader.GetOrdinal("Id_Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Rol"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener datos del usuario desde SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                }

                return pDato;
            }
        }

        //Para obtener los datos de los usuario activos de la BD
        public async Task<EUsuarioDB> Obtener_Usuarios_Async(string pCorreo)
        {
            EUsuarioDB pDato = null;

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_OBTENER_DATOS_USUARIO", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("@Correo", SqlDbType.VarChar).Value = pCorreo;


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                pDato = new EUsuarioDB();

                                pDato.id_usuario = reader.IsDBNull(reader.GetOrdinal("Id_Usuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Usuario"));
                                pDato.nombres_apellidos = reader.IsDBNull(reader.GetOrdinal("Nombres_Completos")) ? "" : reader.GetString(reader.GetOrdinal("Nombres_Completos"));
                                pDato.celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? "" : reader.GetString(reader.GetOrdinal("Celular"));
                                pDato.correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? "" : reader.GetString(reader.GetOrdinal("Correo"));
                                pDato.imagen = reader.IsDBNull(reader.GetOrdinal("Imagen")) ? "" : reader.GetString(reader.GetOrdinal("Imagen"));
                                pDato.id_rol = reader.IsDBNull(reader.GetOrdinal("Id_Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Rol"));
                                pDato.estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? false : reader.GetBoolean(reader.GetOrdinal("Estado"));


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener datos del usuario desde SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                }

                return pDato;
            }
        }

        //Para obtener los datos de los usuario activos e inactivos de la BD
        public async Task<EUsuarioDB> Obtener_Usuarios_General_Async(string pCorreo)
        {
            EUsuarioDB pDato = null;

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_OBTENER_DATOS_USUARIO_GENERAL", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("@Correo", SqlDbType.VarChar).Value = pCorreo;


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                pDato = new EUsuarioDB();

                                pDato.id_usuario = reader.IsDBNull(reader.GetOrdinal("Id_Usuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Usuario"));
                                pDato.nombres_apellidos = reader.IsDBNull(reader.GetOrdinal("Nombres_Completos")) ? "" : reader.GetString(reader.GetOrdinal("Nombres_Completos"));
                                pDato.celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? "" : reader.GetString(reader.GetOrdinal("Celular"));
                                pDato.correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? "" : reader.GetString(reader.GetOrdinal("Correo"));
                                pDato.imagen = reader.IsDBNull(reader.GetOrdinal("Imagen")) ? "" : reader.GetString(reader.GetOrdinal("Imagen"));
                                pDato.id_rol = reader.IsDBNull(reader.GetOrdinal("Id_Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Rol"));
                                pDato.estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? false : reader.GetBoolean(reader.GetOrdinal("Estado"));


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener datos del usuario desde SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                }

                return pDato;
            }
        }

        //Obtener la Lista de Usuarios
        public async Task<List<EUsuarioDB>> Listar_Datos_Usuarios_Async()
        {
            var lst_pUsuarios = new List<EUsuarioDB>();

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_Listar_Usuarios", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                EUsuarioDB pDato = new EUsuarioDB();

                                pDato.id_usuario = reader.IsDBNull(reader.GetOrdinal("Id_Usuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Usuario"));
                                pDato.nombres_apellidos = reader.IsDBNull(reader.GetOrdinal("Nombres_Completos")) ? "" : reader.GetString(reader.GetOrdinal("Nombres_Completos"));
                                pDato.celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? "" : reader.GetString(reader.GetOrdinal("Celular"));
                                pDato.correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? "" : reader.GetString(reader.GetOrdinal("Correo"));
                                pDato.id_rol = reader.IsDBNull(reader.GetOrdinal("Id_Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id_Rol"));
                                pDato.rol = reader.IsDBNull(reader.GetOrdinal("Rol")) ? "" : reader.GetString(reader.GetOrdinal("Rol"));
                                pDato.estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? false : reader.GetBoolean(reader.GetOrdinal("Estado"));
                                pDato.descripcion_estado = reader.IsDBNull(reader.GetOrdinal("Descripcion_Estado")) ? "" : reader.GetString(reader.GetOrdinal("Descripcion_Estado"));

                                lst_pUsuarios.Add(pDato);
                            }
                        }

                        if (lst_pUsuarios.Count == 0)
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener roles desde SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                }

                return lst_pUsuarios;
            }
        }

        //Para Eliminar los usuario activos de la BD
        public async Task<int> Eliminar_Usuarios_Async(string pCorreo)
        {
            int pCodig_Usuario = 0;

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_DESAHABILITAR_USUARIOS", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("@Correo", SqlDbType.VarChar).Value = pCorreo;


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            pCodig_Usuario = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar datos del usuario desde SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                }

                return pCodig_Usuario;
            }
        }

        //Para Registrar los nuevos usuarios en la BD
        public async Task<int> Registrar_Usuarios_Async(string p_nombres, string p_apellidos, string p_celular, string p_correo, string p_contrasenia, int p_id_rol, string p_imagen)
        {
            int pCodig_Usuario = 0;

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_Registrar_Usuarios", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = p_nombres;
                        command.Parameters.Add("@Apellidos", SqlDbType.VarChar).Value = p_apellidos;
                        command.Parameters.Add("@Celular", SqlDbType.VarChar).Value = p_celular;
                        command.Parameters.Add("@Correo", SqlDbType.VarChar).Value = p_correo;
                        command.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = p_contrasenia;
                        command.Parameters.Add("@Id_Rol", SqlDbType.VarChar).Value = p_id_rol;
                        command.Parameters.Add("@Imagen", SqlDbType.VarChar).Value = p_imagen;


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            pCodig_Usuario = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar datos del usuario en SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                }

                return pCodig_Usuario;
            }
        }

    }
}
