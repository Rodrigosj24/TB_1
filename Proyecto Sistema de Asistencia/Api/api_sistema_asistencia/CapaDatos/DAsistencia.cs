using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DAsistencia
    {
        private readonly DConexion _conexion;

        public DAsistencia(DConexion conexion)
        {
            _conexion = conexion;
        }

        //Para Registrar el inicio de asistencia en la BD
        public async Task<int> Registrar_Inicio_Asistencia_Async(int pId_Usuario, DateTime p_Fecha_Inicio)
        {
            int pCodig_Asistencia = 0;

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_Registrar_Inicio_Asistencia", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("@Id_Usuario", SqlDbType.VarChar).Value = pId_Usuario;
                        command.Parameters.Add("@Fecha_Inicio", SqlDbType.VarChar).Value = p_Fecha_Inicio;


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            pCodig_Asistencia = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar el inicio de asistencia en SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                }

                return pCodig_Asistencia;
            }
        }


        //Para Registrar el final de asistencia en la BD
        public async Task<int> Registrar_Fin_Asistencia_Async(int pId_Usuario, DateTime p_Fecha_Fin)
        {
            int pCodig_Asistencia = 0;

            using (var cn = _conexion.Conectar_Cadena_Conexion_BD_Produccion())
            {
                try
                {
                    await cn.OpenAsync();

                    using (var command = new SqlCommand("SP_Registrar_Fin_Asistencia", cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("@Id_Usuario", SqlDbType.VarChar).Value = pId_Usuario;
                        command.Parameters.Add("@Fecha_Fin", SqlDbType.VarChar).Value = p_Fecha_Fin;


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            pCodig_Asistencia = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar el final de asistencia en SQL Server.", ex);
                }
                finally
                {
                    cn.Close();
                }

                return pCodig_Asistencia;
            }
        }


    }
}
